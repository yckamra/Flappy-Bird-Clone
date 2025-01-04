using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListenerScript : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    public GameObject currentScore;
    public GameObject optionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                // do nothing the button was pressed and will do the rest for us
            }
            else
            {
                pauseMenu.SetActive(true);
                currentScore.SetActive(false);
                Debug.Log("pausing game");
                Time.timeScale = 0f;
                paused = true;
            }
        }
    }
    public void backToGame()
    {
        pauseMenu.SetActive(false);
        currentScore.SetActive(true);
        Debug.Log("unpausing game");
        Time.timeScale = 1f;
        paused = false;
    }
    public void toOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);

    }
    public void leaveOptions()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
