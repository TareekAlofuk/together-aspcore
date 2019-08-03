using System.Collections.Generic;
using System.Threading.Tasks;
using together_aspcore.App.Wallet;

namespace together_aspcore.Controllers
{
    public interface IWalletRepository
    {
        Task Deposit(int memberId , WalletAction walletAction);
        Task Withdraw(int memberId , WalletAction walletAction);
        Task<List<WalletAction>> Actions(int memberId);
    }
}