using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOrderController : MonoBehaviour
{
    public List<GameObject> processedFruit = new List<GameObject>();

    public void AddFruitInSalad(GameObject go)
    {
        processedFruit.Add(go);
        go.transform.SetParent(transform);
        go.transform.localScale = new Vector3(0.5f, 0.25f, 0.5f);
        go.transform.position = transform.position + new Vector3(0.0f, (transform.childCount + 2.0f) * 0.3f, -1.5f);
        go = null;
    }

    public void Clear()
    {
        processedFruit.Clear();
    }
}
