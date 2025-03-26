using BO;

namespace DAO
{
    public class AccountDAO
    {
        private readonly FunewsManagementContext _context;
        //do singleton with accountdao
        private static AccountDAO instance;

        private AccountDAO()
        {
            _context = new FunewsManagementContext();
        }
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        private FunewsManagementContext CreateDb() => new FunewsManagementContext();

        //login 
        public SystemAccount? Login(string email, string password)
        {
            using var context = CreateDb();
            {
                return context.SystemAccounts.SingleOrDefault(a => a.AccountEmail.Equals(email) && a.AccountPassword.Equals(password));

            }
        }

        public List<SystemAccount> SystemAccounts()
        {
            using var context = CreateDb();
            {
                return context.SystemAccounts.ToList();
            }

        }
    }
}
