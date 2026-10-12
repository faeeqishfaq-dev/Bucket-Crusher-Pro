using System.Collections.Generic;

namespace Voodoo.Utils
{
	public static class ExtensionMethodsList
	{
		public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
		{
		}

		public static void SwapRange<T>(this IList<T> list, (int start, int end) baseRange, (int start, int end) destRange)
		{
		}

		public static void SwapRange<T>(this List<T> list, (int start, int end) baseRange, (int start, int end) destRange)
		{
		}

		public static void Shuffle<T>(this IList<T> list)
		{
		}

		public static void Shift<T>(this IList<T> list, int offset)
		{
		}

		public static void Shift<T>(this List<T> list, int offset)
		{
		}

		public static T GetRandomValue<T>(this IList<T> list)
		{
			return default(T);
		}

		public static List<T> GetDistinctRandomValues<T>(this IList<T> source, int amount)
		{
			return null;
		}

		public static bool ContainsAll<T>(this IList<T> source, List<T> values)
		{
			return false;
		}

		public static string ToString<T>(this IList<T> list, bool showListContent)
		{
			return null;
		}
	}
}
