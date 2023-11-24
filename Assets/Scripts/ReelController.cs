using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    public Rigidbody Appat;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnReel() 
    {
        // get grabbed objectz
        Rigidbody grabbedObject = GetComponent<RayCasting>().getGrabbedObject();
        if (grabbedObject && grabbedObject.CompareTag("Rod")) {
            Appat.transform.position = Vector3.MoveTowards(Appat.transform.position, transform.position, 0.1f);
        }
    }
}
