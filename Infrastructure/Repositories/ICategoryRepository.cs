using EventManagment.Models;

namespace EventManagment.Infrastructure.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithEventsAsync();
        Task<Category> GetCategoryWithEventsAsync(int id);
    }
}
