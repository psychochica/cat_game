using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float stopDuration;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private Vector3 initScale;
    private bool movingLeft;
    private bool isStopping;

    
   
    private void Awake()
    {
        initScale = enemy.localScale;
        
    }

    private void Update()
    {
        if (!isStopping)
        {
            if (movingLeft)
            {
                if (enemy.position.x > leftEdge.position.x)
                {
                    MoveInDirection(-1);
                }
                else
                {
                    StartCoroutine(StopAndChangeDirection());
                }
            }
            else
            {
                if (enemy.position.x < rightEdge.position.x)
                {
                    MoveInDirection(1);
                }
                else
                {
                    StartCoroutine(StopAndChangeDirection());
                }
            }
        }
        
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

       
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }

    private IEnumerator StopAndChangeDirection()
    {
        isStopping = true;

        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
       
        yield return new WaitForSeconds(stopDuration);

      
        movingLeft = !movingLeft;
        isStopping = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(leftEdge.position, rightEdge.position);
    }
}