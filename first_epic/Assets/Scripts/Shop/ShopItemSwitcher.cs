using System.Linq;
using UnityEngine;

public class ShopItemSwitcher
{
    private const string _SHOP_ITEMS_PATH = "ScriptableObjects/";

    public ShopItemSO CurrentShopItem { get => _shopItemSOs[_currentId]; }

    private ShopItemSO[] _shopItemSOs = null;
    private int _currentId = 0;

    public ShopItemSwitcher()
    {
        var tempItems = UnityEngine.Resources.LoadAll(_SHOP_ITEMS_PATH, typeof(ShopItemSO));
        _shopItemSOs = tempItems.Cast<ShopItemSO>().ToArray();
        PlayerData playerData = new PlayerData();
        playerData.nameData = "name";
        string[] res = new string[_shopItemSOs.Length];
        for (int i = 0; i < _shopItemSOs.Length; ++i)
            res[i] = _shopItemSOs[i].itemName;
        playerData.shopItemsNameData = res;
        UnityEngine.Debug.Log(JsonUtility.ToJson(playerData));
    }

    ~ShopItemSwitcher()
    {
        _shopItemSOs = null;
    }

    public void GetNextItem()
    {
        _currentId++;
        if (_currentId >= _shopItemSOs.Length)
            _currentId = 0;
    }

    public void GetPrevItem()
    {
        _currentId--;
        if (_currentId < 0)
            _currentId = _shopItemSOs.Length - 1;
    }


}

struct PlayerData
{
    public string nameData;
    public string[] shopItemsNameData;
}
