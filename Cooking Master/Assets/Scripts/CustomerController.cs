using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject customer;
    public GameObject quad;
    public List<Customer> customerList = new List<Customer>();
    public Material[] mat;
    public static List<int> isCustomerPresent = new List<int>();
    public float spawnInterval = 10.0f;
    public float maxTimerVal = 40.0f;

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

        InvokeRepeating("MakeCustomer", 0.0f, spawnInterval);
    }

    public void MakeCustomer()
    {
        int pos = rand.Next(0, spawnPos.Count);

        if (isCustomerPresent[pos] != -1 || customerList.Count >= isCustomerPresent.Count)
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

        //finding the timer
        Image bgImage = null, fillImage = null;
        foreach(Transform child in temp.transform)
        {
            if (child.CompareTag("CustomerTimer"))
            {
                bgImage = child.GetChild(0).gameObject.GetComponent<Image>();
                fillImage = child.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
            }
        }

        customerList.Add(new Customer(temp,list, bgImage, fillImage, pos));

        StartCoroutine(CustomerTimer(customerList[customerList.Count-1], customerList.Count - 1, pos));

        list = null;
        temp = null;

    }

    IEnumerator CustomerTimer(Customer customer, int indexInCustomerList, int indexinIsCustomerPresent)
    {
        float startTime = Time.time;
        float val = 0.0f;

        while((val = (Time.time - startTime)/ maxTimerVal) < 1.0f)
        {
            if (isCustomerPresent[indexinIsCustomerPresent] == -1)
                yield break;

            if (val < 0.3f)
            {
                customer.satisfactionState = CustomerSatisfaction.veryHappy;
                customer.bgImage.color = new Color32(95, 255, 45, 255);
            }
            else if (val > 0.3f && val < 0.7f)
            {
                customer.satisfactionState = CustomerSatisfaction.happy;
                customer.bgImage.color = new Color32(255, 180, 45, 255);
            }
            else
            {
                customer.satisfactionState = CustomerSatisfaction.angry;
                customer.bgImage.color = new Color32(255, 60, 45, 255);
            }
            customer.fillImage.fillAmount = val;
            yield return null;
        }

        customer.isFrustated = true;
        RemoveFrustatedCustomer();
    }

    void RemoveFrustatedCustomer()
    {
        //decrement the score of both the players
        for(int i=0; i<playerController.players.Count; ++i)
        {
            playerController.players[i].score -= 5;
        }

        for(int i=0; i<customerList.Count; ++i)
        {
            if (customerList[i].isFrustated)
            {//remove this customer
                Destroy(customerList[i].go);
                RemoveCustomer(i, customerList[i].posInIsCustomerPresent);
            }
        }
    }

    public void RemoveCustomer(int posInCustomerList, int posinIsCustomerPresent)
    {
        customerList.RemoveAt(posInCustomerList);
        isCustomerPresent[posinIsCustomerPresent] = -1;

        for(int i=0; i<isCustomerPresent.Count; ++i)
        {
            if(isCustomerPresent[i] > posInCustomerList && isCustomerPresent[i] != -1)
            {
                isCustomerPresent[i]--;
            }
        }

        //PrintCustomer();
    }

    void PrintCustomer()
    {
        Debug.Log("---");
        for (int i=0; i<customerList.Count; ++i)
        {
            Debug.Log(i);
            foreach (SaladOrder sad in customerList[i].orderList)
            {
                Debug.Log(sad.name);
            }
        }

        foreach (int num in isCustomerPresent)
            Debug.Log(num);
        Debug.Log("---");
    }
}
