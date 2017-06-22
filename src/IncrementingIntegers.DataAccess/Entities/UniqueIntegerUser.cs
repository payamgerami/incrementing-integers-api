using Microsoft.WindowsAzure.Storage.Table;

namespace IncrementingIntegers.DataAccess.Entities
{
    public class UniqueIntegerUser : TableEntity
    {
        public string Email { get; set; }
        public int Id { get; set; }

        public UniqueIntegerUser(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public UniqueIntegerUser()
        {
        }
    }
}
