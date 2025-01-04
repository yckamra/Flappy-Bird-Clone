using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    public AudioSource highlightButton1;
    public AudioSource highlightButton2;
    public AudioSource highlightButton3;
    public AudioSource highlightButton4;
    public AudioSource highlightButton5;
    public GameObject pauseMenu;

    void Update()
    {

        highlightButton1.volume = OptionsScript.Instance.SFXVolume;
        highlightButton2.volume = OptionsScript.Instance.SFXVolume;
        highlightButton3.volume = OptionsScript.Instance.SFXVolume;
        highlightButton4.volume = OptionsScript.Instance.SFXVolume;
        highlightButton5.volume = OptionsScript.Instance.SFXVolume;

    }

    public void PlayGame()
    {

        SceneManager.LoadScene("SampleScene");
    }
    public void TrainAI()
    {
        SceneManager.LoadScene("TrainNN");
    }
    public void toMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "TrainNN")
        {
            Debug.Log("Leaving Train NN");
            Destroy(GameObject.Find("Game Manager"));
        }
    }
    public void toOptions()
    {
        SceneManager.LoadScene("Options");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PointerEntersButton1()
    {
        highlightButton1.Play();
    }
    public void PointerEntersButton2()
    {
        highlightButton2.Play();
    }
    public void PointerEntersButton3()
    {
        highlightButton3.Play();
    }
    public void PointerEntersButton4()
    {
        highlightButton4.Play();
    }
    public void PointerEntersButton5()
    {
        highlightButton5.Play();
    }
}