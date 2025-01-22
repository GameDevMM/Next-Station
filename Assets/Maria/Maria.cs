using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maria : MonoBehaviour
{
    public bool mariaNaMinhaVida;
    public Text Texto;
    public GameObject euAlegre;
    public GameObject eutriste;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (mariaNaMinhaVida == true)
        {
            euAlegre.SetActive(true);
            eutriste.SetActive(false);
            Texto.text = "Eu sou muito feliz";
        }

        else
        {
            euAlegre.SetActive(false);
            eutriste.SetActive(true);
            Texto.text = "Eu sou muito triste";
        }
    }
}
