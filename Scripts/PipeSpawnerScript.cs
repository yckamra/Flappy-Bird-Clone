using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe;
    public float targetForTimer = 5;
    public float timer = 0;
    public float offset = 0;
    public bool spawnPipes = true;
    public List<GameObject> subscribedTo = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spawnPipes = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnPipes)
        {
            if (targetForTimer <= timer)
            {
                spawnPipe();
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
    private void spawnPipe()
    {
        float lowestPoint = transform.position.y - offset;
        float highestPoint = transform.position.y + offset;
        float randNum = Random.Range(lowestPoint, highestPoint);
        GameObject myPipe = Instantiate(pipe, new Vector3(transform.position.x, randNum), transform.rotation);

        PipeScript theScript = myPipe.GetComponent<PipeScript>();
        theScript.xPosition = theScript.transform.position.x;
        GameObject topPos = myPipe.transform.Find("Top Pipe Opening").gameObject;
        GameObject botPos = myPipe.transform.Find("Bottom Pipe Opening").gameObject;
        GameObject midPos = myPipe.transform.Find("Middle Pipe Opening").gameObject;
        theScript.topPipeOpeningY = topPos.transform.position.y;
        theScript.bottomPipeOpeningY = botPos.transform.position.y;
        theScript.middlePipeOpeningY = midPos.transform.position.y;

        subscribedTo.Add(myPipe);
    }
    public void removeObject(GameObject currObject)
    {
        subscribedTo.Remove(currObject);
    }
    public GameObject getClosestPipe(GameObject theBird)
    {
        for(int i = 0; i < subscribedTo.Count; i++)
        {
            if (subscribedTo[i].GetComponent<PipeScript>().transform.Find("Middle Pipe Opening").gameObject.transform.position.x < theBird.GetComponent<AIBirdScript>().transform.Find("Back Of Bird").gameObject.transform.position.x)
                continue;
            else
            {
                return subscribedTo[i];
            }
        }
        return null;
    }
}
