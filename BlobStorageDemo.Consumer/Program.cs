using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using System.Text;


var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: false);
IConfiguration config = builder.Build();

//string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
var connectionString = config.GetSection("AzureStorage")["ConnectionString"];
var queue = new QueueClient(connectionString, "attendee-emails");

if (await queue.ExistsAsync())
{
    QueueProperties properties = await queue.GetPropertiesAsync();
    for (int i = 0; i < properties.ApproximateMessagesCount; i++)
    {
        string value = await RetrieveNextMessageAsync();
        Console.WriteLine($"Received: {value}");

        // Sending Email
        // Storing in Database
    }
}

async Task<string> RetrieveNextMessageAsync()
{
    QueueMessage[] retrievedMessage = await queue.ReceiveMessagesAsync(1);
    var data = Convert.FromBase64String(retrievedMessage[0].Body.ToString());
    string theMessage = Encoding.UTF8.GetString(data);

    await queue.DeleteMessageAsync(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);

    return theMessage;
}
