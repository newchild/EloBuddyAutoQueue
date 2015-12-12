using System;

namespace PVPNetConnect.RiotObjects.Platform.Matchmaking
{
    public class QueueInfo : RiotGamesObject
    {
        public override string TypeName
        {
            get { return this.type; }
        }

        private string type = "com.riotgames.platform.matchmaking.QueueInfo";

        public QueueInfo()
        {
        }

        public QueueInfo(Callback callback)
        {
            this.callback = callback;
        }

        public QueueInfo(TypedObject result)
        {
            base.SetFields(this, result);
        }

        public delegate void Callback(QueueInfo result);

        private Callback callback;

        public override void DoCallback(TypedObject result)
        {
            base.SetFields(this, result);
            callback(this);
        }

        [InternalName("waitTime")]
        public double WaitTime { get; set; }

        [InternalName("queueId")]
        public double QueueId { get; set; }

        [InternalName("queueLength")]
        public int QueueLength { get; set; }
    }
}