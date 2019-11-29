using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public List<Player> players;   //list of players
    public float timerMaxVal = 20.0f;
    public GameObject[] timeBarObject;
    public GameObject trayGO;
    public GameObject endCanvas;
    public Text winningText;
    public Text score;
    public Image winninColor;

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

        for(int i=0; i< players.Count; ++i) {
            players[i].timeRemaining -= 0.1f;
            if(players[i].timeRemaining < 0.0f) {
                CancelInvoke();
                players[i].timeBar.SetActive(false);
                QuitGame(i);
            }
            Vector3 scale = players[i].timeBar.transform.GetChild(0).localScale;
            scale.y = (timeBarMaxSz / timerMaxVal) * players[i].timeRemaining;
            players[i].timeBar.transform.GetChild(0).localScale = scale;
        }
    }

    void QuitGame(int p)
    {
        endCanvas.SetActive(true);
        winningText.text = "Its all " + (p == 1 ? "VOILET" : "GREEN") + " this time.";
        winninColor.color = p == 1 ? new Color32(155, 34, 211, 255) : new Color32(150, 210, 34, 255);
        score.text = players[0].score + "                    " + players[1].score;
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
