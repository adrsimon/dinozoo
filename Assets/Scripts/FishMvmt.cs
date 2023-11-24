using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMvmt : MonoBehaviour
{
    public float speed = 1f;
    public float radius = 6f;
    public float minThreshold = 0.1f;
    public float maxThreshold = 1f;
    public float targetHeight = -1f;

    private Vector3 targetPosition;
    private float targetThreshold;

    void Start()
    {
        ChangeTargetPosition();
        targetThreshold = Random.Range(minThreshold, maxThreshold);
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
        targetPosition = transform.position + Random.insideUnitSphere * radius;
        targetPosition.y = targetHeight;
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
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, speed * Time.deltaTime);
        }
    }
}