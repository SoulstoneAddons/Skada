using BepInEx.Logging;
using SoulstoneSurvivorsSkada.Logging;
using UnityEngine;

namespace SoulstoneSurvivorsSkada;

internal static class ScrollUtility
{
	/// <summary>
	/// Scrollable area for GUI because BeginScrollView is not working
	/// </summary>
	/// <param name="scrollableHeight">Total scrollable height</param>
	/// <param name="itemHeight">Height for each item</param>
	/// <param name="scrollPosition">position to be changed</param>
	/// <param name="delta">Delta of the scroll</param>
	public static bool Scroll(in float scrollableHeight, in float itemHeight, ref int scrollPosition, out float delta)
	{
		// check if event is not scroll wheel
		if (Event.current.type != EventType.ScrollWheel)
		{
			// set delta to default and return false
			delta = default;
			return false;
		}
		
		// calculate how many items are visible
		int availableItems = Mathf.RoundToInt(scrollableHeight / itemHeight / 2.0f);
		
		// get scroll delta
		delta = Event.current.delta.y;
		
		// check if scroll delta is positive and if there are more items to scroll
		switch (delta)
		{
			// scroll down when delta is positive and there are more items to scroll
			case > 0 when PlayerSkadaHistory.DamageBySpellsCount > scrollPosition + availableItems:
				LogManager.Log(LogLevel.Info, $"Scrolling down. ScrollPosition: {scrollPosition}, AvailableItems: {availableItems}");
				// scroll down
				scrollPosition += 1;
				return true; // return true to indicate that the scroll position has changed
			case < 0 when scrollPosition > 0: // scroll up when delta is negative and scroll position is not 0
				// scroll up
				scrollPosition -= 1;
				return true; // return true to indicate that the scroll position has changed
		}

		return false; // return false to indicate that the scroll position has not changed
	}
}