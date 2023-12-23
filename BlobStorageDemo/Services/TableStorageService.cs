using Azure.Data.Tables;
using BlobStorageDemo.Data;

namespace BlobStorageDemo.Services
{
    public class TableStorageService : ITableStorageService
    {
        private const string tableName = "Attendees";
        private readonly TableServiceClient _tableServiceClient;
        private readonly TableClient _tableClient;

        public TableStorageService(
            IConfiguration configuration, 
            TableServiceClient tableServiceClient, 
            TableClient tableClient)
        {
            _tableServiceClient = tableServiceClient;
            _tableClient = tableClient;
        }

        public async Task<AttendeeEntity> GetAttendee(string industry, string id)
        {
            return await _tableClient.GetEntityAsync<AttendeeEntity>(industry, id);
        }

        public async Task<List<AttendeeEntity>> GetAttendees()
        {
            var attendeeEntities = _tableClient.Query<AttendeeEntity>();
            return attendeeEntities.ToList();
        }

        public async Task UpsertAttendee(AttendeeEntity attendeeEntity)
        {
            await _tableClient.UpsertEntityAsync(attendeeEntity);
        }

        public async Task DeleteAttendee(string industry, string id)
        {
            await _tableClient.DeleteEntityAsync(industry, id);
        }

        private async Task<TableClient> GetTableClient()
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }
    }
}
