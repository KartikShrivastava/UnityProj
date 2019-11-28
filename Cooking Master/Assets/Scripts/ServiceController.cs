using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerSatisfaction
{
    angry, veryAngry, happy, veryHappy
}

public class ServiceController : MonoBehaviour
{
    public CustomerSatisfaction CheckCombination(Customer customer, List<GameObject> salad, int player)
    {
        Debug.Log(customer.orderList.Count + " " + salad.Count);
        //customer angry
        if(customer.orderList.Count != salad.Count)
        {
            Debug.Log("Customer angry");
            return CustomerSatisfaction.angry;
        }
        
        for(int i=0; i<customer.orderList.Count; ++i)
        {
            //again customer angry
            if(salad[i].GetComponent<ItemsController>().fruit != customer.orderList[i].name)
            {
                Debug.Log("Customer angry");
                return CustomerSatisfaction.angry;
            }
        }

        //customer happy
        Debug.Log("Customer happy");
        return CustomerSatisfaction.happy;
    }
}
