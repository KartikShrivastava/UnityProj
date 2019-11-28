using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public GameObject customer;
    public GameObject quad;
    public List<Customer> customerList = new List<Customer>();
    public Material[] mat;
    public static List<int> isCustomerPresent = new List<int>();

    private List<Vector3> spawnPos = new List<Vector3>();
    private System.Random rand = new System.Random();

    void Start()
    {
        for(int i=0; i<7; ++i)
        {
            isCustomerPresent.Add(-1);
        }

        spawnPos.Add(new Vector3(-3.9f, 1.2f, 6.5f));
        spawnPos.Add(new Vector3(-2.6f, 1.2f, 6.5f));
        spawnPos.Add(new Vector3(-1.3f, 1.2f, 6.5f));
        spawnPos.Add(new Vector3(-0.0f, 1.2f, 6.5f));
        spawnPos.Add(new Vector3( 1.3f, 1.2f, 6.5f));
        spawnPos.Add(new Vector3( 2.6f, 1.2f, 6.5f));
        spawnPos.Add(new Vector3( 3.9f, 1.2f, 6.5f));

        //MakeCustomer(rand.Next(0, spawnPos.Count));
        InvokeRepeating("MakeCustomer", 0.0f, 10.0f);
    }

    public void MakeCustomer()
    {
        int pos = rand.Next(0, spawnPos.Count);

        if (isCustomerPresent[pos] != -1)
            return;

        isCustomerPresent[pos] = customerList.Count;

        GameObject temp = Instantiate(customer, spawnPos[pos], Quaternion.identity);

        List<SaladOrder> list = new List<SaladOrder>();
        int numOrders = rand.Next(1, 3);
        numOrders = 3;
        GameObject go;
        for(int i=0; i<numOrders; ++i)
        {
            go = Instantiate(quad);
            int fruit = rand.Next(0, 6);
            list.Add(new SaladOrder(go, (FruitName)fruit, mat[fruit]));
            go = null;
        }

        customerList.Add(new Customer(temp,list));

        list = null;
        temp = null;

    }

    public void RemoveCustomer(int pos)
    {
        customerList.RemoveAt(pos);
        for(int i=pos+1; i<customerList.Count; ++i)
        {
            isCustomerPresent[i]--;
        }
        isCustomerPresent[pos] = -1;
    }
}
