using System;
using RabbitMQ.Client;

public class RabbitMQConnection : IConnectionFactory
{
    private readonly RabbitMQConnectionInfo connectionInfo;
    public RabbitMQConnection(RabbitMQConnectionInfo connectionInfo)
    {
        this.connectionInfo = connectionInfo;
    }

    public IConnection CreateConnection()
    {
        var factory = new ConnectionFactory()
        {
            HostName = connectionInfo.HostName,
        };
        var connection = factory.CreateConnection();
        return connection;
    }
}