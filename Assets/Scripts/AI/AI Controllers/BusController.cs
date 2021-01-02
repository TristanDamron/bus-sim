using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BusController : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private Transform _point;
    [SerializeField]
    private List<Transform> _points;   
    [SerializeField] 
    private int _busStopIndex;
    private bool _checkingDestinationReached;
    
    void Awake()
    {
        _checkingDestinationReached = false;
        var stops = GameObject.FindGameObjectsWithTag("Bus Stop");
        foreach (GameObject stop in stops) {
            _points.Add(stop.transform);
        }

        _busStopIndex = Random.Range(0, _points.Count);
        _agent = GetComponent<NavMeshAgent>();
        if (_points.Count >= 2) {
            _agent.SetDestination(_points[_busStopIndex].position);        
        }
    }
    
    void Update()
    {
        var stops = GameObject.FindGameObjectsWithTag("Bus Stop");
        _points.Clear();
        foreach (GameObject stop in stops) {
            _points.Add(stop.transform);
        }

        if (!_checkingDestinationReached)
            StartCoroutine(ChooseNextStop());
    }


    IEnumerator ChooseNextStop() {
        _checkingDestinationReached = true;
        if ((_agent.remainingDistance <= _agent.stoppingDistance) ||  _agent.isStopped && _points.Count >= 2) {
            var rand = Random.Range(0, _points.Count);
            if (rand != _busStopIndex) {
                _busStopIndex = rand;
                yield return new WaitForSeconds(1f);
                _agent.SetDestination(_points[_busStopIndex].position);        
            }
        }
        _checkingDestinationReached = false;
    }
}
