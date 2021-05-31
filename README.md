# messageBroker
Little Message Broker

## For Receive (Consumer) project:

1. Create Migrations:

```
dotnet ef migrations add InitialCreate
```

2. Create Database: 

```
dotnet ef database update
```

3. Run program to receive messages from 'Send' Project. 

```
dotnet run
```

## For Send (Producer) project:

Dummy list of message are inside the project. Adding 10 messages with added second for each new message.

1. To run and then send messages to Receive project.

```
dotnet run
```

## For Testing project:

1. Simple testing for sqlite (in memory).

```
dotnet test
```
