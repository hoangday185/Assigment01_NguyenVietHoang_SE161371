using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
namespace Page.Pages.CategoriesPage
{
    public class DetailsModel : PageModel
    {
        //create a private field ICategoryRepo
        private readonly ICategoryRepo _categoryRepo;

        //add props ICategoryRepo and remove _context from the constructor
        public DetailsModel(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //use _categoryRepo.GetCategory((short)id) to get the category
            var category = _categoryRepo.GetCategory((short)id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }
    }
}
