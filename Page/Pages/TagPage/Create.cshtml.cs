using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
namespace Page.Pages.TagPage
{
    public class CreateModel : PageModel
    {
        //create private field ITagRepo
        private readonly ITagRepo _tagRepo;

        //create constructor with ITagRepo parameter and remove FunewsManagementContext
        public CreateModel(ITagRepo tagRepo)
        {
            _tagRepo = tagRepo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _tagRepo.CreateTag(Tag);

            return RedirectToPage("./Index");
        }
    }
}
