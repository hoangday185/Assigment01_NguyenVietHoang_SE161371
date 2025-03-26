using BO;
using DAO;
namespace Repositories
{
    public class AccountRepo : IAccountRepo
    {
        public void AddAccount(SystemAccount account)
        => AccountDAO.Instance.AddAccount(account);

        public void DeleteAccount(int id)
        {
            AccountDAO.Instance.DeleteAccount(id);
        }

        public SystemAccount FindAccountById(int id)
        => AccountDAO.Instance.FindAccountById(id);

        public SystemAccount GetAccount(string email, string password)
            => AccountDAO.Instance.Login(email, password);


        public List<SystemAccount> GetAccounts()
            => AccountDAO.Instance.SystemAccounts();


        public void UpdateAccount(SystemAccount account)
        {
            AccountDAO.Instance.UpdateAccount(account);
        }


    }
}
