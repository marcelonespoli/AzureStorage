using BlobStorageDemo.Models;
using System.Net.Mail;

namespace BlobStorageDemo.Services
{
    public interface IQueueService
    {
        Task SendMessage(EmailMessage emailMessage);
    }
}
