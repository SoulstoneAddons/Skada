using Il2Cpp;
using SoulstoneSurvivorsSkada.Extensions;
using SoulstoneSurvivorsSkada.Mapper;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Gui;

public static class SkadaBar
{
		
	private static readonly GUIStyle BarStyle = new(GUI.skin.box)
	{
		normal =
		{
			background = TextureUtility.BarTexture
		},
		overflow = new RectOffset(0,0,0,0),
		fixedHeight = ResUtility.GetHeight(30)
	};

	private static readonly GUIStyle iconStyle = new()
	{
		overflow = new RectOffset(0, 0, 0, 0),
		fixedHeight = ResUtility.GetHeight(30),
		fixedWidth = ResUtility.GetWidth(30),
	};

	private static readonly GUIStyle NameLabelStyle = new(GUI.skin.label)
	{
		alignment = TextAnchor.MiddleLeft
	};

	private static readonly GUIStyle DamageLabelStyle = new(GUI.skin.label)
	{
		alignment = TextAnchor.MiddleRight,
	};
	
	public static void RenderBarPercent(ref Rect windowRect, int index, GameStatsSkillData skillData, float totalDamage)
	{
		// get the damage value and make it positive
		float damage = Mathf.Abs(skillData.FloatValue);
		// calculate the percentage of the damage
		float percent = skillData.GetPercent(totalDamage);
		
		// get the position of the GUI
		Rect position = GUILayoutUtility.GetRect(0, ResUtility.GetHeight(30));

		BarStyle.fixedHeight = ResUtility.GetHeight(30);
		
		float textureWidth = ResUtility.GetWidth(30);

		var barRect = new Rect(textureWidth, 
			position.y,
			windowRect.width * percent - textureWidth - 5, 
			position.height);
		
		// draw the percentage bar
		GUI.Box(barRect, 
			GUIContent.none, BarStyle);

		string name = $"{index + 1}. {skillData.SkillName}";
		string text = $"{damage.ToHumanReadableString()} ({percent:P2})";

		// set font size
		GUI.skin.label.fontSize = ResUtility.GetFontSize(15);
		
		
		if (IconMap.Icons.TryGetValue(skillData.SkillName.ToLower(), out Texture2D icon))
		{
			var textureRect = new Rect(0, position.y,
				textureWidth,
				ResUtility.GetHeight(30));
			
			// uniform texture size 30x30
			GUI.Box(textureRect, 
				icon, 
				iconStyle);
		}

		// write the spell name, damage, and percentage to the GUI
		// allow overflow to the next line
		
		// label on the left side of the bar
		
		// set font size
		GUI.skin.label.fontSize = ResUtility.GetFontSize(12);
		GUI.Label(new Rect(textureWidth + 5, 
				position.y, 
				windowRect.width - (textureWidth - 5) / 2f, 
				position.height),
			name, NameLabelStyle);
		
		// set font size
		GUI.skin.label.fontSize = ResUtility.GetFontSize(14);
		GUI.Label(new Rect(textureWidth + 5 + (windowRect.width - (textureWidth - 5)) / 2f, 
				position.y,
				(windowRect.width - (textureWidth - 5)) / 2f - 15f,
				position.height),
			text, DamageLabelStyle);
		
		// Add some space between the bars
		GUILayout.Space(ResUtility.GetHeight(5));
	}
	
	public static void RenderBarDPS(ref Rect windowRect,
		int index,
		GameStatsSkillData skillData,
		float totalDPS, 
		float time)
	{
		// get the damage value and make it positive
		float damage = Mathf.Abs(skillData.FloatValue);
		float duration = SkadaTime.SpellStartTime[skillData.SkillNameHash];
		float dps = damage / (time - duration);
		
		// calculate the percentage of the damage
		float percent = dps / totalDPS;
		
		// get the position of the GUI
		Rect position = GUILayoutUtility.GetRect(0, ResUtility.GetHeight(30));

		BarStyle.fixedHeight = ResUtility.GetHeight(30);
		
		float textureWidth = ResUtility.GetWidth(30);

		var barRect = new Rect(textureWidth, 
			position.y,
			windowRect.width * percent - textureWidth - 5, 
			position.height);
		
		// draw the percentage bar
		GUI.Box(barRect, 
			GUIContent.none, BarStyle);

		string name = $"{index + 1}. {skillData.SkillName}";
		string text = $"{dps.ToHumanReadableString()} ({percent:P2})";

		// set font size
		GUI.skin.label.fontSize = ResUtility.GetFontSize(15);
		
		
		if (IconMap.Icons.TryGetValue(skillData.SkillName.ToLower(), out Texture2D icon))
		{
			var textureRect = new Rect(0, position.y,
				textureWidth,
				ResUtility.GetHeight(30));
			
			// uniform texture size 30x30
			GUI.Box(textureRect, 
				icon, 
				iconStyle);
		}

		// write the spell name, damage, and percentage to the GUI
		// allow overflow to the next line
		
		// label on the left side of the bar
		
		// set font size
		GUI.skin.label.fontSize = ResUtility.GetFontSize(12);
		GUI.Label(new Rect(textureWidth + 5, 
				position.y, 
				windowRect.width - (textureWidth - 5) / 2f, 
				position.height),
			name, NameLabelStyle);
		
		// set font size
		GUI.skin.label.fontSize = ResUtility.GetFontSize(14);
		GUI.Label(new Rect(textureWidth + 5 + (windowRect.width - (textureWidth - 5)) / 2f, 
				position.y,
				(windowRect.width - (textureWidth - 5)) / 2f - 15f,
				position.height),
			text, DamageLabelStyle);
		
		// Add some space between the bars
		GUILayout.Space(ResUtility.GetHeight(5));
	}
	
	public static void RenderBarPercent(ref Rect windowRect, int index, DamageLog log, float totalDamage)
	{
		// get the damage value and make it positive
		float damage = Mathf.Abs(log.DamageValue);
		// // calculate the percentage of the damage
		// float percent = log.(totalDamage);
		
		// get the position of the GUI
		Rect position = GUILayoutUtility.GetRect(0, ResUtility.GetHeight(30));

		BarStyle.fixedHeight = ResUtility.GetHeight(30);
		
		float textureWidth = ResUtility.GetWidth(30);

		var barRect = new Rect(textureWidth, 
			position.y,
			windowRect.width - textureWidth - 5, 
			position.height);
		
		// draw the percentage bar
		GUI.Box(barRect, 
			GUIContent.none, BarStyle);

		string name = $"{index + 1}. {log.DamageSourceNameKey}";
		string text = $"{damage.ToHumanReadableString()} (NaN%)";

		// set font size
		GUI.skin.label.fontSize = ResUtility.GetFontSize(15);
		
		
		if (IconMap.Icons.TryGetValue(log.DamageSourceNameKey.ToLower(), out Texture2D icon))
		{
			var textureRect = new Rect(0, position.y,
				textureWidth,
				ResUtility.GetHeight(30));
			
			// uniform texture size 30x30
			GUI.Box(textureRect, 
				icon, 
				iconStyle);
		}

		// write the spell name, damage, and percentage to the GUI
		// allow overflow to the next line
		
		// label on the left side of the bar
		
		// set font size
		GUI.skin.label.fontSize = ResUtility.GetFontSize(12);
		GUI.Label(new Rect(textureWidth + 5, 
				position.y, 
				windowRect.width - (textureWidth - 5) / 2f, 
				position.height),
			name, NameLabelStyle);
		
		// set font size
		GUI.skin.label.fontSize = ResUtility.GetFontSize(14);
		GUI.Label(new Rect(textureWidth + 5 + (windowRect.width - (textureWidth - 5)) / 2f, 
				position.y,
				(windowRect.width - (textureWidth - 5)) / 2f - 15f,
				position.height),
			text, DamageLabelStyle);
		
		// Add some space between the bars
		GUILayout.Space(ResUtility.GetHeight(5));
	}
}