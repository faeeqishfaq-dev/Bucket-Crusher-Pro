using System;
using Voodoo.Pattern;

public class MessageCenter : SingletonMB<MessageCenter>
{
	public Action OnSettingsShow;

	public Action OnShopShow;

	public Action OnPermanentUpgrade;

	public Action OnCurrencyChanged;

	public Action OnNoAdsPurchase;

	public Action<EndScreenMode, Action, Action> ShowEndScreen;
}
