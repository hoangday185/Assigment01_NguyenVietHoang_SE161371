using BO;
using Microsoft.EntityFrameworkCore;

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

        //add account
        public void AddAccount(SystemAccount account)
        {
            using var context = CreateDb();
            {
                //find account by id
                var existingAccount = FindAccountById(account.AccountId);
                //check if account exists
                if (existingAccount == null)
                {
                    context.SystemAccounts.Add(account);
                    context.SaveChanges();
                }
            }
        }


        //delete account
        public void DeleteAccount(int id)
        {
            using var context = CreateDb();
            {
                //use method find account by id
                var account = FindAccountById(id);
                //check if account exists
                if (account != null)
                {
                    context.SystemAccounts.Remove(account);
                    context.SaveChanges();
                }
            }
        }

        //update account
        public void UpdateAccount(SystemAccount account)
        {
            using var context = CreateDb();
            {
                //use method find account by id
                var existingAccount = FindAccountById(account.AccountId);
                //check if account exists
                if (existingAccount != null)
                {
                    context.SystemAccounts.Update(account);
                    context.SaveChanges();
                }
            }
        }

        //find account by id
        public SystemAccount FindAccountById(int accountId)
        {
            using var context = CreateDb();
            {
                return context.SystemAccounts.AsNoTracking().FirstOrDefault(m => m.AccountId == accountId);
            }
        }


        public bool CheckAccountAdmin(string email, string password)
        {
            using var context = CreateDb();
            {
                return CreateDb().GetAdminAccount(email, password);
            }
        }
    }
}
