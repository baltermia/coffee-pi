using CoffeePi.Shared.DataTransferObjects;

namespace CoffeePi.Core.Repositories
{
    public interface IRepositoryBase<T> where T : IDataTransferObject
    {
        public IEnumerable<T> FindAll();

        public T FindById(int id);

        public Task<T> CreateAsync(T dto);

        public Task UpdateAsync(T dto);
    }
}
