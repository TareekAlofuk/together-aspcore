using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using together_aspcore.App.Wallet;
using together_aspcore.Shared;

namespace together_aspcore.Controllers
{
    public class WalletRepository : IWalletRepository
    {
        private readonly TogetherDbContext _context;

        public WalletRepository(TogetherDbContext context)
        {
            _context = context;
        }

        public async Task Deposit(int memberId, WalletAction walletAction)
        {
            var wallet = await _context.Wallets.Where(x => x.MemberId == memberId).FirstOrDefaultAsync();
            if (wallet == null)
            {
                wallet = new Wallet
                {
                    MemberId = memberId,
                    Amount = 0.0
                };
                await _context.Wallets.AddAsync(wallet);
                await _context.SaveChangesAsync();
            }

            wallet.Amount = wallet.Amount + walletAction.Amount;
            walletAction.Time = DateTime.Now;
            walletAction.WalletId = wallet.Id;
            walletAction.NewAmount = wallet.Amount;
            walletAction.WalletActionType = WalletActionType.DEPOSIT;

            _context.Wallets.Update(wallet);
            await _context.WalletActions.AddAsync(walletAction);
            await _context.SaveChangesAsync();
        }

        public Task Withdraw()
        {
            return null;
        }

        public async Task<List<WalletAction>> Actions(int memberId)
        {
            var wallet = await _context.Wallets.Where(x => x.MemberId == memberId).FirstOrDefaultAsync();
            if (wallet == null)
                throw new Exception();
            return await _context.WalletActions.Where(x => x.WalletId == wallet.Id).ToListAsync();
        }
    }
}