using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
namespace Page.Pages.AccountPage
{
    public class DetailsModel : PageModel
    {
        //create private IAccountRepo
        private readonly IAccountRepo _accountRepo;

        //create constructor with IAccountRepo and remove DAO.FunewsManagementContext context
        public DetailsModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }


        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //using method FindAccountById from IAccountRepo
            var systemaccount = _accountRepo.FindAccountById(id.Value);
            if (systemaccount == null)
            {
                return NotFound();
            }
            else
            {
                SystemAccount = systemaccount;
            }
            return Page();
        }
    }
}
