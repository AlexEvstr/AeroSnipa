using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource soundSource;
    public AudioSource musicSource;

    [Header("UI Buttons")]
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    public GameObject vibroOnButton;
    public GameObject vibroOffButton;

    [Header("UI Save and Back Buttons")]
    public GameObject saveButton;
    public GameObject backButton;

    private const string SoundKey = "sound";
    private const string MusicKey = "music";
    private const string VibroKey = "vibro";
    private int _isVibro;
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _decline;
    [SerializeField] private AudioClip _cash;

    private SceneTransition _sceneTransition;

    private void Start()
    {
        _sceneTransition = GetComponent<SceneTransition>();
        LoadSettings();

        Vibration.Init();
    }

    public void EnableSound()
    {
        soundSource.volume = 1f;

        PlayerPrefs.SetFloat(SoundKey, soundSource.volume);
        PlayerPrefs.Save();

        soundOnButton.transform.GetChild(0).gameObject.SetActive(true);
        soundOffButton.transform.GetChild(0).gameObject.SetActive(false);

        ToggleSaveButton(true);
    }

    public void DisableSound()
    {
        soundSource.volume = 0f;

        PlayerPrefs.SetFloat(SoundKey, soundSource.volume);
        PlayerPrefs.Save();

        soundOnButton.transform.GetChild(0).gameObject.SetActive(false);
        soundOffButton.transform.GetChild(0).gameObject.SetActive(true);

        ToggleSaveButton(true);
    }

    public void EnableMusic()
    {

        musicSource.volume = 1f;

        PlayerPrefs.SetFloat(MusicKey, musicSource.volume);
        PlayerPrefs.Save();

        musicOnButton.transform.GetChild(0).gameObject.SetActive(true);
        musicOffButton.transform.GetChild(0).gameObject.SetActive(false);

        ToggleSaveButton(true);
    }

    public void DisableMusic()
    {

        musicSource.volume = 0f;

        PlayerPrefs.SetFloat(MusicKey, musicSource.volume);
        PlayerPrefs.Save();

        musicOnButton.transform.GetChild(0).gameObject.SetActive(false);
        musicOffButton.transform.GetChild(0).gameObject.SetActive(true);

        ToggleSaveButton(true);
    }

    public void EnableVibro()
    {

        PlayerPrefs.SetInt(VibroKey, 1);
        PlayerPrefs.Save();
        _isVibro = 1;
        vibroOnButton.transform.GetChild(0).gameObject.SetActive(true);
        vibroOffButton.transform.GetChild(0).gameObject.SetActive(false);

        ToggleSaveButton(true);
    }

    public void DisableVibro()
    {
        PlayerPrefs.SetInt(VibroKey, 0);
        PlayerPrefs.Save();
        _isVibro = 0;

        vibroOnButton.transform.GetChild(0).gameObject.SetActive(false);
        vibroOffButton.transform.GetChild(0).gameObject.SetActive(true);

        ToggleSaveButton(true);
    }

    private void LoadSettings()
    {

        float savedSoundVolume = PlayerPrefs.GetFloat(SoundKey, 1f);
        soundSource.volume = savedSoundVolume;
        UpdateSoundUI(savedSoundVolume > 0);

        float savedMusicVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
        musicSource.volume = savedMusicVolume;
        UpdateMusicUI(savedMusicVolume > 0);

        int savedVibro = PlayerPrefs.GetInt(VibroKey, 1);
        UpdateVibroUI(savedVibro > 0);
    }

    private void UpdateSoundUI(bool isOn)
    {
        soundOnButton.transform.GetChild(0).gameObject.SetActive(isOn);
        soundOffButton.transform.GetChild(0).gameObject.SetActive(!isOn);
    }

    private void UpdateMusicUI(bool isOn)
    {
        musicOnButton.transform.GetChild(0).gameObject.SetActive(isOn);
        musicOffButton.transform.GetChild(0).gameObject.SetActive(!isOn);
    }

    private void UpdateVibroUI(bool isOn)
    {
        vibroOnButton.transform.GetChild(0).gameObject.SetActive(isOn);
        vibroOffButton.transform.GetChild(0).gameObject.SetActive(!isOn);
    }

    private void ToggleSaveButton(bool isActive)
    {
        saveButton.SetActive(isActive);
        backButton.SetActive(!isActive);
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        _sceneTransition.ChangeScene("MenuScene");
    }

    public void PlayClickSound()
    {
        soundSource.PlayOneShot(_click);
        if (_isVibro == 1) Vibration.VibratePop();
    }

    public void PlayDeclibeSound()
    {
        soundSource.PlayOneShot(_decline);
        if (_isVibro == 1) Vibration.VibrateNope();
    }

    public void PlayCashSound()
    {
        soundSource.PlayOneShot(_cash);
        if (_isVibro == 1) Vibration.VibratePeek();
    }
}