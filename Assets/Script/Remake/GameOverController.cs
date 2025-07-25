using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    public Button[] buttons;

    void Start()
    {
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }

        StartCoroutine(EnableButtonsAfterSeconds(2f));

        int currentScore = GameStats.currentScore;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        currentScoreText.text = currentScore.ToString();
        highScoreText.text = highScore.ToString();
    }

    IEnumerator EnableButtonsAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Button btn in buttons)
        {
            btn.interactable = true;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE || UNITY_WEBGL
        Application.Quit();
#elif UNITY_ANDROID || UNITY_IOS
Application.Quit();
#endif
    }
}
