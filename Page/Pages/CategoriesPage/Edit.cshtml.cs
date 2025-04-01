using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories;
namespace Page.Pages.CategoriesPage
{
    public class EditModel : PageModel
    {
        //create a private field ICategoryRepo
        private readonly ICategoryRepo _categoryRepo;

        //add props ICategoryRepo and remove _context from the constructor
        public EditModel(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepo.GetCategory((short)id);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
            ViewData["ParentCategoryId"] = new SelectList(_categoryRepo.GetCategories(true), "CategoryId", "CategoryDesciption");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ParentCategoryId"] = new SelectList(_categoryRepo.GetCategories(true), "CategoryId", "CategoryDesciption");

                return Page();
            }

            //_context.Attach(Category).State = EntityState.Modified;

            try
            {
                _categoryRepo.UpdateCategory(Category);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoryExists(short id)
        => _categoryRepo.GetCategory(id) != null;
    }
}
