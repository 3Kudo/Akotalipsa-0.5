using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluff : MonoBehaviour
{
    public Transform waitPoint;
    int exist = 2;

    public void setPlace(Transform newWaitPoint)
    {
        waitPoint = newWaitPoint;
        transform.position = waitPoint.transform.position;
    }

    public void FadeAway()
    {
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a -= 0.3f;
        this.GetComponent<SpriteRenderer>().color = color;
        exist--;
        if (exist == 0)
        {
            GameRules.fluff.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }


}
