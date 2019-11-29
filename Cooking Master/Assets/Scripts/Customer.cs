using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer
{
    public GameObject go;
    public List<SaladOrder> orderList = new List<SaladOrder>();
    public Image bgImage, fillImage;
    public CustomerSatisfaction satisfactionState;
    public bool isFrustated = false;
    public int posInIsCustomerPresent;

    public Customer(GameObject g, List<SaladOrder> ol, Image bgImg, Image fillImg, int pos)
    {
        posInIsCustomerPresent = pos;
        isFrustated = false;
        satisfactionState = CustomerSatisfaction.veryHappy;
        go = g;
        orderList = ol;
        ol = null;

        for(int i=0; i< orderList.Count; ++i)
        {
            orderList[i].quad.transform.SetParent(go.transform.GetChild(0));
            orderList[i].quad.transform.position = go.transform.GetChild(0).position + new Vector3(0.0f, i * 0.6f, i * 0.5f);
        }

        if(bgImg != null && fillImg != null)
        {
            bgImage = bgImg;
            fillImage = fillImg;
        }
    }
}
