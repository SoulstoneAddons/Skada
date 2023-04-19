using System.Runtime.CompilerServices;
using MelonLoader;

namespace SoulstoneSurvivorsSkada.Logging;

public enum LogLevel
{
	Debug,
	Info,
	Warning,
	Error,
	Fatal
}

/// <summary>
/// Log Manager for logging
/// </summary>
internal static class LogManager
{
	// Logger for logging
	private static MelonLogger.Instance Logger { get; set; }
	
	/// <summary>
	/// Set the logger for logging
	/// </summary>
	/// <param name="logger">logger to use</param>
	public static void SetLogger(MelonLogger.Instance logger)
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
		switch (level)
		{
			case LogLevel.Debug:
				Logger.Msg(log);
				break;
			case LogLevel.Info:
				Logger.Msg(log);
				break;
			case LogLevel.Warning:
				Logger.Warning(log);
				break;
			case LogLevel.Error:
				Logger.Error(log);
				break;
			case LogLevel.Fatal:
				Logger.BigError(log);
				break;
		}
	}
}