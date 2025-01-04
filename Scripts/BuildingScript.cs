using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public float xPosition;
    public float movementSpeed = 0;
    public float deadZone = -11;
    public bool moveBuildings = true;
    public BuildingSpawnerScript theBuildingSpawnerScript;
    // Start is called before the first frame update
    void Start()
    {
        moveBuildings = true;
        GameObject newBuildingSpawner = GameObject.FindGameObjectWithTag("Building Spawner");
        theBuildingSpawnerScript = newBuildingSpawner.GetComponent<BuildingSpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        xPosition = transform.position.x;

        if (moveBuildings)
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
