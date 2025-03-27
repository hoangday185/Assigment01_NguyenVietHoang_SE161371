using BO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
namespace Page.Pages.TagPage
{
    public class IndexModel : PageModel
    {
        //create private field ITagRepo 
        private readonly ITagRepo _tagRepo;

        //create constructor with ITagRepo parameter and remove FunewsManagementContext

        public IndexModel(ITagRepo tagRepo)
        {
            _tagRepo = tagRepo;
        }



        public IList<Tag> Tag { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Tag = _tagRepo.Tags();
        }
    }
}
