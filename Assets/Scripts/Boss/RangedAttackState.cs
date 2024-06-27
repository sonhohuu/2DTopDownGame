using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : State
{
    public GameObject bulletPrefab; // Assign your bullet prefab in the Inspector
    private float waitTime = 1f;
    [SerializeField] private Transform armSpawnPoint;
    private SpriteRenderer bossSpriteRenderer;
    private Vector3 originalArmSpawnOffset; // Store the original offset of armSpawnPoint
    public float armOffsetX = 1f;
    public override void Enter()
    {
        base.Enter();
        animator.Play("HomingMissile");

        // Get the SpriteRenderer of the boss
        bossSpriteRenderer = GetComponent<SpriteRenderer>();

        // Store the original local position of the armSpawnPoint
        originalArmSpawnOffset = armSpawnPoint.localPosition;

        // Adjust armSpawnPoint based on the flipX state of the boss
        UpdateArmSpawnPointPosition();
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Shoot();
        bossController.enemyPathfinding.StopMoving();
        canTransition = true;
    }

    private void Shoot()
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, armSpawnPoint.position, Quaternion.identity);
        bullet.transform.SetParent(transform.parent);

        // Set the bullet's speed and direction
        bullet.GetComponent<HomingBullet>().Initialize(PlayerController.Instance.transform);
    }

    public override void Transition()
    {
        if (canTransition)
        {
            canTransition = false;
            StartCoroutine(WaitBeforeTransitionToDash());
        }
    }
    private void UpdateArmSpawnPointPosition()
    {
        if (bossSpriteRenderer.flipX)
        {
            armSpawnPoint.localPosition = new Vector3(-Mathf.Abs(originalArmSpawnOffset.x), originalArmSpawnOffset.y, originalArmSpawnOffset.z);
        }
        else
        {
            armSpawnPoint.localPosition = new Vector3(Mathf.Abs(originalArmSpawnOffset.x), originalArmSpawnOffset.y, originalArmSpawnOffset.z);
        }
    }

    private IEnumerator WaitBeforeTransitionToDash()
    {
        yield return new WaitForSeconds(waitTime);
        bossController.ChangeState(bossController.dashState);
    }
}
