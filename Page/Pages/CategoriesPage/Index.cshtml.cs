using BO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
namespace Page.Pages.CategoriesPage
{
    public class IndexModel : PageModel
    {
        //create a private field ICategoryRepo
        private readonly ICategoryRepo _categoryRepo;
        //add props ICategoryRepo 
        public IndexModel(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        public IList<Category> Category { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Category = _categoryRepo.GetCategories();
        }
    }
}
