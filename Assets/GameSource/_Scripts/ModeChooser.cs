using UnityEngine;

public class ModeChooser : MonoBehaviour
{
    public void ChooseMode(int index)
    {
        PlayerPrefs.SetInt("ModeIndex", index);
    }
}