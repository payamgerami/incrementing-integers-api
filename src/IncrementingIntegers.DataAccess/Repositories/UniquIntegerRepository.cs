using IncrementingIntegers.Common.Configurations;
using IncrementingIntegers.DataAccess.Entities;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace IncrementingIntegers.DataAccess.Repositories
{
    public class UniquIntegerRepository : IUniquIntegerRepository
    {
        const string _tableName = "UniqueIntegers";
        const string _partitionKey = "_Integers";
        private readonly StorageOptions _storageOpptions;
        private CloudTable _table;

        public UniquIntegerRepository(IOptions<StorageOptions> storageOpptions)
        {
            _storageOpptions = storageOpptions.Value;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageOpptions.ConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            _table = tableClient.GetTableReference(_tableName);
        }

        public async Task<UniqueIntegerUserTableEntity> GetOrCreate(string email)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<UniqueIntegerUserTableEntity>(_partitionKey, email);
            TableResult retrievedResult = await _table.ExecuteAsync(retrieveOperation);

            if (retrievedResult.Result != null)
            {
                return (UniqueIntegerUserTableEntity)retrievedResult.Result;
            }
            else
            {
                UniqueIntegerUserTableEntity user = new UniqueIntegerUserTableEntity(_partitionKey, email);
                TableOperation insertOperation = TableOperation.Insert(user);

                TableResult insertResult = await _table.ExecuteAsync(insertOperation);

                return (UniqueIntegerUserTableEntity)insertResult.Result;
            }
        }

        public async Task<TableResult> Update(UniqueIntegerUserTableEntity uniqueIntegerUser)
        {
            TableOperation updateOperation = TableOperation.Replace(uniqueIntegerUser);

            return await _table.ExecuteAsync(updateOperation);
        }
    }
}
