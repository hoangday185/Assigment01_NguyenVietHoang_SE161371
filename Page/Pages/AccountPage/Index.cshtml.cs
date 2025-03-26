using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BO;
using DAO;

namespace Page.Pages.AccountPage
{
    public class IndexModel : PageModel
    {
        private readonly DAO.FunewsManagementContext _context;

        public IndexModel(DAO.FunewsManagementContext context)
        {
            _context = context;
        }

        public IList<SystemAccount> SystemAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SystemAccount = await _context.SystemAccounts.ToListAsync();
        }
    }
}
