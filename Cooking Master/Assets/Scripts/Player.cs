using System.Collections.Generic;
using UnityEngine;

public class Player {

    public int row=0, col=0;
    public Material material;
    public float timeRemaining;
    public GameObject timeBar;
    public GameObject tray;
    public Queue<Items> items;
    public List<GameObject> salad = new List<GameObject>();
    public int score = 0;

    public Player(int r, int c, Material m, float t, GameObject timeBarGO, GameObject trayGO) {
        row = r;
        col = c;
        material = m;
        timeRemaining = t;
        timeBar = timeBarGO;
        tray = trayGO;
        items = new Queue<Items>();
    }

    public void AddItem(ItemType type, FruitName name, GameObject go)
    {
        if (salad.Count > 0)
            return;
        if (go != null)
        {
            go.transform.position = tray.transform.position + new Vector3(0.0f, 0.3f, (tray.transform.childCount - 0.5f) * 0.6f);
            go.transform.SetParent(tray.transform);
            items.Enqueue(new Items(type, name, go));
            go = null;
        }
    }

    public void RemoveItem() {
        if (items.Count > 0)
        {
            items.Peek().go = null;
            items.Dequeue();
            if (tray.transform.childCount > 0)
            {
                int i = 0;
                foreach (Transform go in tray.transform)
                {
                    go.position = tray.transform.position + new Vector3(0.0f, 0.3f, (i - 0.5f) * 0.6f);
                    ++i;
                }
            }
        }
    }

    public void AddSalad(List<GameObject> temp)
    {
        if (items.Count > 0)
            return;

        foreach(GameObject s in temp)
        {
            salad.Add(s);
        }

        foreach(GameObject child in salad)
        {
            child.transform.SetParent(tray.transform);
            child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, 0.0f);
        }
    }

    public void Clear()
    {
        items.Clear();
        salad.Clear();
    }
}
