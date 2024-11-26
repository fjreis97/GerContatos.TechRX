using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Business.Services;

public class CreateContactBackgroundService : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly string queueName = "CreateContract";
    
    public CreateContactBackgroundService()
    {
        InicializarRabbitMQ();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumidor = new EventingBasicConsumer(_channel);

        consumidor.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            Console.WriteLine($"Mensagem recebida: {content}");

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: queueName,
            autoAck: false,
            consumer: consumidor);

        return Task.CompletedTask;
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