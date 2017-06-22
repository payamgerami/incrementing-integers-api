using IncrementingIntegers.DataAccess.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace IncrementingIntegers.DataAccess.Repositories
{
    public interface IUniquIntegerRepository
    {
        Task<UniqueIntegerUserTableEntity> GetOrCreate(string email);
        Task<TableResult> Update(UniqueIntegerUserTableEntity uniqueIntegerUser);
    }
}
