using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpongebobScript : MonoBehaviour
{
    public Rigidbody2D spongebobRigidBody;
    public float flapStrength;
    public LogicManagerScript logicScript;
    public AudioSource flapSound;
    public AudioSource dieSound;
    public AudioSource punchSound;
    public bool birdIsAlive = true;
    public Vector2 lastVelocity = new Vector2(0, 0);
    public float tiltMultiplier = 0;
    public bool paused = false;
    public GameObject pauseMenu;
    public GameObject gameOverObject;
    public GameObject pauseDescription;
    public PlayerListenerScript playerListener;
    

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1f;
        paused = false;
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        flapSound.volume = OptionsScript.Instance.SFXVolume;
        dieSound.volume = OptionsScript.Instance.SFXVolume;
        punchSound.volume = OptionsScript.Instance.SFXVolume;
        if (playerListener.paused)
        {

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
            {
                spongebobRigidBody.velocity = Vector2.up * flapStrength;
                flapSound.Play();
            }
        }
        birdTilt();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Perimeter"))
        {
            // do nothing
        }
        else
        {
            logicScript.gameOver();
            birdIsAlive = false;
            punchSound.Play();
            dieSound.Play();
            spongebobRigidBody.transform.Rotate(0, 0, -70);
        }
    }
    private void birdTilt()
    {
        if (spongebobRigidBody.velocity.y > 0) // we are going up
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion desiredRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 45f);
            spongebobRigidBody.transform.rotation = desiredRotation;
        }
        else // we are going down
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion desiredRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, spongebobRigidBody.velocity.y * tiltMultiplier);
            spongebobRigidBody.transform.rotation = desiredRotation;
        }
    }
}
