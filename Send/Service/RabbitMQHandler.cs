using System;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading;
public class RabbitMQHandler{
    private readonly IConnectionFactory factory;
    public RabbitMQHandler(IConnectionFactory factory){
        this.factory = factory;
    }
    public void SendMessage(List<Message> messages){
        try
        {

            using (var conn = factory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare("messages", true, false, false, null);

                    var properties = channel.CreateBasicProperties();

                    properties.Persistent = true;

                    foreach (Message message in messages)
                    {
                        var jsonified = JsonConvert.SerializeObject(message);
                        byte[] messageBuffer = Encoding.UTF8.GetBytes(jsonified);

                        channel.BasicPublish("", "messages", properties, messageBuffer);

                        Console.WriteLine("Message Sent ID:{0}, Text: {1}", message.ID, message.Text);

                        Thread.Sleep(500);
                    }
                }
            }

            System.Console.WriteLine("Exiting....");
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex);
            throw;
        }
    }
}