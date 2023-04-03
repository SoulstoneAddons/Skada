using System;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using SoulstoneSurvivorsSkada.Logging;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SoulstoneSurvivorsSkada;

// Required attributes for BepInEx to load the plugin
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Soulstone Survivors.exe")] // To make sure the plugin is loaded only in the game process
internal sealed class SkadaPlugin : BasePlugin
{
	/// <summary>
	/// Harmony instance for patching
	/// </summary>
	internal static Harmony Harmony { get; } = new(MyPluginInfo.PLUGIN_GUID);

	private Bootstrapper _bootstrapper;

	/// <summary>
	/// Logger for logging
	/// </summary>
	public SkadaPlugin()
	{
		LogManager.SetLogger(Log);
	}
	
	/// <summary>
	/// Called when the plugin is loaded
	/// </summary>
	public override void Load()
	{
		try
		{
			_bootstrapper = AddComponent<Bootstrapper>();
		}
		catch (Exception ex)
		{
			LogManager.Log(LogLevel.Error, 
				"FAILED to Register " + ex.Message + "");
		}
	}

	/// <summary>
	/// Called when the plugin is unloaded
	/// </summary>
	/// <returns></returns>
	public override bool Unload()
	{
		// Log that the plugin is unloaded
		LogManager.Log(LogLevel.Info, $"Plugin {MyPluginInfo.PLUGIN_GUID} is unloaded!");
		
		// Destroy the bootstrapper if it exists
		if (_bootstrapper != null)
		{
			Object.Destroy(_bootstrapper);
		}
		return true;
	}
}