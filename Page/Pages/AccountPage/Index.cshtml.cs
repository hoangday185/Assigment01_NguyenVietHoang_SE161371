using BO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
namespace Page.Pages.AccountPage
{
    public class IndexModel : PageModel
    {
        //create private IAccountRepo 
        private readonly IAccountRepo _accountRepo;

        //create constructor with IAccountRepo and remove DAO.FunewsManagementContext context

        public IndexModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public IList<SystemAccount> SystemAccount { get; set; } = default!;

        public async Task OnGetAsync()
        {
            SystemAccount = _accountRepo.GetAccounts();
        }
    }
}
