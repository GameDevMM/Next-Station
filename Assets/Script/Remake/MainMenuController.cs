using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private List<GameObject> menuPanels;

    public void SetActiveMenu(GameObject menuToActivate)
    {
        foreach (var menu in menuPanels)
        {
            menu.SetActive(menu == menuToActivate);
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