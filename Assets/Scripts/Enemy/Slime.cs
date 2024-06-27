using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{
    private EnemyPathFinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathFinding>();
    }

    public void Attack()
    {
        if (PlayerController.Instance != null)
        {
            Vector2 direction = (PlayerController.Instance.transform.position - transform.position).normalized;

            //while (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > 5f)
            //{
            //    enemyPathfinding.MoveTo(direction);
            //}
        }
    }
}
