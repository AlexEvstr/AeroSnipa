using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text totalCoinsText;
    public Text roundCoinsText;
    public GameObject[] lifeIcons;

    private int totalCoins;
    private int roundCoins;
    private int lives = 3;
    [SerializeField] private Text _coinsToAdd;
    [SerializeField] private GameObject _coinImage;
    private int _totalHitsCount;
    private int _totalGamesCount;

    private GameWindowManager _gameWindowManager;

    private void Start()
    {
        _totalHitsCount = PlayerPrefs.GetInt("TotalHits", 0);
        _totalGamesCount = PlayerPrefs.GetInt("TotalGames", 0);
        _gameWindowManager = GetComponent<GameWindowManager>();
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinUI();
    }

    public void AddCoins()
    {
        IncreaseAndHits();
        int coinsToAdd = UnityEngine.Random.Range(50, 76);
        coinsToAdd -= coinsToAdd % 5;
        StartCoroutine(ShowCoinsText(coinsToAdd));

        totalCoins += coinsToAdd;
        roundCoins += coinsToAdd;

        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        UpdateCoinUI();
    }

    private void IncreaseAndHits()
    {
        string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
        
        _totalHitsCount++;
        PlayerPrefs.SetInt("TotalHits", _totalHitsCount);
        if (_totalHitsCount >= 100)
        {
            PlayerPrefs.SetInt("Achieve_3", 1);
            PlayerPrefs.SetString("DateForAchieve_2", currentDate);
        }
        else if (_totalHitsCount >= 50)
        {
            PlayerPrefs.SetInt("Achieve_2", 1);
            PlayerPrefs.SetString("DateForAchieve_1", currentDate);
        }
        else if (_totalHitsCount >= 10)
        {
            PlayerPrefs.SetInt("Achieve_1", 1);
            PlayerPrefs.SetString("DateForAchieve_0", currentDate);
        }
    }

    private IEnumerator ShowCoinsText(int coinsCount)
    {
        _coinImage.SetActive(true);
        _coinsToAdd.text = $"+{coinsCount}";
        yield return new WaitForSeconds(1.0f);
        _coinImage.SetActive(false);
    }

    private void UpdateCoinUI()
    {
        totalCoinsText.text = totalCoins.ToString();
        roundCoinsText.text = roundCoins.ToString();
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            lifeIcons[lives].SetActive(false);
        }

        if (lives == 0)
        {
            _gameWindowManager.OpenGameOver();
            IncreaseAndCheckGames();
        }
    }

    private void IncreaseAndCheckGames()
    {
        string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

        _totalGamesCount++;
        PlayerPrefs.SetInt("TotalGames", _totalGamesCount);
        if (_totalGamesCount >= 100)
        {
            PlayerPrefs.SetInt("Achieve_6", 1);
            PlayerPrefs.SetString("DateForAchieve_5", currentDate);
        }
        else if (_totalGamesCount >= 50)
        {
            PlayerPrefs.SetInt("Achieve_5", 1);
            PlayerPrefs.SetString("DateForAchieve_4", currentDate);
        }
        else if (_totalGamesCount >= 10)
        {
            PlayerPrefs.SetInt("Achieve_4", 1);
            PlayerPrefs.SetString("DateForAchieve_3", currentDate);
        }
    }

    public void ResetRound()
    {
        roundCoins = 0;
        UpdateCoinUI();
        lives = 3;

        foreach (var icon in lifeIcons)
        {
            icon.SetActive(true);
        }
    }
}