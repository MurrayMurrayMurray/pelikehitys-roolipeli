using UnityEngine;
using TMPro;


public class PlayerDataManager : MonoBehaviour
{
    public int coins = 0;
    public TextMeshProUGUI countText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = coins.ToString();

    }
    public void PlayerCoins()
    {
        coins++;
    }
}
