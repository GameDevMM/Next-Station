using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nome_Player : MonoBehaviour
{
    //SCRIPT PARA SALVAR NOME DO JOGADOR

    private Game_Controller scriptGameController;
    [SerializeField] InputField nome;


    void Start()
    {
        scriptGameController = FindObjectOfType(typeof(Game_Controller)) as Game_Controller;
        nome.text = "";
    }

    public void Salvar()
    {
        PlayerPrefs.SetString("Nome", nome.text);
        scriptGameController.idFase += 1;
    }
}
