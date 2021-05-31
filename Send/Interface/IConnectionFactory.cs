using System;
using RabbitMQ.Client;
public interface IConnectionFactory
{
    IConnection CreateConnection();
}