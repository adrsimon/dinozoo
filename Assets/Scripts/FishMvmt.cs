using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMvmt : MonoBehaviour
{
    public float speed = 1f;
    public float radius = 0.1f; // Rayon du cercle dans lequel les poissons peuvent se déplacer
    public Vector3 bowlCenter = Vector3.zero; // Centre du "bocal"
    public float minThreshold = 0.1f;
    public float maxThreshold = 1f;
    public float targetHeight = -1f;

    private Rigidbody appat;

    private Vector3 targetPosition;
    private float targetThreshold;

    private bool fished = false;
    private bool grabbed = false;

    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        targetThreshold = Random.Range(minThreshold, maxThreshold);
        ChangeTargetPosition();
        fished = false;
    }

    private void Update()
    {
        if(!fished)
        {
            // si le poisson n'est pas péché il se déplace comme il le souhaite
            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance < targetThreshold)
            {
                ChangeTargetPosition();
            }
            else
            {
                MoveTowardsTarget();
            }
        }else if (!grabbed)
        {
            // Si il est péché, il est attaché à l'appat
            transform.position = appat.position;
        } else if (grabbed)
        {
            Debug.Log("je me détache !!");
        }
    }

    // Change l'etat du poisson en attrapé
    public void SetGrabbed()
    {
        Debug.Log("grabbé !");
        grabbed = true;
        appat = null;
        GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // en cas de collision avec un appat, il est mis à péché = true.
        if (collision.CompareTag("Appat") && !fished)
        {
            Debug.Log("collision !");
            fished = true;
            appat = collision.GetComponent<Rigidbody>();
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    void ChangeTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized * radius;
        targetPosition = bowlCenter + randomDirection;
        targetPosition.y = targetHeight;
        targetThreshold = Random.Range(minThreshold, maxThreshold);
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        LookAtTarget(direction);
    }

    void LookAtTarget(Vector3 direction) 
    {
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            toRotation *= Quaternion.Euler(0, 90, 0); // Ajoute une rotation de 90 degrés autour de l'axe Y
            float rotationSpeed = 5f;
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
