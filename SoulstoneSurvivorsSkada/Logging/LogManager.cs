using System.Runtime.CompilerServices;
using BepInEx.Logging;

namespace SoulstoneSurvivorsSkada.Logging;

/// <summary>
/// Log Manager for logging
/// </summary>
internal static class LogManager
{
	// Logger for logging
	private static ManualLogSource Logger { get; set; }
	
	/// <summary>
	/// Set the logger for logging
	/// </summary>
	/// <param name="logger">logger to use</param>
	public static void SetLogger(ManualLogSource logger)
	{
		Logger = logger;
	}
	
	/// <summary>
	/// Log a message
	/// </summary>
	/// <param name="level">Log level</param>
	/// <param name="message">Message</param>
	/// <param name="name">Caller name</param>
	public static void Log(LogLevel level, string message, [CallerMemberName] string name = "")
	{
		// get class name
		string log = $"[{name}] {message}";
		Logger.Log(level, log);
	}
}