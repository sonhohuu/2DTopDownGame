using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    public float homingDuration = 2f;
    private float homingTimeElapsed = 0f;

    public void Initialize(Transform playerTransform)
    {
        target = playerTransform;
    }

    private void Update()
    {
        if (target != null && homingTimeElapsed < homingDuration)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Flip the bullet sprite based on the direction
            SpriteRenderer bulletSpriteRenderer = GetComponent<SpriteRenderer>();
            if (direction.x < 0)
            {
                bulletSpriteRenderer.flipX = true;
            }
            else
            {
                bulletSpriteRenderer.flipX = false;
            }

            homingTimeElapsed += Time.deltaTime;
        }
        else
        {
            // Continue moving in the last direction after homingDuration
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
}
