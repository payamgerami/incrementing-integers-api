using IncrementingIntegers.DataAccess.Entities;
using System.Threading.Tasks;

namespace IncrementingIntegers.DataAccess.Repositories
{
    public interface IUniquIntegerRepository
    {
        Task<UniqueIntegerUser> GetOrCreate(string email);
        Task Update(UniqueIntegerUser uniqueIntegerUser);
    }
}
