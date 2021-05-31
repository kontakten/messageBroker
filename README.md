# messageBroker
Little Message Broker

## Description

Producer & Consumer - messagebroker.

Purpose: 

Producer app: Produce messages with a timestamp. Producer generates 10 messages in this project.

Consumer app:

Take message out of the queue. 
* If the timestamp of the message is more than 1 minute old - throw it away.
* If the timestamp is less than 1 min old and the second hand on the timestamp is an even number, save the message in a database.
* If the timestamp is less than 1 min old and the second hand on the timestamp is an odd number, then put the message back in the queue with a new timestamp

### For Receive (Consumer) project:

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
