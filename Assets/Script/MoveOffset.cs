using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour
{
    //SCRIPT USADO PARA MOVIMENTAÇÃO DO CENARIO PARA SENSAÇÃO DE MOVIMENTO

    private     Material    Material;
    private     float       Offset;
    public      float       velocidadeX;
    public      float       velocidadeY;
    public      float       Incremento;

    // Use this for initialization
    void Start()
    {
        Material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Offset += Incremento;
        Material.SetTextureOffset("_MainTex", new Vector2(Offset * velocidadeX, Offset * velocidadeY));
    }
}
