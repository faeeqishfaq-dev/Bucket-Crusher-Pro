using UnityEngine;

public static class EmptySprite
{
	private static Sprite instance;

	public static Sprite Get()
	{
		return null;
	}

	public static bool IsEmptySprite(Sprite s)
	{
		return false;
	}
}
