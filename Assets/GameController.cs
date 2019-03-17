using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public GameObject germ;
    public Vector3 spawnValues;

    public int germCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        UpdateScore();
        StartCoroutine (spawnWaves());   
    }

    void Update()
    {
         if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator spawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for (int i = 0; i < germCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(germ, spawnPosition, germ.transform.rotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restart = true;
                restartText.text = "Press 'R' for Restart";
                break;
            }
        }
    }

    public void addScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", 10);
        form.AddField("score", score);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/score", form);

        www.SendWebRequest();

        gameOver = true;
        gameOverText.text = "Game Over!";
    }
    
}
