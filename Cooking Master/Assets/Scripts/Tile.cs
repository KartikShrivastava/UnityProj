using UnityEngine;

public class Tile {
    public GameObject go;
    public bool isOccupied;
    public int occupyingPlayer;

    public Tile() {
        go = null;
        isOccupied = false;
        occupyingPlayer = -1;
    }
}
