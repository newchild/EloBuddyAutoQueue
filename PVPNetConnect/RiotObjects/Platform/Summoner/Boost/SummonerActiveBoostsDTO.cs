using System;

namespace PVPNetConnect.RiotObjects.Platform.Summoner.Boost
{
    public class SummonerActiveBoostsDTO : RiotGamesObject
    {
        public override string TypeName
        {
            get { return this.type; }
        }

        private string type = "com.riotgames.platform.summoner.boost.SummonerActiveBoostsDTO";

        public SummonerActiveBoostsDTO()
        {
        }

        public SummonerActiveBoostsDTO(Callback callback)
        {
            this.callback = callback;
        }

        public SummonerActiveBoostsDTO(TypedObject result)
        {
            base.SetFields(this, result);
        }

        public delegate void Callback(SummonerActiveBoostsDTO result);

        private Callback callback;

        public override void DoCallback(TypedObject result)
        {
            base.SetFields(this, result);
            callback(this);
        }

        [InternalName("xpBoostEndDate")]
        public double XpBoostEndDate { get; set; }

        [InternalName("xpBoostPerWinCount")]
        public int XpBoostPerWinCount { get; set; }

        [InternalName("xpLoyaltyBoost")]
        public int XpLoyaltyBoost { get; set; }

        [InternalName("ipBoostPerWinCount")]
        public int IpBoostPerWinCount { get; set; }

        [InternalName("ipLoyaltyBoost")]
        public int IpLoyaltyBoost { get; set; }

        [InternalName("summonerId")]
        public double SummonerId { get; set; }

        [InternalName("ipBoostEndDate")]
        public double IpBoostEndDate { get; set; }
    }
}