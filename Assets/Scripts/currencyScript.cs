using UnityEngine;
using System.Collections;

public class currencyScript : MonoBehaviour {

    public int currency;
    
	// Use this for initialization
	void Start () {

        PlayerPrefs.GetInt("currency", currency);
              
	}
	
	// Update is called once per frame
	void Update () {
        if(currency < 0)
        {
            currency = 0;
        }
        
        PlayerPrefs.SetInt("currency", currency);
        DontDestroyOnLoad(this.gameObject);
        Debug.Log(currency);
    }

    public void addCurrency(int amount)
    {
        currency += amount;
    }
    public void removeCurrency(int amount)
    {
        currency -= amount;
    }



}
