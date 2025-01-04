using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManagerScript : MonoBehaviour
{
    public WingScript wingScript;
    public AudioSource pointSound;
    public int playerScore;
    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverObject;
    public PipeSpawnerScript pipeSpawner;
    public int highScore;
    private const string highScoreKey = "highScore";
    public GameObject pauseDescription;
    public GameManagerScript gameManagerScript;
    public BirdSpawnerScript theBirdSpawnerScript;
    public List<GameObject> theLeaderboard;

    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highScoreText.text = highScore.ToString();
        this.gameManagerScript = GameManagerScript.Instance;

    }
    void Update()
    {
        pointSound.volume = OptionsScript.Instance.SFXVolume;
    }

    [ContextMenu("Increase Score")]
    public void addScore()
    {
        pointSound.Play();
        playerScore++;
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "TrainNN")
        {
            if (playerScore > highScore)
            {
                PlayerPrefs.SetInt(highScoreKey, playerScore);
                highScore = playerScore;
            }
        }
        scoreText.text = playerScore.ToString();
        highScoreText.text = highScore.ToString();
    }
    public void gameOver()
    {
        gameOverObject.SetActive(true);
        pauseDescription.SetActive(false);
        pipeSpawner.spawnPipes = false;
        wingScript.keepFlapping = false;

        foreach (GameObject i in pipeSpawner.subscribedTo)
        {
            Transform bottomPipe = i.transform.Find("Bottom Pipe");
            Transform topPipe = i.transform.Find("Top Pipe");
            BoxCollider2D bottomCollider = bottomPipe.GetComponent<BoxCollider2D>();
            BoxCollider2D topCollider = topPipe.GetComponent<BoxCollider2D>();
            bottomCollider.enabled = false;
            topCollider.enabled = false;
            PipeScript newScript = i.GetComponent<PipeScript>();
            newScript.movePipes = false;
        }
        
    }
    public void gameOverForNN()
    {
        pipeSpawner.spawnPipes = false;
        gameManagerScript.addGeneration();
        gameManagerScript.generationCounter += 1;


        theBirdSpawnerScript.QuickSortFromFitness(theBirdSpawnerScript.subscribedTo, 0, theBirdSpawnerScript.subscribedTo.Count - 1);
        theLeaderboard = theBirdSpawnerScript.subscribedTo;
        theLeaderboard.Reverse();


        gameManagerScript.AddParentsToWinningParents(theLeaderboard);

        if(GameManagerScript.Instance.topBirdSoFar == null)
        {
            GameManagerScript.Instance.topBirdSoFar = theLeaderboard[0].GetComponent<AIBirdScript>().theAgentScript;
            GameManagerScript.Instance.topScoreSoFar = theLeaderboard[0].GetComponent<AIBirdScript>().fitness;

        }
        else
        {
            if(GameManagerScript.Instance.topScoreSoFar < theLeaderboard[0].GetComponent<AIBirdScript>().fitness)
            {
                GameManagerScript.Instance.topBirdSoFar = theLeaderboard[0].GetComponent<AIBirdScript>().theAgentScript;
                GameManagerScript.Instance.topScoreSoFar = theLeaderboard[0].GetComponent<AIBirdScript>().fitness;
            }
        }
        Debug.Log("the top fitness is: " + GameManagerScript.Instance.topScoreSoFar);
        restart();

    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
