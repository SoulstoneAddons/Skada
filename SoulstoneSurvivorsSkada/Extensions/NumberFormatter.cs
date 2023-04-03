using UnityEngine;

namespace SoulstoneSurvivorsSkada.Extensions;

internal static class NumberFormatter
{
	// to human readable string K, M, B, T and so on
	// Thank you Copilot, I don't really want to bother with this :)
	public static string ToHumanReadableString(this float number)
	{
		if (number < 1000)
		{
			return number.ToString("F0");
		}
		int exp = (int) (Mathf.Log(number) / Mathf.Log(1000));
		return (number / Mathf.Pow(1000, exp)).ToString("F1") + GetSuffix(exp);
	}
	
	// GetSuffix for the human readable string
	private static string GetSuffix(int exp)
	{
		return exp switch
		{
			1  => "K",
			2  => "M",
			3  => "B",
			4  => "T",
			5  => "Qa",
			6  => "Qi",
			7  => "Sx",
			8  => "Sp",
			9  => "Oc",
			10 => "No",
			11 => "Dc",
			12 => "Ud",
			13 => "Dd",
			14 => "Td",
			15 => "Qad",
			16 => "Qid",
			17 => "Sxd",
			18 => "Spd",
			19 => "Ocd",
			20 => "Nod",
			21 => "Vg",
			22 => "Uvg",
			_  => "??"
		};
	}
}