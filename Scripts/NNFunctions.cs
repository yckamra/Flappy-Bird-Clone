using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NNFunctions
{ 
    private static System.Random rand = new System.Random();

    public static float Sig(float X)//DONE
    {
        float returnValue = 1 / (1 + Mathf.Exp(-1 * X));
        return returnValue;
    }

    public static float Relu(float X)//DONE
    {
        if (X > 0f)
        {
            return X;
        }
        else
        {
            return 0;
        }
    }

    public static float randomGausNum(float mean = 0f, float stdDev = 1f) // DONE
    {
        double randNormal = 0;

        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2);
        randNormal = mean + stdDev * randStdNormal;


        return (float)randNormal;
    }
}
