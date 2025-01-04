using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBirdScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject closestPipe;
    public GameObject thePipeSpawner;
    public PipeSpawnerScript thePipeSpawnerScript;
    public float flapStrength = 12;
    public NeuralNetworkAgentScript theAgentScript;
    public float timeAlive = 0;
    public bool isDead = false;
    public Rigidbody2D theBirdRigidBody;
    public float tiltMultiplier = 3;
    public GameObject theBirdSpawner;
    public float middleOfTheClosestPipe;
    public float yHeightAtDeath;
    public float fitness;
    public float yVelocityOfBird;
    public float xPositionOfPipe;
    public float yTopOfPipe;
    public float yBottomOfPipe;
    public float birdYPosition;

    void Start()
    {
        GameObject newSpawner = GameObject.FindGameObjectWithTag("BirdSpawner");
        theBirdSpawner = newSpawner;
        thePipeSpawnerScript = thePipeSpawner.GetComponent<PipeSpawnerScript>();
        closestPipe = thePipeSpawnerScript.getClosestPipe(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isDead) // the bird is not dead yet
        {
            timeAlive += Time.deltaTime;

            closestPipe = thePipeSpawnerScript.getClosestPipe(gameObject);


            if (closestPipe != null) // this will be null at the beginning before pipes have spawned
            {
                PipeScript pipeScript = closestPipe.GetComponent<PipeScript>();

                // Parameters for the neural network
                this.xPositionOfPipe = pipeScript.GetComponent<PipeScript>().transform.Find("Middle Pipe Opening").gameObject.transform.position.x;
                this.yTopOfPipe = pipeScript.topPipeOpeningY;
                this.yBottomOfPipe = pipeScript.bottomPipeOpeningY;
                this.birdYPosition = gameObject.transform.position.y;
                yVelocityOfBird = gameObject.GetComponent<Rigidbody2D>().velocity.y;
                this.middleOfTheClosestPipe = pipeScript.middlePipeOpeningY;

                if (theAgentScript != null)
                {
                    bool shouldFlap = theAgentScript.flap(xPositionOfPipe, yTopOfPipe, yBottomOfPipe, birdYPosition, yVelocityOfBird);

                    if (shouldFlap)
                    {
                        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * flapStrength; // for flapping
                    }
                    else
                    {
                        // don't flap
                    }
                }
                else
                {
                    // do nothing if there is no NN agent attached to bird
                    Debug.Log("No neural network attached to bird");
                }
            }
            birdTilt();
        }
        else // the bird is dead
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("AIBird"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if(collision.collider.CompareTag("Pipe"))
        {
            this.yHeightAtDeath = theBirdRigidBody.transform.position.y;
            calculateFitness();
            this.isDead = true;
            theBirdRigidBody.transform.Rotate(0, 0, -70);
            BirdSpawnerScript theBirdSpawnerScript = theBirdSpawner.GetComponent<BirdSpawnerScript>();
            theBirdSpawnerScript.removeFromAliveBirdList(gameObject);
        }
        else
        {

        }
    }
    private void birdTilt()
    {
     
        if (theBirdRigidBody.velocity.y > 0) // we are going up
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion desiredRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 45f);
            theBirdRigidBody.transform.rotation = desiredRotation;
        }
        else // we are going down
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion desiredRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, theBirdRigidBody.velocity.y * tiltMultiplier);
            theBirdRigidBody.transform.rotation = desiredRotation;
        }
    }

    public void calculateFitness()
    {
        float theFitness = timeAlive * 100f;
        float heightDifference = this.yHeightAtDeath - middleOfTheClosestPipe;
        if(heightDifference < 0f)
        {
            heightDifference *= -1f;
        }
        theFitness -= (heightDifference / 10);
        this.fitness = theFitness;
    }
}
