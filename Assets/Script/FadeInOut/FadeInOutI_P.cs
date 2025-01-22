using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FadeInOutI_P : MonoBehaviour
{
    private SpriteRenderer fadeTexture;
    public float fadeSpeed;


    // Use this for initialization
    void Start()
    {

        fadeTexture = GetComponent<SpriteRenderer>();
        StartCoroutine("fadeOut");

    }

    // Update is called once per frame
    void Update()
    {



    }

    public void FuncaoMudarCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    IEnumerator fadeOut()
    {

        Color cor = new Color(0, 0, 0, 1);
        fadeTexture.material.color = cor;

        for (float f = 1; f > 0; f -= fadeSpeed)
        {
            cor.a = f;
            fadeTexture.material.color = cor;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(5);
        StartCoroutine("fadeIn");
        yield return new WaitForSeconds(1);
        FuncaoMudarCena("Primavera");

    }

    IEnumerator fadeIn()
    {

        Color cor = new Color(0, 0, 0, 0);
        fadeTexture.material.color = cor;

        for (float f = 0; f < 1; f += fadeSpeed)
        {
            cor.a = f;
            fadeTexture.material.color = cor;
            yield return new WaitForEndOfFrame();
        }

    }


}
