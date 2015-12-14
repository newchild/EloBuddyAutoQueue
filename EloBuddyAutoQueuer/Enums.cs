using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EloBuddyAutoQueuer
{
	public enum Status
	{
		LoggedIn,
		InQueue,
		InLoginQueue,
		Disconnected,
		InGame
	}

	public enum SummonerSpells
	{
		Ghost = 6,
		Heal,
		Ignite = 14,
		Barrier = 21
	}

	public enum Champ
	{
		Ashe = 22,
		Caitlyn = 51,
		Cassiopeia = 69,
		Ezreal = 81
	}
}
