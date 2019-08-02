using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using together_aspcore.App.Wallet;

namespace together_aspcore.Controllers
{
    [Route("api/wallet")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private IWalletRepository _walletRepository;

        public WalletController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        [HttpPost("deposit")]
        public async Task<ActionResult> Deposit([FromForm] WalletActionRequest requestInfo)
        {
            await _walletRepository.Deposit(requestInfo.MemberId, requestInfo.GetWalletAction());
            return Ok();
        }

        [HttpGet("report")]
        public async Task<ActionResult> Withdraw([FromQuery] int memberId)
        {
            var actions = await _walletRepository.Actions(memberId);
            return Ok(actions);
        }
    }

    public class WalletActionRequest
    {
        public double Amount { get; set; }
        public string Receiver { get; set; }
        public int MemberId { get; set; }


        public WalletAction GetWalletAction()
        {
            return new WalletAction
            {
                Amount = Amount,
                Receiver = Receiver
            };
        }
    }
}