using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ball : MonoBehaviour
{
    public static bool complete;
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "進球感應")
        {
            complete = true;
        }
    }
}
