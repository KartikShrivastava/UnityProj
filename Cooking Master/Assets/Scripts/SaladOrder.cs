using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaladOrder
{
    public GameObject quad;
    public FruitName name;

    public SaladOrder(GameObject go, FruitName n, Material mat)
    {
        quad = go;
        quad.GetComponent<MeshRenderer>().material = mat;
        name = n;
    }
}
