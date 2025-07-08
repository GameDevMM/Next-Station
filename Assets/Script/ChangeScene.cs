using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject panelOptionsMenu;

    public void FuncaoMudarCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void OpenOptionsMenu()
    {
        panelOptionsMenu.SetActive(true);
        panelMainMenu.SetActive(false);
    }

    public void OpenMainMenu()
    {
        panelMainMenu.SetActive(true);
        panelOptionsMenu.SetActive(false);
    }
}
