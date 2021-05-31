using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Receive;
using System;
using System.Linq;
public class TestReceive
{
    [Fact]
    public void ShouldBeAbleToAddMessage()
    {
        using (var connection = new SqliteConnection("DataSource=:memory:"))
        {
            var options = new DbContextOptionsBuilder<MessageContext>().UseSqlite(connection).Options;
            
            using (var messageDbContext = new MessageContext(options))
            {
                messageDbContext.Database.EnsureDeleted();
                messageDbContext.Database.EnsureCreated();

                messageDbContext.Messages.Add(new Message
                {
                    Text = "MessageOne",
                    ID = 1,
                    Timestamp = DateTime.Now.ToString("HH:mm:ss")
                });

                messageDbContext.SaveChanges();

                var data = messageDbContext.Messages.ToList();

                Assert.Single(data);
            }
        }
    }

    [Fact]
    public void ShouldBeAbleToUpdateMessage()
    {
        using (var connection = new SqliteConnection("DataSource=:memory:"))
        {
            var options = new DbContextOptionsBuilder<MessageContext>().UseSqlite(connection).Options;

            using (var messageDbContext = new MessageContext(options))
            {
                messageDbContext.Database.EnsureCreated();

                var existingMessage = messageDbContext.Messages.SingleOrDefault(m => m.ID == 1);

                existingMessage.Text = "MessageTwo";
                existingMessage.Timestamp = DateTime.Now.ToString("HH:mm:ss");
                messageDbContext.Messages.Update(existingMessage);

                messageDbContext.SaveChanges();

                Assert.Single(messageDbContext.Messages);
            }
        }
    }

    [Fact]
    public void ShouldBeAbleToGetMessage()
    {
        using (var connection = new SqliteConnection("DataSource=:memory:"))
        {
            var options = new DbContextOptionsBuilder<MessageContext>().UseSqlite(connection).Options;

            using (var messageDbContext = new MessageContext(options))
            {
                messageDbContext.Database.EnsureCreated();

                var data = messageDbContext.Messages.ToList();

                Assert.Single(data);
            }
        }
    }

    [Fact]
    public void ShouldBeEvenTimestamp()
    {
        DateTime evenTime = new DateTime(2021, 6, 1, 18, 20, 22);
        Assert.True(TimeStampHandler.RegulateEvenTimeStamp(evenTime));

    }
    [Fact]
    public void ShouldBeUnevenTimestamp()
    {
        DateTime unevenTime = new DateTime(2021, 6, 1, 18, 20, 23);

        Assert.False(TimeStampHandler.RegulateEvenTimeStamp(unevenTime));
    }
}

