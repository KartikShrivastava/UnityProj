using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    public Material[] mat;
    public PlayerController playerController;
    public GameObject[] fruits;

    private List<List<Tile>> tiles;
    private int numRows, numColumns;

    void Start() {
        //storing reference of each tile for player navigation
        tiles = new List<List<Tile>>(this.transform.childCount);
        for (int i= 0; i < this.transform.childCount; ++i) {

            int size = this.transform.GetChild(i).childCount;
            List<Tile> t = new List<Tile>();

            for(int j=0; j < size; ++j) {
                Tile tile = new Tile
                {
                    go = this.transform.GetChild(i).GetChild(j).gameObject
                };
                t.Add(tile);
            }

            tiles.Add(t);
        }
        numRows = tiles.Count;
        numColumns = tiles[0].Count;

        playerController.InitializePlayers(numRows, numColumns, mat);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            MoveLeft(0);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            MoveUp(0);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            MoveRight(0);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            MoveDown(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
            React(playerController.players[0].row, playerController.players[0].col, 0);

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveLeft(1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            MoveUp(1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveRight(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            MoveDown(1);
        }
        if (Input.GetKeyDown(KeyCode.Slash))
            React(playerController.players[1].row, playerController.players[1].col, 1);
    }

    void MoveLeft(int p) {
        Move(p, playerController.players[p].row, 0, playerController.players[p].col, -1);
    }

    void MoveRight(int p) {
        Move(p, playerController.players[p].row, 0, playerController.players[p].col, 1);
    }

    void MoveUp(int p) {
        Move(p, playerController.players[p].row, -1, playerController.players[p].col, 0);
    }

    void MoveDown(int p) {
        Move(p, playerController.players[p].row, 1, playerController.players[p].col, 0);
    }

    void Move(int p, int row, int rowDelta, int col, int colDelta) {
        if ((row+rowDelta >= 0 && row+rowDelta < numRows) && (col+colDelta>=0 && col+colDelta < numColumns) 
            && !tiles[row+rowDelta][col+colDelta].isOccupied 
            && tiles[row+rowDelta][col+colDelta].go.CompareTag("Movable"))
        {

            tiles[row][col].go.GetComponent<MeshRenderer>().material = mat[mat.Length-1];
            tiles[row][col].isOccupied = false;
            tiles[row][col].occupyingPlayer = -1;

            row = row + rowDelta;
            col = col + colDelta;
            playerController.players[p].row = row;
            playerController.players[p].col = col;

            playerController.players[p].tray.transform.position = new Vector3(tiles[row][col].go.transform.position.x, 0.629f,
                tiles[row][col].go.transform.position.z);

            tiles[row][col].go.GetComponent<MeshRenderer>().material = playerController.players[p].material;
            tiles[row][col].isOccupied = true;
            tiles[row][col].occupyingPlayer = p;
        }
    }

    void React(int row, int col, int p) {
        ItemsController obj = tiles[row][col].go.GetComponent<ItemsController>();
        if (obj != null)
        {
            switch (obj.type)
            {
                case ItemType.Fruit:
                    GameObject go = null;
                    if (playerController.players[p].items.Count < 4)
                         go = Instantiate(fruits[(int)obj.fruit]);
                    switch (obj.fruit)
                    {
                        case FruitName.Apple:
                            playerController.players[p].AddItem(obj.type, obj.fruit, go);
                            break;
                        case FruitName.Banana:
                            playerController.players[p].AddItem(obj.type, obj.fruit, go);
                            break;
                        case FruitName.Watermelon:
                            playerController.players[p].AddItem(obj.type, obj.fruit, go);
                            break;
                        case FruitName.Cherry:
                            playerController.players[p].AddItem(obj.type, obj.fruit, go);
                            break;
                        case FruitName.Avocaado:
                            playerController.players[p].AddItem(obj.type, obj.fruit, go);
                            break;
                        case FruitName.Strawberry:
                            playerController.players[p].AddItem(obj.type, obj.fruit, go);
                            break;
                    }
                    break;
                case ItemType.Process:
                    if (playerController.players[p].items.Count > 0)
                    {
                        Destroy(playerController.players[p].items.Peek().go);
                        playerController.players[p].RemoveItem();
                    }
                    break;
            }
        }
    }
}