using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "ShopItems/ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public string itemName;
    public Sprite previewSprite;
    public bool isBought;
    public int prise;
    public string AnimatorPath;
    public string SpritePath;
}
