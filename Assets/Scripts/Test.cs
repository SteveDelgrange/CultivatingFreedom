using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int numberOfIterations = 100000;
       
    public void Execute()
    {
        float tstart, tend;

        //Calculating the time of doing the iterations
        tstart = Time.realtimeSinceStartup;
        for (int i = 0; i < numberOfIterations; i++) {}
        tend = Time.realtimeSinceStartup;
        float iterationTime = tend - tstart;

        tstart = Time.realtimeSinceStartup;
        for (int i = 0; i < numberOfIterations; i++) {

        }
        tend = Time.realtimeSinceStartup;
        Debug.Log((tend - tstart).ToString("0.000000"));
    }

}
