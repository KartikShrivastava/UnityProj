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
        //customer angry
        if(customer.orderList.Count != salad.Count)
        {
            return CustomerSatisfaction.veryAngry;
        }
        
        for(int i=0; i<customer.orderList.Count; ++i)
        {
            //again customer angry
            if (salad[i].GetComponent<ItemsController>().fruit != customer.orderList[i].name)
            {
                return CustomerSatisfaction.veryAngry;
            }
        }

        //check at what time did customer get served
        return customer.satisfactionState;
    }
}
