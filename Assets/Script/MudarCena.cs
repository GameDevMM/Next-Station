using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MudarCena : MonoBehaviour
{
    //SCRIPT USADO PARA MUDANÇA DE CENARIOS

    public  void    FuncaoMudarCena(string    nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }
}
