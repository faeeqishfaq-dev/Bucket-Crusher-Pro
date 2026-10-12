using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Voodoo.Meta.RVModules
{
	public class ChestRoomKeysManager : MonoBehaviour
	{
		private const string LasCollectedKeyLevel = "Voodoo_Last_Collected_Key_Level";

		private const string TotalCollectedKey = "Voodoo_Total_Collected_Key";

		[SerializeField]
		private ChestRoomAdapter _chestRoomPanel;

		[SerializeField]
		private KeyCollectable _keyPrefab;

		public int keysNeededToOpen;

		private static ChestRoomKeysManager _instance;

		public int LevelsToWait => 0;

		private int LastCollectedKey
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		private int KeysCount
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int CurrentKeys => 0;

		public static ChestRoomKeysManager Instance => null;

		public event Action onKeyCollected
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		private void Awake()
		{
		}

		public bool ShouldSpawnKey(int currentLevel)
		{
			return false;
		}

		public bool HasEnoughKey()
		{
			return false;
		}

		public void ComputeKeyCollected(int currentLevel)
		{
		}

		public void TryOpenChestRoom(Action onClose)
		{
		}
	}
}
