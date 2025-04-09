using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Для сборки
        Application.Quit();
#endif
    }
}