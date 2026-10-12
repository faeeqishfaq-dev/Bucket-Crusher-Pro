using System.Collections.Generic;
using Voodoo.Meta.RVModules;
using Voodoo.Pattern;

public class ChestRoomRewardsController : SingletonMB<ChestRoomRewardsController>
{
	public List<RewardInfo> DefaultRewardsItems;

	public List<RewardInfo> LengthRewardItems;

	public List<RewardInfo> GasRewardsItems;

	public List<RewardInfo> SizeRewardsItems;

	public List<RewardInfo> PowerRewardsItems;

	public List<RewardInfo> GetRewards()
	{
		return null;
	}
}
