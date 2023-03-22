using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform pointA; // The starting point
    public Transform pointB; // The ending point
    public float speed = 2f; // The speed of movement
    public float interval = 2f; // The time interval between movements
    private float timeSinceLastMove = 0f; // The time since the last movement

    private Vector3 nextPosition; // The next position to move to

    public bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = pointA.position; // Start at point A
    }

    // Update is called once per frame
    void Update()
    {
        // Update the time since the last move
        timeSinceLastMove += Time.deltaTime;

        // Check if it's time to move
        if (timeSinceLastMove >= interval)
        {
            // Move to the next position
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

            // Check if we've reached the next position
            if (transform.position == nextPosition)
            {
                // Switch to the other point
                nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;

                // Reset the time since the last move
                timeSinceLastMove = 0f;
            }
        }
    }

    // Called when the player touches this enemy
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ignore"))
    {
        // Destroy this game object
        Destroy(gameObject);
    }
    }
}
