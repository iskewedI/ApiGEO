using System.Text;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Microsoft.Extensions.Hosting;
using GeoApi.Messaging.Receive.Options.v1;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using System.Threading;
using Newtonsoft.Json;
using GeoApi.Service.v1.Services;
using GeoApi.Service.v1.Models;

namespace GeoApi.Messaging.Receive.Receiver.v1
{
    public class CodificationResponseReceiver : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly ICodificationResponseService _codificationResponseService;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;

        public CodificationResponseReceiver(ICodificationResponseService codificationResponseService, IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _codificationResponseService = codificationResponseService;

            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password,
                VirtualHost = "/"
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var codificationResponseModel = JsonConvert.DeserializeObject<CodificationResponseModel>(content);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                HandleMessageAsync(codificationResponseModel);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessageAsync(CodificationResponseModel localizationRequestModel)
        {
            //await _codificationService.CodificateLocalization(localizationRequestModel);
        }

        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
