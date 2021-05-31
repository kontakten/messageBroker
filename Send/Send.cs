using System;
using RabbitMQ.Client;
using System.Collections.Generic;
class Send
{
    
    static void Main()
    {
        RabbitMQConnectionInfo info = new RabbitMQConnectionInfo()
        {
            HostName = "localhost",
        };

        IConnectionFactory connection = new RabbitMQConnection(info);
        
        RabbitMQHandler rabbitMQHandler = new RabbitMQHandler(connection);

        rabbitMQHandler.SendMessage(CreateMessages());

    }
    private static List<Message> CreateMessages(){
        try
        {
            List<Message> messages = new List<Message>();

            messages.Add(new Message() { Text = "Message 01", ID = 1, Timestamp = DateTime.Now.ToString("HH:mm:ss") });
            messages.Add(new Message() { Text = "Message 02", ID = 2, Timestamp = DateTime.Now.AddSeconds(1).ToString("HH:mm:ss") });
            messages.Add(new Message() { Text = "Message 03", ID = 3, Timestamp = DateTime.Now.AddSeconds(2).ToString("HH:mm:ss") });
            messages.Add(new Message() { Text = "Message 04", ID = 4, Timestamp = DateTime.Now.AddSeconds(3).ToString("HH:mm:ss") });
            messages.Add(new Message() { Text = "Message 05", ID = 5, Timestamp = DateTime.Now.AddSeconds(4).ToString("HH:mm:ss") });
            messages.Add(new Message() { Text = "Message 06", ID = 6, Timestamp = DateTime.Now.AddSeconds(5).ToString("HH:mm:ss") });
            messages.Add(new Message() { Text = "Message 07", ID = 7, Timestamp = DateTime.Now.AddSeconds(6).ToString("HH:mm:ss") });
            messages.Add(new Message() { Text = "Message 08", ID = 8, Timestamp = DateTime.Now.AddSeconds(7).ToString("HH:mm:ss") });
            messages.Add(new Message() { Text = "Message 09", ID = 9, Timestamp = DateTime.Now.AddSeconds(8).ToString("HH:mm:ss") });
            messages.Add(new Message() { Text = "Message 10", ID = 10, Timestamp = DateTime.Now.AddSeconds(9).ToString("HH:mm:ss") });
            
            return messages;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}

