namespace SoulstoneSurvivorsSkada.Constants;

public static class WindowState
{
	public const int Main = 0x0;
	public const int Settings = 0x1;
	
	public static readonly System.Collections.Generic.Dictionary<int, bool> WindowStates = new()
	{
		{Main, true},
		{Settings, false}
	};
	
	public static void ToggleWindow(int window)
	{
		WindowStates[window] = !WindowStates[window];
	}
	
	public static bool IsWindowOpen(int window)
	{
		return WindowStates[window];
	}
	
	public static void OpenWindow(int window)
	{
		WindowStates[window] = true;
	}
	
	public static void CloseWindow(int window)
	{
		WindowStates[window] = false;
	}
	
	public static void ToggleAllWindows()
	{
		foreach (System.Collections.Generic.KeyValuePair<int, bool> windowState in WindowStates)
		{
			WindowStates[windowState.Key] = !windowState.Value;
		}
	}
	
	public static void OpenAllWindows()
	{
		foreach (System.Collections.Generic.KeyValuePair<int, bool> windowState in WindowStates)
		{
			WindowStates[windowState.Key] = true;
		}
	}
	
	public static void CloseAllWindows()
	{
		foreach (System.Collections.Generic.KeyValuePair<int, bool> windowState in WindowStates)
		{
			WindowStates[windowState.Key] = false;
		}
	}
}