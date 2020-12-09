using Grocery.Model.Entities;
using System.Threading.Tasks;

namespace Grocery.Data.Interfaces {
  public interface IRepository<T> where T : Entity {
    Task<T> GetByIdAsync(string id, string partitionkey);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
  }
}
