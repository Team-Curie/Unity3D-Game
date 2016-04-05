using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSelect : MonoBehaviour
{
    public GameObject[] shopItems;
    public Text itemText;
    public int itemIndex = 0;
    public int previousIndex;

    void Awake()
    {
        shopItems[itemIndex].SetActive(true);
        itemText.text = GetCurrentItemText();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextButtonPressed()
    {
        previousIndex = itemIndex;
        itemIndex++;

        CheckItemIndex(itemIndex);
        ActivateShopItem(itemIndex, previousIndex);
    }

    public void PreviousButtonPressed()
    {
        previousIndex = itemIndex;
        itemIndex--;

        CheckItemIndex(itemIndex);
        ActivateShopItem(itemIndex, previousIndex);
    }

    private void CheckItemIndex(int ind)
    {
        if (ind == shopItems.Length)
        {
            itemIndex = 0;
            previousIndex = shopItems.Length - 1;
        }

        if (ind < 0)
        {
            itemIndex = shopItems.Length - 1;
            previousIndex = 0;
        }
    }

    private void ActivateShopItem(int itemIndex, int previousIndex)
    {
        shopItems[itemIndex].SetActive(true);
        itemText.text = GetCurrentItemText();
        shopItems[previousIndex].SetActive(false);
    }

    private string GetCurrentItemText()
    {
        return string.Format(shopItems[itemIndex].name + Environment.NewLine + shopItems[itemIndex].GetComponent<Text>().text);
    }
}
