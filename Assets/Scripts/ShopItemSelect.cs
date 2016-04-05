using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopItemSelect : MonoBehaviour
{
    public GameObject[] shopItems;
    public Text itemText;
    public Text moneyText;
    public Text fuelText;
    public Text ammunitionText;
    public Text shieldText;
    public int itemIndex = 0;
    public int previousIndex;
    public float shipShield;
    public float shipFuel;
    public int clips;

  
    [SerializeField]
    private int shieldValue;
    [SerializeField]
    private int fuelValue;
    [SerializeField]
    private int money;
   
    
    void Awake()
    {
        shipShield = PlayerPrefs.GetFloat("shipShield");
        shipFuel = PlayerPrefs.GetFloat("shipFuel");
        clips = PlayerPrefs.GetInt("clips");
        money = PlayerPrefs.GetInt("Money");
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

        moneyText.text = money.ToString();
        shieldText.text = "Ship Armor: " + shipShield.ToString();
        fuelText.text = "Ship Fuel: " + shipFuel.ToString();
        ammunitionText.text = "Ammunition: " + clips.ToString();
        


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


    //public void upgradeHealth(int health)
    //{
    //    // playerprefs health + health    
    //}

    public void upgradeShield(int Shield)
    {
        shipShield += Shield;
    }

    public void upgradeFuel(int fuel)
    {
        shipFuel += fuel;
    }

    public void upgradeBullets(int clip)
    {
        clips += clip;
    }

    public void buyItem()
    {
        if(itemIndex == 0 && money >= 20 )
        {
            money -= 20;
            upgradeShield(20);
        }
       else  if (itemIndex == 1 && money >= 10)
        {
            money -= 10;
            upgradeFuel(100);
        }
      else   if (itemIndex == 2 && money >= 30)
        {
            money -= 30;
            upgradeBullets(1);
        }
        else
        {
            Debug.Log("Not Enough Crystals");
        }




    }

    public void exitScene()
    {

        PlayerPrefs.SetFloat("shipShield", shipShield);
        PlayerPrefs.SetFloat("shipFuel", shipFuel);
        PlayerPrefs.SetInt("clips", clips);
        PlayerPrefs.SetInt("Money", money);
        SceneManager.UnloadScene("Shop");
        SceneManager.LoadScene("main");


    }




}
