using UnityEngine;
using TMPro;

public class TextHud : MonoBehaviour
{
    public TMP_Text scoreText; // assign TMP_ScoreText
    public TMP_Text infoText;  // assign TMP_InfoText

    int score;

    void Start()
    {
        SetScore(0);
        SetInfo("Welcome! WASD to move, Space to jump.");
    }

    public void AddScore(int delta)
    {
        score += delta;
        SetScore(score);
        Debug.Log($"Score changed to {score}");
    }

    public void SetInfo(string message)
    {
        if (infoText) infoText.text = message;
    }

    void SetScore(int s)
    {
        if (scoreText) scoreText.text = $"Score: <b>{s}</b>";
    }
}