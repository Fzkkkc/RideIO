using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField] private Transform[] targetPositions;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float waypointThreshold = 1f;

    private int currentTargetIndex = 0;
    private bool isMoving = true;

    private void Update()
    {
        if (targetPositions.Length == 0)
        {
            return;
        }

        if (!isMoving)
            return;

        Vector3 targetPosition = targetPositions[currentTargetIndex].position;
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        if (direction.magnitude <= waypointThreshold)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length;
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void ResumeMoving()
    {
        isMoving = true;
    }
}