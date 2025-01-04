using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public float xPosition;
    public float topPipeOpeningY;
    public float bottomPipeOpeningY;
    public float middlePipeOpeningY;
    public float movementSpeed = 0;
    public float deadZone = -11;
    public bool movePipes = true;
    public PipeSpawnerScript thePipeSpawnerScript;
    // Start is called before the first frame update
    void Start()
    {
        movePipes = true;
        GameObject newSpawner = GameObject.FindGameObjectWithTag("Spawner");
        thePipeSpawnerScript = newSpawner.GetComponent<PipeSpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        xPosition = transform.position.x;
        
        if (movePipes)
        {
            if (transform.position.x <= deadZone)
            {
                notifySubscribers();
                Destroy(gameObject);

            }
            else
            {
                transform.position += Vector3.left * movementSpeed * Time.deltaTime;

            }
        }
        else
        {
            // do nothing
        }
    }
    void notifySubscribers()
    {
        thePipeSpawnerScript.removeObject(gameObject);
    }
}
