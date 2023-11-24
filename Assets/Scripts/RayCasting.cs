using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCasting : MonoBehaviour
{
    private Camera _mainCamera;
    private const int _raycastDistance = 100;
    private Rigidbody grabbdedObject = null;
    private float offset = 0.0f;
    private LayerMask layerGrabbable;

    void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        layerGrabbable = LayerMask.GetMask("Grabbable");
        Cursor.lockState = CursorLockMode.Confined;
    }

    void FixedUpdate()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * _raycastDistance, Color.yellow);

        if (grabbdedObject != null)
        {
            Vector3 newPosition = ray.origin + (ray.direction * offset);
            grabbdedObject.MovePosition(newPosition);
        }
    }

    void OnGrab(InputValue value)
    {
        if (value.isPressed)
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, _raycastDistance, layerGrabbable))
            {
                grabbdedObject = hit.rigidbody;
                offset = Vector3.Distance(ray.origin, hit.transform.position);
                grabbdedObject.isKinematic = true;
            }

        }
        else
        {
            if (grabbdedObject != null)
            {
                grabbdedObject.isKinematic = false;
                grabbdedObject = null;
            }
        }
    }

    public Rigidbody getGrabbedObject()
    {
        return grabbdedObject;
    }
}