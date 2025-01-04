using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushScript : MonoBehaviour
{
    public float xPosition;
    public float movementSpeed = 0;
    public float deadZone = -11;
    public bool moveBushes = true;
    public BushesSpawnerScript theBushesSpawnerScript;
    // Start is called before the first frame update
    void Start()
    {
        moveBushes = true;
        GameObject newBushesSpawner = GameObject.FindGameObjectWithTag("Bush Spawner");
        theBushesSpawnerScript = newBushesSpawner.GetComponent<BushesSpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        xPosition = transform.position.x;

        if (moveBushes)
        {
            if (transform.position.x <= deadZone)
            {
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
}
