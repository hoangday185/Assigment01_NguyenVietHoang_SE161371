using BO;
using DAO;
namespace Repositories
{
    public class AccountRepo : IAccountRepo
    {
        public SystemAccount GetAccount(string email, string password)
        {
            return AccountDAO.Instance.Login(email, password);
        }

        public List<SystemAccount> GetAccounts()
        {
            return AccountDAO.Instance.SystemAccounts();
        }
    }
}
