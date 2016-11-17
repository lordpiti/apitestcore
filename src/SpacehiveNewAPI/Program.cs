using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Threading;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SpacehiveNewAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //// Start a thread that calls a parameterized static method.
            //Thread newThread = new Thread(Start); 
            //newThread.Start(42);

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        public static void DoWork(object data)
        {
            #region external API call test

            var apiUrl = "http://mockbin.org/bin/30c84903-e7f2-4c7e-815a-e5bcc5cb687a?foo=bar&foo=baz";
                HttpClient client = new HttpClient();

                var task = Task.Run(() => client.GetAsync(apiUrl));
                var aa = task.Result;

            #endregion

            #region Rabbit MQ test

            //SendMessage("Pablo knows");
            //SendMessage("Manny doesn't know");

            //while (true)
            //{
            //    GetMessages();
            //}

            #endregion

            #region DI test

            //var serviceProvider = new ServiceCollection()
            //.AddSingleton<IMongoDbRepository, MongoDbRepository>()
            //.BuildServiceProvider();


            //// now you can resolve services
            //var mongoRepo = serviceProvider.GetRequiredService<IMongoDbRepository>();

            #endregion

        }

        static void Start(object data)
        {

            var autoEvent = new AutoResetEvent(false);

            var stateTimer = new Timer(DoWork,
                                               autoEvent, 1000, 5000);

        }

        #region RabbitMQ testing

        public static void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "pablo",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "pablo",
                                     basicProperties: null,
                                     body: body);
            }
        }

        public static void GetMessages()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "pablo",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                };
                var a = channel.BasicConsume(queue: "pablo",
                                     noAck: true, consumer: consumer);
            }
        }

        #endregion

    }
}
