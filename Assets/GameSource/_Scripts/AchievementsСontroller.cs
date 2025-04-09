using UnityEngine;
using UnityEngine.UI;

public class AchievementsСontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] _achievements;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Achieve_1", 0) == 1) UnlockAchieve(0);
        if (PlayerPrefs.GetInt("Achieve_2", 0) == 1) UnlockAchieve(1);
        if (PlayerPrefs.GetInt("Achieve_3", 0) == 1) UnlockAchieve(2);
        if (PlayerPrefs.GetInt("Achieve_4", 0) == 1) UnlockAchieve(3);
        if (PlayerPrefs.GetInt("Achieve_5", 0) == 1) UnlockAchieve(4);
        if (PlayerPrefs.GetInt("Achieve_6", 0) == 1) UnlockAchieve(5);
    }

    private void UnlockAchieve(int achieveIndex)
    {
        _achievements[achieveIndex].transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _achievements[achieveIndex].transform.GetChild(2).GetComponent<Text>().text = PlayerPrefs.GetString($"DateForAchieve_{achieveIndex}", "");
    }
}