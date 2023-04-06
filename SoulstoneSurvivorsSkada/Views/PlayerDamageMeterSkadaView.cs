using System;
using System.Linq;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.Collections.Generic;
using JetBrains.Annotations;
using SoulstoneSurvivorsSkada.Extensions;
using SoulstoneSurvivorsSkada.Interfaces;
using SoulstoneSurvivorsSkada.Mapper;
using UnityEngine;

namespace SoulstoneSurvivorsSkada.Views;

public sealed class PlayerDamageMeterSkadaView : ISkadaView
{

	public int ScrollPosition { get; set; }
	public string Title { get; set; } = "Skada - Player Damage";
	
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

	public void OnEnable()
	{
		
	}

	public void OnDisable()
	{
		
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

				string name = $"{i + 1}. {skillData.SkillName}";
				string text = $"{damage.ToHumanReadableString()} ({percent:P2})";

				// set font size
				GUI.skin.label.fontSize = ResUtility.GetFontSize(15);
				
				
				if (IconMap.Icons.TryGetValue(skillData.SkillName, out Texture2D icon))
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
				
				// GUILayout.Label($"{skillData.SkillName} - {damage.ToHumanReadableString()} - {percent:P2}%");
				
				// // write the spell name and damage to the GUI
				// GUILayout.Label($"{skillData.SkillName} - {damage.ToHumanReadableString()} - {percent:P2}%");
			}
		}
		GUILayout.EndVertical();
	}
}