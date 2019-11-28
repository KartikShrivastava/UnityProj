using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessFruitController : MonoBehaviour {

    public PickOrderController pickOrderController;
    public bool isProcesssing = false;
    public float timer = 1.0f;
    public Image img;

    private GameObject processingFruit;

    public void StartProcessing(GameObject go)
    {
        img.fillAmount = 0.0f;
        isProcesssing = true;
        processingFruit = go;
        go = null;
        processingFruit.transform.SetParent(transform);
        processingFruit.transform.position = transform.position + new Vector3(0.0f, 1.0f, -1.5f);
        StartCoroutine(ProcessTimer());
    }

    IEnumerator ProcessTimer()
    {
        float startTime = Time.time;
        float ratio = 0.0f;
        while((ratio = (Time.time - startTime)/timer) < 1.0f)
        {
            img.fillAmount = ratio;
            yield return null;
        }

        //on timer end
        pickOrderController.AddFruitInSalad(processingFruit);
        processingFruit = null;
        isProcesssing = false;
        img.fillAmount = 0.0f;
    }
}
