using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image blackScreenImage;
    public float fadeDuration = 1.0f;

    private void Start()
    {
        blackScreenImage.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeIn());
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackScreenImage.color = new Color(0, 0, 0, 1 - (timer / fadeDuration));
            yield return null;
        }
        blackScreenImage.color = new Color(0, 0, 0, 0);
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackScreenImage.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }
        blackScreenImage.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene(sceneName);
    }
}