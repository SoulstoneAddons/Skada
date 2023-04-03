using UnityEngine;

namespace SoulstoneSurvivorsSkada.Extensions;

public static class GameStatsSkillDataExtension
{
	/// <summary>
	/// Get the Percent of the given value from 0 to 1
	/// </summary>
	/// <param name="data">Skill data</param>
	/// <param name="total">Total damage done</param>
	/// <returns>Percent between 0 and 1</returns>
	public static float GetPercent(this GameStatsSkillData data, in float total)
	{
		return Mathf.Abs(data.FloatValue) / total;
	}
}