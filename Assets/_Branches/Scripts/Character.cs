using UnityEngine;

public class Character : MonoBehaviour
{
    public int characterSequenceNumber;
    public string characterName;
    public int price;
    public bool isFree = false;

    public bool IsUnlocked
    {
        get
        {
            return (isFree || PlayerPrefs.GetInt(characterName, 0) == 1);
        }
    }

    void Awake()
    {
        characterName = characterName.ToUpper();
    }

    public bool Unlock()
    {
        if (IsUnlocked)
            return true;
            

        if (SgLib.CoinManager.Instance.Cakes >= price)
        {
            PlayerPrefs.SetInt(characterName, 1);
            PlayerPrefs.Save();

            int coins = PlayerPrefs.GetInt("CakeCount");
            coins -= price;
            PlayerPrefs.SetInt("CakeCount", coins);

            return true;
        }

        return false;
    }
}
