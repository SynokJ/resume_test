using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _prevTitle;
    [SerializeField] private UnityEngine.UI.Image _prevbImg;

    [SerializeField] private GameObject _selectButton;
    [SerializeField] private GameObject _purchaseButton;

    private ShopItemSwitcher _itemSwitcher;
    private ShopUIManagement _uiManagement;

    public event System.Action<ShopItemSO> OnShopItemUpdated = default;

    private void OnEnable()
    {
        OnShopItemUpdated += _uiManagement.ManageActiveButtons;
        OnShopItemUpdated += _uiManagement.SetShopItemInfo;
    }

    private void OnDisable()
    {
        OnShopItemUpdated -= _uiManagement.ManageActiveButtons;
        OnShopItemUpdated -= _uiManagement.SetShopItemInfo;
    }

    private void Awake()
    {
        _itemSwitcher = new ShopItemSwitcher();
        _uiManagement = new ShopUIManagement(_selectButton, _purchaseButton, _prevTitle, _prevbImg);
    }

    private void Start()
    {
        OnShopItemUpdated?.Invoke(_itemSwitcher.CurrentShopItem);
    }

    public void SwitchToNext()
    {
        _itemSwitcher.GetNextItem();
        OnShopItemUpdated?.Invoke(_itemSwitcher.CurrentShopItem);
    }

    public void SwitchToPrev()
    {
        _itemSwitcher.GetPrevItem();
        OnShopItemUpdated?.Invoke(_itemSwitcher.CurrentShopItem);
    }

    public void BuyShopItem()
    {
        Debug.Log("Item Is Bought");
        _itemSwitcher.CurrentShopItem.isBought = true;
        OnShopItemUpdated?.Invoke(_itemSwitcher.CurrentShopItem);
    }

    public void SelectShopItem()
    {
        Debug.Log("Item Is Selected");
        PlayerDataManager.testData = _itemSwitcher.CurrentShopItem;
    }
}
