using UnityEngine;
using UnityEngine.UI;

public class AirplaneCard : MonoBehaviour
{
    public int cardIndex;
    public Button buyOrChooseButton;
    public GameObject priceObject;
    public GameObject chooseObject;
    public GameObject chosenObject;
    [SerializeField] private SettingsManager _settingsManager;

    private ShopManager shopManager;

    private void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();

        buyOrChooseButton.onClick.AddListener(OnBuyOrChooseClick);
    }

    private void OnBuyOrChooseClick()
    {
        if (PlayerPrefs.GetInt($"Airplane_{cardIndex}_Purchased", 0) == 1)
        {
            _settingsManager.PlayClickSound();
            shopManager.SetAllCardsToChooseState();
            shopManager.SaveChosenAirplane(cardIndex);
            SetState(false, false, true);
        }
        else
        {
            if (shopManager.totalCoins >= ShopManager.airplanePrice)
            {
                shopManager.totalCoins -= ShopManager.airplanePrice;
                shopManager.UpdateCoinsUI();
                _settingsManager.PlayCashSound();
                PlayerPrefs.SetInt($"Airplane_{cardIndex}_Purchased", 1);
                PlayerPrefs.SetInt("TotalCoins", shopManager.totalCoins);
                PlayerPrefs.Save();

                shopManager.SetAllCardsToChooseState();
                shopManager.SaveChosenAirplane(cardIndex);
                SetState(false, false, true);

                var airplaneSkills = GetComponent<AirplaneSkills>();
                if (airplaneSkills != null)
                {
                    airplaneSkills.UpdateUpgradeButtons();
                }
            }
            else
            {
                _settingsManager.PlayDeclibeSound();
            }
        }
    }

    public void SetState(bool showPrice, bool showChoose, bool showChosen)
    {
        priceObject.SetActive(showPrice);
        chooseObject.SetActive(showChoose);
        chosenObject.SetActive(showChosen);
    }
}