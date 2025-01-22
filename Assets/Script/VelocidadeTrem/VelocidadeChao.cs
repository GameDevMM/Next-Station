using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocidadeChao : MonoBehaviour
{
    private Renderer meshRender;
    private Material currentMaterial;
    private float offset;
    public float incrementoOffset;
    public string sortingLayer;
    public int orderInLayer;
    public float Velocidade;

    // Start is called before the first frame update
    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        meshRender.sortingLayerName = sortingLayer;
        meshRender.sortingOrder = orderInLayer;

        currentMaterial = meshRender.material;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset += incrementoOffset;
        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * Velocidade, 0));
    }
}
