using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;
namespace Page.Pages.TagPage
{
    public class EditModel : PageModel
    {
        //create private field ITagRepo 
        private readonly ITagRepo _tagRepo;

        //create constructor with ITagRepo parameter and remove FunewsManagementContext
        public EditModel(ITagRepo tagRepo)
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
            Tag = tag;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Tag).State = EntityState.Modified;

            try
            {
                _tagRepo.UpdateTag(Tag);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(Tag.TagId))
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

        private bool TagExists(int id)
        => _tagRepo.GetTagById(id) != null;
    }
}
