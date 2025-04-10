using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissionManager : MonoBehaviour
{
    public enum MissionType { TimeHits, NoLifeLossHits }

    [System.Serializable]
    public class Mission
    {
        public MissionType type;
        public int requiredHits;
        public float timeLimit;
        public string missionText;
    }

    [SerializeField] private GameObject _gameoverWindow;
    [SerializeField] private GameObject _shootBtn;
    [SerializeField] private Text _resultText;
    private GameWindowManager _gameWindowManager;
    public Mission[] missions;
    public Text missionTextUI;
    public Text timerTextUI;

    private GameManager gameManager;
    private int currentMissionIndex = 0;
    private int hitCount = 0;
    private int startLives;
    private bool missionActive = false;
    private bool missionEnabled = false;


    private void Start()
    {
        if (PlayerPrefs.GetInt("ModeIndex", 0) != 1)
        {
            _shootBtn.SetActive(true);
            return;
        }
        _resultText.text = "MISSION FAILED";
        _gameWindowManager = GetComponent<GameWindowManager>();
        _gameWindowManager.OpenMissionWindow();

        missionEnabled = true;
        gameManager = GetComponent<GameManager>();
        currentMissionIndex = PlayerPrefs.GetInt("MissionIndex", 0);
        if (currentMissionIndex >= missions.Length)
            currentMissionIndex = 0;

        missionTextUI.text = missions[currentMissionIndex].missionText;
    }

    public void StartMission()
    {
        _shootBtn.SetActive(true);
        hitCount = 0;
        missionActive = true;
        startLives = GetCurrentLives();
        

        if (missions[currentMissionIndex].type == MissionType.TimeHits)
            StartCoroutine(Timer(missions[currentMissionIndex].timeLimit));
        else
            timerTextUI.text = "";
    }

    public void RegisterHit()
    {
        if (!missionEnabled || !missionActive) return;

        hitCount++;

        var mission = missions[currentMissionIndex];
        

        if (hitCount >= mission.requiredHits)
        {
            CompleteMission();
        }
    }

    public void CheckLifes()
    {
        var mission = missions[currentMissionIndex];
        if (mission.type == MissionType.NoLifeLossHits && GetCurrentLives() < startLives)
        {
            FailMission();
            return;
        }
    }

    private IEnumerator Timer(float time)
    {
        int seconds = Mathf.CeilToInt(time);
        while (seconds > 0)
        {
            timerTextUI.text = seconds.ToString();
            yield return new WaitForSeconds(1f);
            seconds--;
        }

        timerTextUI.text = "0";

        if (!missionActive) yield break;

        if (hitCount >= missions[currentMissionIndex].requiredHits)
            CompleteMission();
        else
            FailMission();
    }

    private void CompleteMission()
    {
        missionActive = false;
        timerTextUI.text = "";
        _resultText.text = "MISSION COMPLETE";
        gameManager.AddCoinsForMission(500);
        _gameWindowManager.OpenGameOver();

        currentMissionIndex++;
        if (currentMissionIndex >= missions.Length)
            currentMissionIndex = 0;

        PlayerPrefs.SetInt("MissionIndex", currentMissionIndex);
    }

    private void FailMission()
    {
        if (_gameoverWindow.transform.localScale.x == 0)
        {
            missionActive = false;
            timerTextUI.text = "";
            _gameWindowManager.OpenGameOver();
        }
    }

    private int GetCurrentLives()
    {
        return gameManager.GetCurrentLives();
    }
}
