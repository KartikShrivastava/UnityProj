using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer
{
    public GameObject go;
    public List<SaladOrder> orderList = new List<SaladOrder>();

    public Customer(GameObject g, List<SaladOrder> ol)
    {
        go = g;
        orderList = ol;
        ol = null;

        for(int i=0; i< orderList.Count; ++i)
        {
            orderList[i].quad.transform.SetParent(go.transform.GetChild(0));
            orderList[i].quad.transform.position = go.transform.GetChild(0).position + new Vector3(0.0f, i * 0.6f, i * 0.5f);
        }
    }
    
}
