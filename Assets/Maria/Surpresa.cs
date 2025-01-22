using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surpresa : MonoBehaviour
{

    private Maria scriptMaria;
    private Rigidbody2D CorpoFisico;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SurpresaCorrotina");
        scriptMaria = FindObjectOfType(typeof(Maria)) as Maria;
        CorpoFisico = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SurpresaCorrotina()
    {
        
        if (scriptMaria.mariaNaMinhaVida == true)
        {
            yield return new WaitForSeconds(2);
        }
    }
}
