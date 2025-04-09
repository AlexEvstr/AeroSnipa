using UnityEngine;
using UnityEngine.UI;

public class AirplaneSkills : MonoBehaviour
{
    [Header("Skill Settings")]
    public int cardIndex;
    public Button[] upgradeButtons;
    public GameObject[] skillChargesObjects;
    public int[] skillLevels = { 1, 1, 1 };
    public int[] skillCosts = { 75, 100, 125 };

    private const int maxSkillLevel = 10;
    private ShopManager shopManager;
    [SerializeField] private SettingsManager _settingsManager;

    private void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();

        LoadSkillLevels();

        UpdateUpgradeButtons();

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int skillIndex = i;
            upgradeButtons[i].onClick.AddListener(() => UpgradeSkill(skillIndex));
        }
    }

    private void LoadSkillLevels()
    {
        for (int i = 0; i < skillLevels.Length; i++)
        {
            string skillKey = $"Airplane_{cardIndex}_Skill_{i}";
            skillLevels[i] = PlayerPrefs.GetInt(skillKey, skillLevels[i]);
            UpdateSkillCharges(i);
        }
    }

    private void SaveSkillLevel(int skillIndex)
    {
        string skillKey = $"Airplane_{cardIndex}_Skill_{skillIndex}";
        PlayerPrefs.SetInt(skillKey, skillLevels[skillIndex]);
        PlayerPrefs.Save();
    }

    private void UpgradeSkill(int skillIndex)
    {
        if (PlayerPrefs.GetInt($"Airplane_{cardIndex}_Purchased", 0) == 0)
        {
            return;
        }

        if (skillLevels[skillIndex] >= maxSkillLevel)
        {
            return;
        }

        if (shopManager.totalCoins < skillCosts[skillIndex])
        {
            _settingsManager.PlayDeclibeSound();
            return;
        }

        shopManager.totalCoins -= skillCosts[skillIndex];
        shopManager.UpdateCoinsUI();
        PlayerPrefs.SetInt("TotalCoins", shopManager.totalCoins);
        _settingsManager.PlayCashSound();

        skillLevels[skillIndex]++;
        SaveSkillLevel(skillIndex);
        UpdateSkillCharges(skillIndex);
    }

    private void UpdateSkillCharges(int skillIndex)
    {
        Transform charges = skillChargesObjects[skillIndex].transform;
        for (int i = 0; i < charges.childCount; i++)
        {
            charges.GetChild(i).gameObject.SetActive(i < skillLevels[skillIndex]);
        }
    }

    public void UpdateUpgradeButtons()
    {

        bool isPurchased = PlayerPrefs.GetInt($"Airplane_{cardIndex}_Purchased", 0) == 1;

        foreach (var button in upgradeButtons)
        {
            button.interactable = isPurchased;
        }
    }
}