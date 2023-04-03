using System;
using System.Linq;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.Collections.Generic;
using SoulstoneSurvivorsSkada.Extensions;
using SoulstoneSurvivorsSkada.Interfaces;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Views;

public sealed class PlayerDamageMeterSkadaView : ISkadaView
{
	public int ScrollPosition { get; set; }
	public string Title { get; set; } = "Skada - Player Damage";

	/// <summary>
	/// Style for the Bar
	/// </summary>
	private static readonly GUIStyle BarStyle = new(GUI.skin.box)
	{
		normal =
		{
			background = BarTexture
		},
		overflow = new RectOffset(0,0,0,0),
		fixedHeight = ResUtility.GetHeight(20)
	};

	private static readonly Texture2D Texture = BarTexture;
	
	/// <summary>
	/// Texture for the Bar
	/// </summary>
	private static Texture2D BarTexture
	{
		get
		{
			// create a 1x1 texture with red color
			Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
			// set the pixel to red
			texture.SetPixel(0, 0, Color.red);
			// apply the changes
			texture.Apply();
			// return the texture
			return texture;
		}
	}
	
	public void OnGUI(ref Rect windowRect, int windowID)
	{
		GUILayout.BeginVertical();
		{
			// save spells into a variable to avoid multiple calls
			float totalDamage = PlayerSkadaHistory.PlayerTotalDamage;
			
			Il2CppReferenceArray<GameStatsSkillData> spells = PlayerSkadaHistory.DamageBySpellsOrdered;
			
			GUILayout.Label($"DPS: {PlayerSkadaHistory.PlayerDps.ToHumanReadableString()}");
			
			// set start index to scroll position
			// run until the end of the list
			for (int i = ScrollPosition; i < spells.Count; i++)
			{
				// Get the spell data based on index
				GameStatsSkillData skillData = spells[i];
				// if the spell name hash is 0, skip it
				if (skillData.SkillNameHash == 0) continue;
				// get the damage value and make it positive
				float damage = Mathf.Abs(skillData.FloatValue);
				// calculate the percentage of the damage
				float percent = skillData.GetPercent(totalDamage);
				
				// get the position of the GUI
				Rect position = GUILayoutUtility.GetRect(0, ResUtility.GetHeight(20));

				BarStyle.fixedHeight = ResUtility.GetHeight(20);
				
				// draw the percentage bar
				GUI.Box(new Rect(0, position.y, windowRect.width * percent, position.height), 
					"", BarStyle);

				string text = $"{skillData.SkillName} - {damage.ToHumanReadableString()} - {percent:P2}";

				// set font size
				GUI.skin.label.fontSize = ResUtility.GetFontSize(12);
				
				// write the spell name, damage, and percentage to the GUI
				// allow overflow to the next line
				GUI.Label(new Rect(0, position.y, windowRect.width, position.height),
					text);
				
				// Add some space between the bars
				GUILayout.Space(ResUtility.GetHeight(5));
				
				// GUILayout.Label($"{skillData.SkillName} - {damage.ToHumanReadableString()} - {percent:P2}%");
				
				// // write the spell name and damage to the GUI
				// GUILayout.Label($"{skillData.SkillName} - {damage.ToHumanReadableString()} - {percent:P2}%");
			}
		}
		GUILayout.EndVertical();
	}
}