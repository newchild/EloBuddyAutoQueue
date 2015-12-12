using System;

namespace PVPNetConnect.RiotObjects.Platform.Game
{
    public class PlayerChampionSelectionDTO : RiotGamesObject
    {
        public override string TypeName
        {
            get { return this.type; }
        }

        private string type = "com.riotgames.platform.game.PlayerChampionSelectionDTO";

        public PlayerChampionSelectionDTO()
        {
        }

        public PlayerChampionSelectionDTO(Callback callback)
        {
            this.callback = callback;
        }

        public PlayerChampionSelectionDTO(TypedObject result)
        {
            base.SetFields(this, result);
        }

        public delegate void Callback(PlayerChampionSelectionDTO result);

        private Callback callback;

        public override void DoCallback(TypedObject result)
        {
            base.SetFields(this, result);
            callback(this);
        }

        [InternalName("summonerInternalName")]
        public string SummonerInternalName { get; set; }

        [InternalName("spell2Id")]
        public double Spell2Id { get; set; }

        [InternalName("selectedSkinIndex")]
        public int SelectedSkinIndex { get; set; }

        [InternalName("championId")]
        public int ChampionId { get; set; }

        [InternalName("spell1Id")]
        public double Spell1Id { get; set; }
    }
}