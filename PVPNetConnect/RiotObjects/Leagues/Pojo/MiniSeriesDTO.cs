using System;

namespace PVPNetConnect.RiotObjects.Leagues.Pojo
{
    public class MiniSeriesDTO : RiotGamesObject
    {
        public override string TypeName
        {
            get { return this.type; }
        }

        private string type = "com.riotgames.leagues.pojo.MiniSeriesDTO";

        public MiniSeriesDTO()
        {
        }

        public MiniSeriesDTO(Callback callback)
        {
            this.callback = callback;
        }

        public MiniSeriesDTO(TypedObject result)
        {
            base.SetFields(this, result);
        }

        public delegate void Callback(MiniSeriesDTO result);

        private Callback callback;

        public override void DoCallback(TypedObject result)
        {
            base.SetFields(this, result);
            callback(this);
        }

        [InternalName("progress")]
        public string Progress { get; set; }

        [InternalName("target")]
        public int Target { get; set; }

        [InternalName("losses")]
        public int Losses { get; set; }

        [InternalName("timeLeftToPlayMillis")]
        public double TimeLeftToPlayMillis { get; set; }

        [InternalName("wins")]
        public int Wins { get; set; }
    }
}