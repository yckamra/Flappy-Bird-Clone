using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingScript : MonoBehaviour
{
    public float timeUntilFlap = 1f;
    public float counter = 0f;
    public float tiltDegree = 10;
    public bool keepFlapping = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(counter >= timeUntilFlap)
        {
            if (keepFlapping)
            {
                tiltWing();
            }
            else
            {
                Quaternion currentRotation = transform.rotation;
                Quaternion desiredRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 0);
                gameObject.transform.rotation = desiredRotation;
            }
            counter = 0f;
        }
        else
        {
            counter += Time.deltaTime;
        }
    }
    private float getTiltOfWing()
    {
        float currentZ = gameObject.transform.rotation.z;
        return currentZ;
    }
    private void tiltWing()
    {
        float currentTilt = getTiltOfWing();
        if(currentTilt < 0)
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion desiredRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, tiltDegree);
            gameObject.transform.rotation = desiredRotation;
        }
        else
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion desiredRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, tiltDegree * -1f);
            gameObject.transform.rotation = desiredRotation;
        }
    }
}
