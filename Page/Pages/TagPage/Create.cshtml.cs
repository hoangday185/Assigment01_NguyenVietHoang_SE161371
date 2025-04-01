using Assigment01_NguyenVietHoang_SE161371;
using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Repositories;
namespace Page.Pages.TagPage
{
    public class CreateModel : PageModel
    {
        //create private field ITagRepo
        private readonly ITagRepo _tagRepo;
        //create private filed HubContext 
        private readonly IHubContext<SignalR> _hubContext;

        //create constructor with ITagRepo parameter and remove FunewsManagementContext
        public CreateModel(ITagRepo tagRepo,
            //add IHubContext
            IHubContext<SignalR> hubContext
            )
        {
            //assign ITagRepo to _tagRepo
            _tagRepo = tagRepo;
            //assign IHubContext to _hubContext
            _hubContext = hubContext;
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
            await _hubContext.Clients.All.SendAsync("TagsUpdated");

            return RedirectToPage("./Index");
        }
    }
}
