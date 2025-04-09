using EventManagment.Infrastructure.Repositories;
using EventManagment.Models;
using EventManagment.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    // Récupérer toutes les catégories avec leurs événements associés
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllWithEventsAsync();
    }

    // Récupérer une catégorie par son ID avec ses événements associés
    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _categoryRepository.GetCategoryWithEventsAsync(id);
    }

    // Ajouter une nouvelle catégorie
    public async Task AddCategoryAsync(Category category)
    {
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveAsync();
    }

    // Mettre à jour une catégorie
    public async Task UpdateCategoryAsync(Category category)
    {
        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveAsync();
    }

    // Supprimer une catégorie
    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category != null)
        {
            await _categoryRepository.DeleteAsync(category);
        }
    }
}
