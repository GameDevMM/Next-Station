using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{

    [Header("Configuração do sexo")]
    public  SpriteRenderer      animacaoGarotoOff;
    public  SpriteRenderer      animacaoGarotoOn;
    public  SpriteRenderer      animacaoGarotaOff;
    public  SpriteRenderer      animacaoGarotaOn;
    public  Button              botaoSelecionarGaroto;
    public  Button              botaoSelecionarGarota;
    public  Button              selecionarSexo;
    public  Button              botaoSeguranca;

    [Header("Configurãção dos Paineis")]
    public  GameObject          painelSelecionarSexo;
    public  GameObject          painelInserirNome;
    public  GameObject          painelDialogo;

    [Header ("Dados do Jogador")]
    public  string              Sexo;
    public  string              Nome;
    public  Text                falaDialogo;
    public  int                 idFase;


    // Start is called before the first frame update
    void Start()
    {

        idFase = 0;
        animacaoGarotoOff.enabled = true;
        animacaoGarotoOn.enabled = false;
        animacaoGarotaOff.enabled = true;
        animacaoGarotaOn.enabled = false;

        botaoSeguranca.interactable = true;
        selecionarSexo.interactable = false;

        Nome = PlayerPrefs.GetString("Nome");
    }

    // Update is called once per frame
    void Update()
    {
        Dialogo();
        Nome = PlayerPrefs.GetString("Nome");
    }

    public  void    SelecionarGaroto()
    {
        selecionarSexo.interactable = true;
        Sexo = "Garoto";
        PlayerPrefs.SetString("Sexo", Sexo);
        animacaoGarotoOff.enabled = false;
        animacaoGarotoOn.enabled = true;
        animacaoGarotaOff.enabled = true;
        animacaoGarotaOn.enabled = false;
    }

    public void SelecionarGarota()
    {
        selecionarSexo.interactable = true;
        Sexo = "Garota";
        PlayerPrefs.SetString("Sexo", Sexo);
        animacaoGarotaOff.enabled = false;
        animacaoGarotaOn.enabled = true;
        animacaoGarotoOff.enabled = true;
        animacaoGarotoOn.enabled = false;
    }

    public  void    ClicarForaTela()
    {
        selecionarSexo.interactable = false;
        animacaoGarotaOff.enabled = true;
        animacaoGarotoOff.enabled = true;
        animacaoGarotaOn.enabled = false;
        animacaoGarotoOn.enabled = false;
    }

    public  void    SelecionarSexo()
    {
        PlayerPrefs.GetString("Sexo");
        idFase += 1;
    }

    public void Proximo()
    {
        if (idFase < 3)
        {
            idFase += 1;
        }

        else
        {
            SceneManager.LoadScene("Verao");
        }

    }

    public  void    Dialogo()
    {
        switch (idFase)
        {
            case 0:
                painelInserirNome.SetActive(false);
                painelDialogo.SetActive(false);
                painelSelecionarSexo.SetActive(true);
                break;

            case 1:
                painelSelecionarSexo.SetActive(false);
                painelDialogo.SetActive(true);
                falaDialogo.text = "Olá " +Sexo+ ", tudo bem? espero que sim, antes de começarmos, qual o seu nome?";
                break;

            case 2:
                painelDialogo.SetActive(false);
                painelInserirNome.SetActive(true);
                break;

            case 3:
                painelInserirNome.SetActive(false);
                painelDialogo.SetActive(true);
                falaDialogo.text = "Prazer " + Nome + ", me chamo José dos Espetinhos, vamos começar?";
                break;
        }
    }
    
}
