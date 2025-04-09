using System.Collections;
using UnityEngine;

public class GameWindowManager : MonoBehaviour
{
    public float animationDuration = 0.5f;
    [SerializeField] private GameObject _gameOverWindow;
    private GameAudioCotroller _gameAudioCotroller;

    private void Start()
    {
        _gameAudioCotroller = GetComponent<GameAudioCotroller>();
    }

    public void OpenGameOver()
    {
        StartCoroutine(ExpandWindow());
        _gameAudioCotroller.PlayGameOverSound();
    }

    private IEnumerator ExpandWindow(float duration = 0.5f)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            _gameOverWindow.transform.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }

        _gameOverWindow.transform.localScale = endScale;
    }
}