using Business.Interfaces.Services;
using Business.Dtos.Request.Contact;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using AutoMapper;
using Core.Entities;

namespace Business.Services
{
    public class CreateContactBackgroundService : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly string queueName = "CreateContract";
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CreateContactBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            InicializarRabbitMQ();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumidor = new EventingBasicConsumer(_channel);

            consumidor.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($"Mensagem recebida: {content}");

                try
                {
                    // Processa a mensagem recebida
                    await ProcessarMensagemAsync(content);

                    // Confirma a mensagem após o processamento
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    // Se ocorrer um erro, não confirma a mensagem e loga o erro
                    Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
                    _channel.BasicNack(ea.DeliveryTag, false, true); // Rejeita a mensagem e coloca na fila novamente
                }
            };

            _channel.BasicConsume(queue: queueName,
                autoAck: false, // Não confirmamos automaticamente
                consumer: consumidor);

            // Mantém o serviço em execução enquanto o RabbitMQ estiver enviando mensagens
            await Task.Delay(-1, stoppingToken); // Espera até que o token de cancelamento seja acionado
        }

        private async Task ProcessarMensagemAsync(string content)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var contatoService = scope.ServiceProvider.GetRequiredService<IContatoService>();
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                // Deserializa o conteúdo da mensagem para o DTO de criação de contato
                var createContactRequest = JsonConvert.DeserializeObject<CreateContactRequest>(content);

                if (createContactRequest != null)
                {
                    // Mapeia o DTO para a entidade Contato
                    var contato = mapper.Map<Contato>(createContactRequest);

                    // Chama o serviço para criar o contato
                    var response = await contatoService.Create(contato);

                    if (response.IsSuccess)
                    {
                        Console.WriteLine($"Contato criado com sucesso: {response.Data.Id}");
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao criar contato: {response.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Erro ao deserializar a mensagem.");
                }
            }
        }

        private void InicializarRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
