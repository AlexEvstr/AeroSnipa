using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBehavior : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GoToMenu());
    }

    private IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MenuScene");
    }
}