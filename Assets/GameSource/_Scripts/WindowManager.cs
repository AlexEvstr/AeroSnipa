using System.Collections;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GameObject menuWindow;
    public GameObject settingsWindow;
    public GameObject shopWindow;
    public GameObject achievementsWindow;
    public GameObject _saveBtn;
    public GameObject _backBtn;

    public float animationDuration = 0.5f;

    public void OpenWindow(GameObject windowToOpen)
    {
        StartCoroutine(AnimateWindow(windowToOpen, true));
        menuWindow.SetActive(false);
    }

    public void CloseWindowAndOpenMenu(GameObject windowToClose)
    {
        StartCoroutine(AnimateWindow(windowToClose, false, () =>
        {
            menuWindow.SetActive(true);
        }));
        _saveBtn.SetActive(false);
        _backBtn.SetActive(true);
    }

    private IEnumerator AnimateWindow(GameObject window, bool isOpening, System.Action onComplete = null)
    {
        window.SetActive(true);
        Transform windowTransform = window.transform;
        Vector3 startScale = isOpening ? Vector3.zero : Vector3.one;
        Vector3 endScale = isOpening ? Vector3.one : Vector3.zero;

        float timer = 0f;
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / animationDuration;
            windowTransform.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }

        windowTransform.localScale = endScale;

        if (!isOpening)
        {
            window.SetActive(false);
        }

        onComplete?.Invoke();
    }
}