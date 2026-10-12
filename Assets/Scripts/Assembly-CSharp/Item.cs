using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Shop/Item")]
public class Item : ScriptableObject
{
	public string ID;

	public ItemType ItemType;

	public bool UnlockedByDefault;

	public string Name;

	public int Order;

	public int Price;

	public int RvPrice;

	public int Max;

	public Sprite Icon;

	public Sprite IconBonus;

	public GameObject Prefab;

	public Mesh Mesh;

	[Header("Price Formula")]
	public bool UseFormula;

	public int BasePrice;

	public int IncreaseRate;

	[Header("Gem Price Formula")]
	public int GemBasePrice;

	public int GemIncreaseRate;

	[Header("Skin Only Perks")]
	public int FuelPercent;

	public int PowerPercent;

	public int SizePercent;

	[Header("Skin Variables")]
	public GameObject HeadObject;

	public GameObject HeadBonusObject;

	public Material CylindarMaterial;

	public Material JointCenter;

	public Material JointOuter;

	public DroneHandler Drone;

	public Sprite DroneIcon;

	public int GetPrice(int level = 0)
	{
		return 0;
	}
}
