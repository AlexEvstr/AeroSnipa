using UnityEngine;

public class GameAudioCotroller : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    private int _isVibro;

    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _cancelSound;
    [SerializeField] private AudioClip _cashSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _shotSound;

    private void Start()
    {
        Vibration.Init();
        _musicSource.volume = PlayerPrefs.GetFloat("music", 1);
        _soundSource.volume = PlayerPrefs.GetFloat("sound", 1);
        _isVibro = PlayerPrefs.GetInt("vibro", 1);
    }

    public void PlayClickSound()
    {
        _soundSource.PlayOneShot(_clickSound);
        if (_isVibro == 1) Vibration.VibratePop();
    }

    public void PlayDeclibeSound()
    {
        _soundSource.PlayOneShot(_cancelSound);
        if (_isVibro == 1) Vibration.VibrateNope();
    }

    public void PlayCashSound()
    {
        _soundSource.PlayOneShot(_cashSound);
        if (_isVibro == 1) Vibration.VibratePeek();
    }

    public void PlayGameOverSound()
    {
        _musicSource.Stop();
        _soundSource.PlayOneShot(_gameOverSound);
        if (_isVibro == 1) Vibration.Vibrate();
    }

    public void PlayShotSound()
    {
        _soundSource.PlayOneShot(_shotSound);
        if (_isVibro == 1) Vibration.VibratePop();
    }
}