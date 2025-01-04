using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushesSpawnerScript : MonoBehaviour
{
    public GameObject bush;
    public float targetForTimer = 5;
    public float timer = 0;
    public bool spawnBushes = true;
    // Start is called before the first frame update
    void Start()
    {
        GameObject aBush = Instantiate(bush, new Vector3(0, transform.position.y, 7), transform.rotation);
        DontDestroyOnLoad(aBush);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnBushes)
        {
            if (targetForTimer <= timer)
            {
                spawnBush();
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            // do not spawn pipes
        }
    }
    private void spawnBush()
    {
        GameObject aBush = Instantiate(bush, new Vector3(transform.position.x, transform.position.y, 7), transform.rotation);
        DontDestroyOnLoad(aBush);
    }
}
