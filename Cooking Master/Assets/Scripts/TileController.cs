using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour {

    public Material[] mat;
    public PlayerController playerController;
    public CustomerController customerController;
    public GameObject[] fruits;
    public Text[] scoreText;
    public Text[] userGuide;

    private string[] ctrlFuncs = { "E", "/" };
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
            && tiles[row+rowDelta][col+colDelta].go.CompareTag("Movable")
            && !(p == 0 && (row + rowDelta == 0) && (col + colDelta == 5 || col + colDelta == 6))
            && !(p == 1 && (row + rowDelta == 0) && (col + colDelta == 2 || col + colDelta == 3)))
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

            ItemsController obj = tiles[row][col].go.GetComponent<ItemsController>();
            if (obj != null)
            {
                switch (obj.type)
                {
                    case ItemType.Fruit:
                        userGuide[p].text = "Press " + ctrlFuncs[p] + " to pick fruit";
                        break;
                    case ItemType.Process:
                        userGuide[p].text = "Press " + ctrlFuncs[p] + " to process fruit";
                        break;
                    case ItemType.PickOrder:
                        userGuide[p].text = "Press " + ctrlFuncs[p] + " to pick order";
                        break;
                    case ItemType.Trash:
                        userGuide[p].text = "Press " + ctrlFuncs[p] + " to trash the items";
                        break;
                    case ItemType.Serve:
                        userGuide[p].text = "Press " + ctrlFuncs[p] + " serve the order";
                        break;
                    default:
                        userGuide[p].text = "";
                        break;
                }
            }
            else
            {
                userGuide[p].text = "";
            }
        }
    }

    void React(int row, int col, int p) {
        ItemsController obj = tiles[row][col].go.GetComponent<ItemsController>();
        if (obj != null)
        {
            switch (obj.type)
            {
                case ItemType.Fruit:
                    if (playerController.players[p].items.Count < 2 && playerController.players[p].salad.Count <= 0)
                    {
                        GameObject go = Instantiate(fruits[(int)obj.fruit]);
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
                        go = null;
                    }
                    break;

                case ItemType.Process:
                    if (playerController.players[p].items.Count > 0)
                    {
                        ProcessFruitController processFruitController = tiles[row][col].go.GetComponent<ProcessFruitController>();
                        if (processFruitController && !processFruitController.isProcesssing)
                        {
                            processFruitController.StartProcessing(playerController.players[p].items.Peek().go);
                            playerController.players[p].RemoveItem();
                        }
                    }
                    break;

                case ItemType.PickOrder:
                    PickOrderController pickOrderController = tiles[row][col].go.GetComponent<PickOrderController>();
                    if (pickOrderController && pickOrderController.processedFruit.Count>0 && playerController.players[p].items.Count <= 0)
                    {
                        playerController.players[p].AddSalad(pickOrderController.processedFruit);
                        pickOrderController.Clear();
                    }
                    break;

                case ItemType.Trash:
                    Trash(p);
                    break;

                case ItemType.Serve:
                    ServiceController serviceController = tiles[row][col].go.GetComponent<ServiceController>();
                    Debug.Log(CustomerController.isCustomerPresent[col - 1]);
                    if(serviceController && CustomerController.isCustomerPresent[col - 1] != -1)
                    {
                        CustomerSatisfaction customerSatisfaction = serviceController.CheckCombination(
                            customerController.customerList[CustomerController.isCustomerPresent[col-1]],
                            playerController.players[p].salad, p);

                        switch (customerSatisfaction)
                        {
                            case CustomerSatisfaction.angry:
                                playerController.players[p].score += 5;
                                Destroy(customerController.customerList[CustomerController.isCustomerPresent[col - 1]].go);
                                customerController.RemoveCustomer(CustomerController.isCustomerPresent[col - 1], col - 1);
                                Trash(p);
                                break;

                            case CustomerSatisfaction.veryAngry:
                                playerController.players[p].score -= 10;
                                break;

                            case CustomerSatisfaction.happy:
                                playerController.players[p].score += 20;
                                playerController.players[p].GiveBonusTime(2.0f, playerController.timerMaxVal);
                                Destroy(customerController.customerList[CustomerController.isCustomerPresent[col-1]].go);
                                customerController.RemoveCustomer(CustomerController.isCustomerPresent[col - 1], col - 1);
                                Trash(p);
                                break;

                            case CustomerSatisfaction.veryHappy:
                                playerController.players[p].score += 25;
                                playerController.players[p].GiveBonusTime(3.0f, playerController.timerMaxVal);
                                Destroy(customerController.customerList[CustomerController.isCustomerPresent[col - 1]].go);
                                customerController.RemoveCustomer(CustomerController.isCustomerPresent[col - 1], col - 1);
                                Trash(p);
                                break;
                        }
                        UpdateScores(p, playerController.players[p].score);
                    }
                    break;
            }
        }
    }

    void UpdateScores(int player, int score)
    {
        scoreText[player].text = "" + score;
    }

    void Trash(int p)
    {
        foreach (Transform child in playerController.players[p].tray.transform)
            Destroy(child.gameObject);
        playerController.players[p].Clear();
    }
}
