using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voodoo.GameOps
{
	public class InventoryManager : MonoBehaviour
	{
		public static InventoryManager Instance;

		public List<Item> AllItems;

		public Action OnSkinChanged;

		public Action OnItemPurchased;

		private void Awake()
		{
		}

		internal void ResetToDefault(ItemType _type)
		{
		}

		public Item GetRandomItem()
		{
			return null;
		}

		public Item GetRandomItemOfType(ItemType _type)
		{
			return null;
		}

		public Item _GetRandomItem(List<Item> _items)
		{
			return null;
		}

		public List<Item> GetAllOfType(ItemType _type)
		{
			return null;
		}

		public void UnlockItem(Item item)
		{
		}

		public int GetInventoryCount(Item item)
		{
			return 0;
		}

		public bool IsUnlocked(Item item)
		{
			return false;
		}

		internal Item CurrentlySelected(ItemType category)
		{
			return null;
		}

		internal int GetPrice(Item item)
		{
			return 0;
		}

		internal void SelectItem(Item collectible)
		{
		}

		internal Item GetDefault(ItemType _type)
		{
			return null;
		}

		internal Item NextItemToUnlock()
		{
			return null;
		}

		internal Item NextItemToUnlock(ItemType _type)
		{
			return null;
		}

		private Item _NextUnlockItem(List<Item> _items)
		{
			return null;
		}

		internal List<Item> AllItemsAvailableOfType(ItemType _type)
		{
			return null;
		}

		internal List<Item> AllIUnlockedItems()
		{
			return null;
		}

		internal List<Item> AllLockedItems()
		{
			return null;
		}

		internal bool ItemsAvailableToUnlock()
		{
			return false;
		}

		internal bool ItemsAvailable(ItemType _type)
		{
			return false;
		}

		internal Item GetItem(string ID)
		{
			return null;
		}

		internal bool HasInventory(Item item)
		{
			return false;
		}

		public int RvCountToUnlockItem(Item item)
		{
			return 0;
		}

		public void RvForUnlockItemShown(Item item)
		{
		}

		public float CurrentBalance()
		{
			return 0f;
		}

		public int CurrentGemsBalance()
		{
			return 0;
		}

		public void SpendMoney(float amount)
		{
		}

		public void AddMoney(float amount)
		{
		}

		public void SpendGems(int amount)
		{
		}

		public void AddGems(int amount)
		{
		}
	}
}
