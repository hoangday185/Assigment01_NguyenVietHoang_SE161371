using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories;

namespace Page.Pages.CategoriesPage
{
    public class CreateModel : PageModel
    {
        //create ICategory Repo
        private readonly ICategoryRepo _categoryRepo;

        //add props ICategoryRepo and remove _context from the constructor
        public CreateModel(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        public IActionResult OnGet()
        {
            ViewData["ParentCategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryDesciption");
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ParentCategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryDesciption");

                return Page();
            }

            _categoryRepo.CreateCategory(Category);

            return RedirectToPage("./Index");
        }
    }
}
