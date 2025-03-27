using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
namespace Page.Pages.TagPage
{
    public class DeleteModel : PageModel
    {
        private readonly DAO.FunewsManagementContext _context;
        //create private field ITagRepo
        private readonly ITagRepo _tagRepo;

        //create constructor with ITagRepo parameter and remove FunewsManagementContext
        public DeleteModel(ITagRepo tagRepo)
        {
            _tagRepo = tagRepo;
        }


        [BindProperty]
        public Tag Tag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagRepo.GetTagById(id.Value);

            if (tag == null)
            {
                return NotFound();
            }
            else
            {
                Tag = tag;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagRepo.GetTagById(id.Value);
            if (tag != null)
            {
                Tag = tag;
                _tagRepo.DeleteTag(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
