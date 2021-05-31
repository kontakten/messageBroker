using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading;
public class RabbitMQHandler
{
    private readonly IConnectionFactory factory;

    public RabbitMQHandler(IConnectionFactory factory)
    {
        this.factory = factory;
    }

    public void SendMessages(Message message)
    {
        try
        {
            using (var conn = factory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare("messages", true, false, false, null);

                    var properties = channel.CreateBasicProperties();

                    properties.Persistent = true;

                    var jsonified = JsonConvert.SerializeObject(message);
                    byte[] messageBuffer = Encoding.UTF8.GetBytes(jsonified);

                    channel.BasicPublish("", "messages", properties, messageBuffer);
                }
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex);
            throw;
        }
    }
    public void InitReceivedMessages(){
        try
        {
            using (var conn = factory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare("messages", true, false, false, null);

                    channel.BasicQos(0, 1, false);

                    Console.WriteLine("Waiting for messages.");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var jsonified = Encoding.UTF8.GetString(body);

                        Message messageReceived = JsonConvert.DeserializeObject<Message>(jsonified);

                        DateTime messageTime = Convert.ToDateTime(messageReceived.Timestamp);

                        if (messageTime.Minute < DateTime.Now.Minute - 1)
                        {
                            Console.WriteLine("Message from sender ID: {0} - with timestamp {1} - is now older than one minute", messageReceived.ID, messageTime.ToString("HH:mm:ss"));
                            channel.BasicReject(ea.DeliveryTag, false);
                        }
                        else if (!TimeStampHandler.RegulateEvenTimeStamp(messageTime))
                        {
                            Console.WriteLine("Message from sender ID: {0} - with uneven second timestamp {1} - Resending with new timestamp", messageReceived.ID, messageTime.ToString("HH:mm:ss"));

                            channel.BasicAck(ea.DeliveryTag, true);

                            Message message = new Message()
                            {
                                Text = messageReceived.Text,
                                ID = messageReceived.ID,
                                Timestamp = DateTime.Now.ToString("HH:mm:ss")
                            };

                            SendMessages(message);

                        }
                        else
                        {
                            Console.WriteLine("Message from sender ID: {0} - with even second timestamp {1}", messageReceived.ID, messageTime.ToString("HH:mm:ss"));
                            channel.BasicAck(ea.DeliveryTag, true);

                            MessageHandler.AddMessagesToTable(messageReceived);
                        }
                        Thread.Sleep(500);
                    };

                    string consumerTag = channel.BasicConsume("messages", false, consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex);
            throw;
        }
    }

    
}