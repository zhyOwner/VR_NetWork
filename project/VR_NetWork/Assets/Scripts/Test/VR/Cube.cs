using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : BaseEvent
{

    public override void TriggerClick(string msg)
    {
        Debug.Log(msg + " : " + gameObject.name);
    }
}
