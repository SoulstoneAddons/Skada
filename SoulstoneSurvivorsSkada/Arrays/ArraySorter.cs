using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace SoulstoneSurvivorsSkada.Arrays;

public static class ArraySorter
{
	public static void Sort<T>(ref Il2CppReferenceArray<T> array, System.Comparison<T> comparison) 
		where T : Il2CppObjectBase
	{
		for (int a = 0; a < array.Length - 1; a++)
		{
			for (int b = 0; b < array.Length - a - 1; b++)
			{
				if (comparison(array[b], array[b + 1]) > 0)
				{
					(array[b], array[b + 1]) = (array[b + 1], array[b]);
				}
			}
		}
	}
}