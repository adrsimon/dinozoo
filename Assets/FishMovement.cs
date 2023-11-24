using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 5f;
    public float targetChangeTime = 5f;
    public float radius = 10f;

    private Vector3 targetPosition;
    private float timer;

    void Start()
    {
        ChangeTargetPosition();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= targetChangeTime)
        {
            ChangeTargetPosition();
            timer = 0;
        }

        MoveTowardsTarget();
    }

    void ChangeTargetPosition()
    {
        targetPosition = transform.position + Random.insideUnitSphere * radius;
        targetPosition.y = transform.position.y; // Keep the fish at the same height
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        LookAtTarget();
    }

    void LookAtTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }
}

