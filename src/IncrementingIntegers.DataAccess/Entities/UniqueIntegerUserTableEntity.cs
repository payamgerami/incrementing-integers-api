using Microsoft.WindowsAzure.Storage.Table;

namespace IncrementingIntegers.DataAccess.Entities
{
    public class UniqueIntegerUserTableEntity : TableEntity
    {
        public string Email { get; set; }
        public int Id { get; set; }

        public UniqueIntegerUserTableEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public UniqueIntegerUserTableEntity()
        {
        }
    }
}
