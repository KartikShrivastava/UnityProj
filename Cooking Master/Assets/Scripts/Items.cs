using UnityEngine;

public enum ItemType {
    Fruit, PickOrder, Serve, Trash, Process
}

public enum FruitName {
    Apple, Banana, Watermelon, Avocaado, Cherry, Strawberry, None
}

public class Items {
    public ItemType type;
    public FruitName name;
    public GameObject go;

    public Items(ItemType t, FruitName n, GameObject g) {
        type = t;
        name = n;
        go = g;
    }
}
