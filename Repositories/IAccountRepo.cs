using BO;

namespace Repositories
{
    public interface IAccountRepo
    {
        public SystemAccount GetAccount(string email, string password);

        //interface for list of accounts
        public List<SystemAccount> GetAccounts();

        //interface for add account
        public void AddAccount(SystemAccount account);

        //interface for delete account

        public void DeleteAccount(int id);

        //interface for update account
        public void UpdateAccount(SystemAccount account);

        //interface for find account by id
        public SystemAccount FindAccountById(int id);


    }
}
