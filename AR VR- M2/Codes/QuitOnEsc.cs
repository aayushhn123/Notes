using UnityEngine;

public class QuitOnEsc : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            Debug.Log("ESC pressed: would quit app (Editor detected).");
            UnityEditor.EditorApplication.isPlaying = false; // stop playmode
#else
            Debug.Log("ESC pressed: quitting application.");
            Application.Quit();
#endif
        }
    }
}