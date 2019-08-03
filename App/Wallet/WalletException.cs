using System;

namespace together_aspcore.App.Wallet
{
    public class WalletException : Exception
    {
        public WalletErrorCode ErrorCode { get; set; }

        public WalletException(WalletErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}