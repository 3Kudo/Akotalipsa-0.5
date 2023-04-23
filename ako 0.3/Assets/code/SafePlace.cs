using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePlace : MonoBehaviour
{
    public Transform waitPoint;

    public void setPlace(Transform newWaitPoint)
    {
        waitPoint = newWaitPoint;
        transform.position = waitPoint.transform.position;
    }

}
