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

                    // Confirma a mensagem ap�s o processamento
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    // Se ocorrer um erro, n�o confirma a mensagem e loga o erro
                    Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
                    _channel.BasicNack(ea.DeliveryTag, false, true); // Rejeita a mensagem e coloca na fila novamente
                }
            };

            _channel.BasicConsume(queue: queueName,
                autoAck: false, // N�o confirmamos automaticamente
                consumer: consumidor);

            // Mant�m o servi�o em execu��o enquanto o RabbitMQ estiver enviando mensagens
            await Task.Delay(-1, stoppingToken); // Espera at� que o token de cancelamento seja acionado
        }

        private async Task ProcessarMensagemAsync(string content)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var contatoService = scope.ServiceProvider.GetRequiredService<IContatoService>();
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                // Deserializa o conte�do da mensagem para o DTO de cria��o de contato
                var createContactRequest = JsonConvert.DeserializeObject<CreateContactRequest>(content);

                if (createContactRequest != null)
                {
                    // Mapeia o DTO para a entidade Contato
                    var contato = mapper.Map<Contato>(createContactRequest);

                    // Chama o servi�o para criar o contato
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
