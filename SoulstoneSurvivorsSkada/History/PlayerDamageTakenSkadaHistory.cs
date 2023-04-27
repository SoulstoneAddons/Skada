using Il2Cpp;
using Il2CppSystem.Collections.Generic;

namespace SoulstoneSurvivorsSkada;

public class PlayerDamageTakenSkadaHistory
{
	private static List<DamageLog> _damageTaken =>
		GameManagerUtil.GameStats.DamageTakenLogs;
	
	private static readonly System.Collections.Generic.Dictionary<string, DamageLog> _damageTakenDictionary = new();

	public static float TotalDamageTaken
	{
		get
		{
			float totalDamageTaken = 0;
			foreach (DamageLog damageLog in _damageTaken)
			{
				totalDamageTaken += damageLog.DamageValue;
			}

			return totalDamageTaken;
		}
	}

	//TODO fix this - it's not working
	public static System.Collections.Generic.Dictionary<string, DamageLog> DamageTaken
	{
		get
		{
			_damageTakenDictionary.Clear();
			
			foreach (DamageLog damageLog in _damageTaken)
			{
				_damageTakenDictionary[damageLog.DamageSourceNameKey] = damageLog;
			}

			return _damageTakenDictionary;
		}
	}
}