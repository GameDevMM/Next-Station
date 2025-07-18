using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
}
