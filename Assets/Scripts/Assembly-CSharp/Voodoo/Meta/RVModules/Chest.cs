using UnityEngine;
using UnityEngine.UI;

namespace Voodoo.Meta.RVModules
{
	public class Chest : MonoBehaviour
	{
		public bool isOpen;

		public ChestVisual chestVisual;

		public GameObject chestHidderBig;

		public GameObject chestHidderText;

		private ChestHidder _chestHidder;

		private Button _button;

		private ChestRoomView _chestRoomView;

		private RewardInfo _content;

		public void SetUp(float delay, ChestRoomView chestRoomView)
		{
		}

		private void OnClick()
		{
		}

		public void OpenChest()
		{
		}

		public void SetUpRewardVisual(RewardInfo parameters)
		{
		}

		public void ChestOpened()
		{
		}

		public void ShowContent(RewardInfo parameters)
		{
		}

		public void Clean()
		{
		}
	}
}
