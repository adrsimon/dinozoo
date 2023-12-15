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

    public bool grabbed = false;
    private bool attachedToRod = false;
    private Rigidbody appat;

    private Vector3 targetPosition;
    private float targetThreshold;

    void Start()
    {
        targetThreshold = Random.Range(minThreshold, maxThreshold);
        ChangeTargetPosition();
    }

    void Update()
    {
        if (this.grabbed) {
            return;
        }

        if (this.attachedToRod)
        {
            transform.position = appat.position;
        }

        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < targetThreshold)
        {
            ChangeTargetPosition();
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Appat") && !this.grabbed)
        {
            SetAttachedToRod(true);
            appat = collision.collider.GetComponent<Rigidbody>();
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

    public void SetAttachedToRod(bool value)
    {
        attachedToRod = true;
    }

    public void SetGrabbed(bool value)
    {
        attachedToRod = false;
        grabbed = value;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
