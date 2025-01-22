using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cena_Jogo : MonoBehaviour
{
    [Header ("Configuração de Velocidade Trem")]
    public int velocidadeRapida;
    public int velocidadeMedia;
    public int velocidadeLenta;
    public int velocidadeAtual;
    public GameObject Indicador;
    public GameObject indicadorRapido;
    public GameObject indicadorMedio;
    public GameObject indicadorLento;

    [Header("Configuração para Descarrilhar")]
    public Transform indicadorDescarrilhar;
    public float valorDescarrilharLPos;
    public float valorDescarrilharMPos;
    public float valorDescarrilharRPos;
    public float valorDescarrilharLNeg;
    public float valorDescarrilharMNeg;
    public float valorDescarrilharRNeg;
    public float posXIndicador;
    public float posYIndicador;

    [Header("Configuração de Temperatura")]
    public Transform indicadorTemperatura;
    public float Temperatura_5P;
    public float Temperatura_4P;
    public float Temperatura_3P;
    public float Temperatura_2P;
    public float Temperatura_1P;
    public float Temperatura_0;
    public float Temperatura_1N;
    public float Temperatura_2N;
    public float Temperatura_3N;
    public float Temperatura_4N;
    public float Temperatura_5N;
    public float temperaturaAtual;
    public float posXTemperatura;
    public float posYTemperatura;

    public int Distancia;
    public Text distanciaTexto;
    private MoveOffset          scriptMoveOffset;
    private VelocidadeChao      scriptMovChao;
    private VelocidadeTrilho    scriptMovTrilho;
    private VelocidadeCerca     scriptMovCerca;
    private VelocidadePraia     scriptMovPraia;
    private VelocidadeCeu       scriptMovCeu;

    // Start is called before the first frame update
    void Start()
    {
        scriptMovChao = FindObjectOfType(typeof(VelocidadeChao)) as VelocidadeChao;
        scriptMovTrilho = FindObjectOfType(typeof(VelocidadeTrilho)) as VelocidadeTrilho;
        scriptMovCerca = FindObjectOfType(typeof(VelocidadeCerca)) as VelocidadeCerca;
        scriptMovPraia = FindObjectOfType(typeof(VelocidadePraia)) as VelocidadePraia;
        scriptMovCeu = FindObjectOfType(typeof(VelocidadeCeu)) as VelocidadeCeu;
        scriptMoveOffset = FindObjectOfType(typeof(MoveOffset)) as MoveOffset;
        velocidadeAtual = velocidadeRapida;
        StartCoroutine("IndicadorDescarrilhar");
        StartCoroutine("MarcarDistancia");
    }

    // Update is called once per frame
    void Update()
    {
        DiminuirTemperatura();
        AumentarTemperatura();
        Temperatura();
        distanciaTexto.text = Distancia.ToString();
        MudarVelocidade();
        IndicadorVelocidade();
        ControleTrilho();
        indicadorDescarrilhar.transform.position = new Vector2(posXIndicador, posYIndicador);
        indicadorTemperatura.transform.position = new Vector2(posXTemperatura, posYTemperatura);
        irParaInverno();
        irParaOutono();
        irParaPrimavera();
        irParaVerao();
    }


    public  void    MudarVelocidade()
    {
        if (velocidadeAtual == 3 || velocidadeAtual == 2 || velocidadeAtual == 1)
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                velocidadeAtual -= 1;
            }

            else if (Input.GetKeyDown(KeyCode.W))
            {
                velocidadeAtual += 1;
            }
        }

        else if (velocidadeAtual == 0)
        {
            velocidadeAtual = 1;
        }

        else if (velocidadeAtual == 4)
        {
            velocidadeAtual = 3;
        }
    }

    public  void    IndicadorVelocidade()
    {
        switch(velocidadeAtual)
        {
            case 1:
                CenarioLenta();
                Indicador.transform.position = (new Vector2 (indicadorLento.transform.position.x, indicadorLento.transform.position.y));
                break;

            case 2:
                CenarioMedio();
                Indicador.transform.position = (new Vector2(indicadorMedio.transform.position.x, indicadorMedio.transform.position.y));
                break;

            case 3:
                CenarioRapido();
                Indicador.transform.position = (new Vector2(indicadorRapido. transform.position.x, indicadorRapido.transform.position.y));
                break;
        }
    }

    IEnumerator IndicadorDescarrilhar()
    {
        yield return new WaitForSeconds(1);
        switch (velocidadeAtual)
        {
            case 1:
                if(posXIndicador >= 0)
                {
                    posXIndicador += valorDescarrilharLPos;
                }
                else
                {
                    posXIndicador += valorDescarrilharLNeg;
                }
                break;

            case 2:
                if (posXIndicador >= 0)
                {
                    posXIndicador += valorDescarrilharMPos;
                }
                else
                {
                    posXIndicador += valorDescarrilharMNeg;
                }
                break;

            case 3:
                if (posXIndicador >= 0)
                {
                    posXIndicador += valorDescarrilharRPos;
                }
                else
                {
                    posXIndicador += valorDescarrilharRNeg;
                }
                break;
        }
        StartCoroutine("IndicadorDescarrilhar");
    }

    public  void    ControleTrilho()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            switch (velocidadeAtual)
            {
                case 1:
                    posXIndicador += valorDescarrilharLNeg;
                    break;

                case 2:
                    posXIndicador += valorDescarrilharMNeg;
                    break;

                case 3:
                    posXIndicador += valorDescarrilharRNeg;
                    break;
            }
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            switch (velocidadeAtual)
            {
                case 1:
                    posXIndicador += valorDescarrilharLPos;
                    break;

                case 2:
                    posXIndicador += valorDescarrilharMPos;
                    break;

                case 3:
                    posXIndicador += valorDescarrilharRPos;
                    break;
            }
        }
    }

    public  void    CenarioRapido()
    {
        scriptMovCerca.Velocidade = 20;
        scriptMovCeu.Velocidade = 1;
        scriptMovChao.Velocidade = 20;
        scriptMovPraia.Velocidade = 5f;
        scriptMovTrilho.Velocidade = 20;
    }

    public void CenarioMedio()
    {
        scriptMovCerca.Velocidade = 10;
        scriptMovCeu.Velocidade = 0.5f;
        scriptMovChao.Velocidade = 10;
        scriptMovPraia.Velocidade = 2.5f;
        scriptMovTrilho.Velocidade = 10;
    }

    public void CenarioLenta()
    {
        scriptMovCerca.Velocidade = 7;
        scriptMovCeu.Velocidade = 0.3f;
        scriptMovChao.Velocidade = 7;
        scriptMovPraia.Velocidade = 1.6f;
        scriptMovTrilho.Velocidade = 7;
    }

    public  void    Temperatura()
    {
        switch (temperaturaAtual)
        {
            case 1:
                posYTemperatura = Temperatura_5P;
                break;

            case 0.8f:
                posYTemperatura = Temperatura_4P;
                break;

            case 0.6f:
                posYTemperatura = Temperatura_3P;
                break;

            case 0.4f:
                posYTemperatura = Temperatura_2P;
                break;

            case 0.2f:
                posYTemperatura = Temperatura_1P;
                break;

            case 0:
                posYTemperatura = Temperatura_0;
                break;

            case -0.2f:
                posYTemperatura = Temperatura_1N;
                break;

            case -0.4f:
                posYTemperatura = Temperatura_2N;
                break;

            case -0.6f:
                posYTemperatura = Temperatura_3N;
                break;

            case -0.8f:
                posYTemperatura = Temperatura_4N;
                break;

            case -1:
                posYTemperatura = Temperatura_5N;
                break;
        }
    }

    IEnumerator MarcarDistancia()
    {
        switch (velocidadeAtual)
        {
            case 1:
                Distancia += 2;
                break;

            case 2:
                Distancia += 5;
                break;

            case 3:
                Distancia += 10;
                break;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine("MarcarDistancia");
    }

    public  void    irParaInverno()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("Inverno");
        }
    }

    public void irParaPrimavera()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Primavera");
        }
    }

    public void irParaVerao()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("Verao");
        }
    }

    public void irParaOutono()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("Outono");
        }
    }

    public void AumentarTemperatura()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            temperaturaAtual += 0.2f;
        }
    }

    public void DiminuirTemperatura()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            temperaturaAtual -= 0.2f;
        }
    }
}
