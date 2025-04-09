using EventManagment.Infrastructure.Repositories;
using EventManagment.Models;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Méthode pour récupérer toutes les catégories avec leurs événements associés
    public async Task<IEnumerable<Category>> GetAllWithEventsAsync()
    {
        return await _context.Categories.Include(c => c.Events).ToListAsync();
    }

    // Méthode pour récupérer une catégorie spécifique avec ses événements
    public async Task<Category> GetCategoryWithEventsAsync(int id)
    {
        return await _context.Categories
                             .Include(c => c.Events)
                             .FirstOrDefaultAsync(c => c.Id == id);
    }
}
