using System.Collections.Generic;
using UnityEngine;

namespace Voodoo.Meta.RVModules
{
	public class ChestArea : MonoBehaviour
	{
		public Chest chest;

		public GameObject chestGrid;

		private readonly List<Chest> chests;

		public bool IsInitialized => false;

		public void FillGrid(int numberOfChests, ChestRoomView chestRoomView)
		{
		}

		public bool IsChestAvailable()
		{
			return false;
		}

		public void Clean()
		{
		}
	}
}
