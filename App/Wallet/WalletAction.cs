using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using together_aspcore.Shared.JsonConverter;

namespace together_aspcore.App.Wallet
{
    public class WalletAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int WalletId { get; set; }

        [JsonConverter(typeof(DateTimeJsonFormat))]
        public DateTime Time { get; set; }

        public double Amount { get; set; }
        public WalletActionType WalletActionType { get; set; }
        public double NewAmount { get; set; }
        public string Receiver { get; set; }
        public int ReferencePerson { get; set; }
    }

    public enum WalletActionType
    {
        DEPOSIT = 1,
        WITHDRAW = 2
    }
}