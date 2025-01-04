using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    OptionsScript options = OptionsScript.Instance;
    

    void Start()
    {
        if(gameObject.CompareTag("Music Slider"))
        {
            gameObject.GetComponent<Slider>().value = options.musicVolume;

        }
        else
        {
            gameObject.GetComponent<Slider>().value = options.SFXVolume * 100;

        }
    }

    public void changeMusicVolume()
    {
        options.changeMusicVolume(gameObject.GetComponent<Slider>());
    }
    public void changeSFXVolume()
    {
        options.changeSFXVolume(gameObject.GetComponent<Slider>());
    }
}
