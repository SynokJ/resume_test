public class ShopUIManagement
{
    private UnityEngine.GameObject _selectButton;
    private UnityEngine.GameObject _purchaseButton;

    private TMPro.TextMeshProUGUI _prevTitle;
    private UnityEngine.UI.Image _prevbImg;

    public ShopUIManagement(
        UnityEngine.GameObject select, 
        UnityEngine.GameObject purchase,
        TMPro.TextMeshProUGUI title,
        UnityEngine.UI.Image prevImg)
    {
        _selectButton = select;
        _purchaseButton = purchase;
        _prevTitle = title;
        _prevbImg = prevImg;
    }

    ~ShopUIManagement()
    {
    }

    public void ManageActiveButtons(ShopItemSO shopItem)
    {
        if (shopItem.isBought)
        {
            _selectButton.SetActive(true);
            _purchaseButton.SetActive(false);
        }
        else
        {
            _selectButton.SetActive(false);
            _purchaseButton.SetActive(true);
        }
    }

    public void SetShopItemInfo(ShopItemSO shopItem)
    {
        _prevbImg.sprite = shopItem.previewSprite;
        _prevTitle.text = shopItem.itemName;
    }
}
