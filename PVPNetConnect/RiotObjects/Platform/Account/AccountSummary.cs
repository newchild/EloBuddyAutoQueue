using System;

namespace PVPNetConnect.RiotObjects.Platform.Account
{
    public class AccountSummary : RiotGamesObject
    {
        public override string TypeName
        {
            get { return this.type; }
        }

        private string type = "com.riotgames.platform.account.AccountSummary";

        public AccountSummary()
        {
        }

        public AccountSummary(Callback callback)
        {
            this.callback = callback;
        }

        public AccountSummary(TypedObject result)
        {
            base.SetFields(this, result);
        }

        public delegate void Callback(AccountSummary result);

        private Callback callback;

        public override void DoCallback(TypedObject result)
        {
            base.SetFields(this, result);
            callback(this);
        }

        [InternalName("groupCount")]
        public int GroupCount { get; set; }

        [InternalName("username")]
        public string Username { get; set; }

        [InternalName("accountId")]
        public double AccountId { get; set; }

        [InternalName("summonerInternalName")]
        public object SummonerInternalName { get; set; }

        [InternalName("admin")]
        public bool Admin { get; set; }

        [InternalName("hasBetaAccess")]
        public bool HasBetaAccess { get; set; }

        [InternalName("summonerName")]
        public object SummonerName { get; set; }

        [InternalName("partnerMode")]
        public bool PartnerMode { get; set; }

        [InternalName("needsPasswordReset")]
        public bool NeedsPasswordReset { get; set; }
    }
}