using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _cameraZoomSpeed;
    [SerializeField]
    private float _cameraPanSpeed;
    [SerializeField]
    private GameObject _busStop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        transform.position = Vector3.Lerp(transform.position,
                                          new Vector3(transform.position.x + (horizontal * _cameraZoomSpeed), transform.position.y + (vertical * _cameraPanSpeed), transform.position.z + ((Input.mouseScrollDelta.x + Input.mouseScrollDelta.y) * _cameraZoomSpeed)),
                                          Time.deltaTime);

        if (Input.GetMouseButtonDown(0)) {
            CreateBusStop();
        } 

        if (Input.GetMouseButtonDown(1)) {
            RemoveBusStop();
        }
    }

    private void CreateBusStop() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.tag == "Road") {
                var stop = Instantiate(_busStop);
                stop.transform.parent = hit.transform.parent;
                stop.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.01f);                
                stop.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);                        
            }
        }
    }

    private void RemoveBusStop() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.tag == "Bus Stop") {
                Destroy(hit.transform.gameObject);                        
            }
        }
    }    
}
