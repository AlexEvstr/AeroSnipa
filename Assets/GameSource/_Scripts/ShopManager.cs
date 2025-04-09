using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Text coinsText;
    public Button leftArrow;
    public Button rightArrow;
    public GameObject[] airplaneCards;

    public int totalCoins;
    private int currentCardIndex = 0;
    private int chosenCardIndex = 0;

    public const int airplanePrice = 1400;
    [SerializeField] private Image _planeImage;
    [SerializeField] private Sprite[] _spritesPlanes;

    private void Start()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinsUI();

        chosenCardIndex = PlayerPrefs.GetInt("ChosenAirplane", 0);

        InitializeCards();

        leftArrow.onClick.AddListener(OnLeftArrowClick);
        rightArrow.onClick.AddListener(OnRightArrowClick);

        UpdateCardDisplay();

        _planeImage.sprite = _spritesPlanes[chosenCardIndex];
    }

    private void InitializeCards()
    {
        for (int i = 0; i < airplaneCards.Length; i++)
        {
            var cardScript = airplaneCards[i].GetComponent<AirplaneCard>();

            if (i == 0 && PlayerPrefs.GetInt($"Airplane_{i}_Purchased", 0) == 0)
            {
                PlayerPrefs.SetInt($"Airplane_{i}_Purchased", 1);
            }

            if (i == chosenCardIndex)
            {
                cardScript.SetState(false, false, true);
            }
            else if (PlayerPrefs.GetInt($"Airplane_{i}_Purchased", 0) == 1)
            {
                cardScript.SetState(false, true, false);
            }
            else
            {
                cardScript.SetState(true, false, false);
            }
        }
    }

    public void UpdateCoinsUI()
    {
        coinsText.text = totalCoins.ToString();
    }

    public void OnLeftArrowClick()
    {
        currentCardIndex = (currentCardIndex == 0) ? airplaneCards.Length - 1 : currentCardIndex - 1;
        UpdateCardDisplay();
    }

    public void OnRightArrowClick()
    {
        currentCardIndex = (currentCardIndex == airplaneCards.Length - 1) ? 0 : currentCardIndex + 1;
        UpdateCardDisplay();
    }

    private void UpdateCardDisplay()
    {
        for (int i = 0; i < airplaneCards.Length; i++)
        {
            airplaneCards[i].SetActive(i == currentCardIndex);
        }
    }

    public void SetAllCardsToChooseState()
    {
        foreach (var card in airplaneCards)
        {
            var cardScript = card.GetComponent<AirplaneCard>();
            if (PlayerPrefs.GetInt($"Airplane_{cardScript.cardIndex}_Purchased", 0) == 1)
            {
                cardScript.SetState(false, true, false);
            }
        }
    }

    public void SaveChosenAirplane(int cardIndex)
    {
        chosenCardIndex = cardIndex;
        PlayerPrefs.SetInt("ChosenAirplane", chosenCardIndex);
        PlayerPrefs.Save();
        _planeImage.sprite = _spritesPlanes[cardIndex];
    }
}