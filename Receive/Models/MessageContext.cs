using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
public class MessageContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    public MessageContext(DbContextOptions options) : base(options) { }
    public MessageContext() { }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {

        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();

        options.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
    }
}

