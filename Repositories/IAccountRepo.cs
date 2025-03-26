using BO;

namespace Repositories
{
    public interface IAccountRepo
    {
        public SystemAccount GetAccount(string email, string password);


        //interface for list of accounts
        public List<SystemAccount> GetAccounts();
    }
}
