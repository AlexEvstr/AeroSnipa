using System.Collections;
using UnityEngine;

public class GameWindowManager : MonoBehaviour
{
    public float animationDuration = 0.5f;
    [SerializeField] private GameObject _gameOverWindow;
    [SerializeField] private GameObject _missionWindow;
    private GameAudioCotroller _gameAudioCotroller;

    private void Start()
    {
        _gameAudioCotroller = GetComponent<GameAudioCotroller>();
    }

    public void OpenGameOver()
    {
        StartCoroutine(ExpandWindow(_gameOverWindow));
        _gameAudioCotroller.PlayGameOverSound();
    }

    public void OpenMissionWindow()
    {
        StartCoroutine(ExpandWindow(_missionWindow));
    }

    public void CloseMissionWindow()
    {
        StartCoroutine(CollapseWindow(_missionWindow));
    }


    private IEnumerator ExpandWindow(GameObject window, float duration = 0.5f)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            window.transform.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }

        window.transform.localScale = endScale;
    }

    private IEnumerator CollapseWindow(GameObject window, float duration = 0.5f)
    {
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.zero;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            window.transform.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }

        window.transform.localScale = endScale;
        window.SetActive(false); // Опционально скрываем объект после анимации
    }

}