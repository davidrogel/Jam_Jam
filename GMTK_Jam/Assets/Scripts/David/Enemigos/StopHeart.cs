using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopHeart : MonoBehaviour
{
    public Behaviour[] behaviours;

    public void StopIt()
    {
        foreach(Behaviour behaviour in behaviours)
        {
            behaviour.enabled = false;
        }
    }
}
