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

    private Vector3 targetPosition;
    private float targetThreshold;

    void Start()
    {
        targetThreshold = Random.Range(minThreshold, maxThreshold);
        ChangeTargetPosition();
    }

    void Update()
    {
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
