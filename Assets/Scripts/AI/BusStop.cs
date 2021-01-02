using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStop : MonoBehaviour
{
    [SerializeField]
    private Goals.Destination _destination;

    void OnTriggerStay(Collider c) {
        if (c.tag == "Downtown") {
            _destination = Goals.Destination.Downtown;
        } else if (c.tag == "Temple Heights") {
            _destination = Goals.Destination.Temple;
        } else if (c.tag == "Historic Core") {
            _destination = Goals.Destination.Core;
        }        
    }
    
    public Goals.Destination GetDestination() {
        return _destination;
    }
}
