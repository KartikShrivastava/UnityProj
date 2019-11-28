using System.Collections.Generic;
using UnityEngine;

public class Player {

    public int row=0, col=0;
    public Material material;
    public float timeRemaining;
    public GameObject timeBar;
    public GameObject tray;
    public Queue<Items> items;

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
        if (go != null)
        {
            items.Enqueue(new Items(type, name, go));
            go.transform.position = tray.transform.position + new Vector3(0.0f, 0.3f, (tray.transform.childCount - 0.5f) * 0.6f);
            go.transform.SetParent(tray.transform);
        }
    }

    public void RemoveItem() {
        items.Dequeue();
        if (tray.transform.childCount > 0)
        {
            int i = -1;
            foreach (Transform go in tray.transform)
            {
                go.position = tray.transform.position + new Vector3(0.0f, 0.3f, (i - 0.5f) * 0.6f);
                ++i;
            }
        }
    }

}
