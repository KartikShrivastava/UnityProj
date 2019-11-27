using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZoneMovements : MonoBehaviour {

    public Material[] mat;

    private List<List<Tile>> tiles;
    private int numRows, numColumns;

    private List<Player> players;   //list of players

    void Start() {
        //storing reference of each tile for player navigation
        tiles = new List<List<Tile>>(this.transform.childCount);
        for (int i= 0; i < this.transform.childCount; ++i) {

            int size = this.transform.GetChild(i).childCount;
            List<Tile> t = new List<Tile>();

            for(int j=0; j < size; ++j) {
                Tile tile = new Tile();
                tile.go = this.transform.GetChild(i).GetChild(j).gameObject;
                t.Add(tile);
            }

            tiles.Add(t);
        }
        numRows = tiles.Count;
        numColumns = tiles[0].Count;

        //setting up players
        players = new List<Player>();
        Player p0 = new Player();
        p0.row = numRows / 2;
        p0.col = 0;
        p0.material = mat[0];
        players.Add(p0);

        Player p1 = new Player();
        p1.row = numRows / 2;
        p1.col = numColumns - 1;
        p1.material = mat[1];
        players.Add(p1);
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
    }

    void MoveLeft(int p) {
        Move(p, players[p].row, players[p].col - 1);
    }

    void MoveRight(int p) {
        Move(p, players[p].row, players[p].col + 1);
    }

    void MoveUp(int p) {
        Move(p, players[p].row - 1, players[p].col);
    }

    void MoveDown(int p) {
        Move(p, players[p].row + 1, players[p].col);
    }

    void Move(int p, int row, int col) {
        if ((row >= 0 && row < numRows) && (col>=0 && col < numColumns) && !tiles[row][col].isOccupied) {
            tiles[players[p].row][players[p].col].go.GetComponent<MeshRenderer>().material = mat[mat.Length-1];
            tiles[players[p].row][players[p].col].isOccupied = false;
            tiles[players[p].row][players[p].col].occupyingPlayer = -1;
            players[p].row = row;
            players[p].col = col;
            tiles[players[p].row][players[p].col].go.GetComponent<MeshRenderer>().material = players[p].material;
            tiles[players[p].row][players[p].col].isOccupied = true;
            tiles[players[p].row][players[p].col].occupyingPlayer = p;
        }
    }
}