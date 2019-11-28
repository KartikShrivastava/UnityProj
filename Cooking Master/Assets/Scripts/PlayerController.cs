using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public List<Player> players;   //list of players
    public float timerMaxVal = 20.0f;
    public GameObject[] timeBarObject;
    public GameObject trayGO;

    private float timeBarMaxSz;

    void Start() {
        timeBarMaxSz = timeBarObject[0].transform.GetChild(0).localScale.y;
        InvokeRepeating("UpdateTime", 1.0f, 0.5f);
    }

    public void InitializePlayers(int numRows, int numColumns, Material[] mat) {         
        //setting up players
        players = new List<Player>();

        GameObject obj = Instantiate(trayGO, new Vector3(-3.9f, 0.629f, 2.6f), Quaternion.identity);
        Player p0 = new Player(numRows / 2, 1, mat[0], timerMaxVal, timeBarObject[0], obj);
        players.Add(p0);

        obj = Instantiate(trayGO, new Vector3(3.9f, 0.629f, 2.6f), Quaternion.identity);
        Player p1 = new Player(numRows / 2, numColumns - 2, mat[1], timerMaxVal, timeBarObject[1], obj);
        players.Add(p1);
    }

    void UpdateTime() {
        foreach(Player player in players) {
            player.timeRemaining -= 0.1f;
            if(player.timeRemaining < 0.0f) {
                CancelInvoke();
                player.timeBar.SetActive(false);
            }
            Vector3 scale = player.timeBar.transform.GetChild(0).localScale;
            scale.y = (timeBarMaxSz / timerMaxVal) * player.timeRemaining;
            player.timeBar.transform.GetChild(0).localScale = scale;
        }
    }
}
