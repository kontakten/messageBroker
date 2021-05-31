using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Receive
{
    public class Receive
    {
        static void Main()
        {

            System.Console.WriteLine("Starting up....");

            RabbitMQConnectionInfo info = new RabbitMQConnectionInfo()
            {
                HostName = "localhost",
            };

            IConnectionFactory connection = new RabbitMQConnection(info);

            RabbitMQHandler rabbitMQHandler = new RabbitMQHandler(connection);

            rabbitMQHandler.InitReceivedMessages();

        }
    }
}




