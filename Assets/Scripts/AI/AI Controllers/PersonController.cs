using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonController : MonoBehaviour
{
    [SerializeField]
    private Goals.Destination _destination;
    [SerializeField]
    private Goals.Task _task;
    [SerializeField]
    private Goals.Status _status;
    [SerializeField]
    private Vector3 _destinationPoint;
    private Transform _bus;
    private bool _onBus;
    private NavMeshAgent _agent;

    void Start()
    {
        _destination = Goals.Destination.Core;    
        _task = Goals.Task.Home;
        _status = Goals.Status.Todo;
        _agent = GetComponent<NavMeshAgent>();
        _onBus = false;
        // FindDestinationPoint();
    }
    
    void Update()
    {
        if (_destinationPoint == Vector3.zero && GameObject.FindGameObjectsWithTag("Bus Stop").Length > 0) {
            FindDestinationPoint();
        }   

        if (_onBus) {
            transform.localPosition = Vector3.zero;
        }
    }

    private void FindDestinationPoint() {
        var min = Mathf.Infinity;
        GameObject stop = gameObject;
        foreach (GameObject busStop in GameObject.FindGameObjectsWithTag("Bus Stop")) {
            if (Vector3.Distance(transform.localPosition, busStop.transform.localPosition) < min) {
                min = Vector3.Distance(transform.localPosition, busStop.transform.localPosition);
                stop = busStop;
            }
        }
        Debug.Log("Stop found! " + min + " units away");
        _agent.SetDestination(stop.transform.position);
        _destinationPoint = stop.transform.position;
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Bus Stop") {
            if (c.gameObject.GetComponent<BusStop>().GetDestination() == _destination) {
                Destroy(gameObject);
            } else {
                _agent.isStopped = true;
            }
        } else if (c.tag == "Bus" && !_onBus && _agent.isStopped) {
            _onBus = true;
            _bus = c.gameObject.transform;
            transform.parent = _bus;   
        }
    }
}
