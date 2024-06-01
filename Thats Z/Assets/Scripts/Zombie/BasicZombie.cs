using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicZombie : MonoBehaviour
{
    public Animator ar;
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float chaseDistance = 20f;
    public float attackDistance = 2f;
    public int damage = 10;
    public float attackCooldown = 2f;
    public float attackMoveSpeed = 0.01f;
    private Transform positionZ;
    
    public Transform target;
    private NavMeshAgent agent;
    private float timer;
    private bool isChasing = false;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        FindRandomWanderPoint();
        ar.SetBool("IsWalking", true);
    }

    void Update()
    {
        
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= attackDistance && !isAttacking)
            {
                Attack();
            }
            else if (distance <= chaseDistance)
            {
                isChasing = true;
                isAttacking = false;
                agent.isStopped = false;
                agent.SetDestination(target.position);
                ar.SetBool("IsAttacking", false);
            }
            else if (isChasing)
            {
                isChasing = false;
                isAttacking = false;
                agent.isStopped = false;
                agent.speed = agent.speed;
                FindRandomWanderPoint();
                ar.SetBool("IsAttacking", false);
            }
        }
        else
        {
            isChasing = false;
            isAttacking = false;
            agent.speed = agent.speed;
            FindRandomWanderPoint();
        }

        if (isChasing)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                FindRandomWanderPoint();
                timer = 0;
            }
        }
        
        
    }

    void FindRandomWanderPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            ar.SetBool("IsAttacking", true);
            // Wykonaj obrażenia na graczu
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            lastAttackTime = Time.time;
            isAttacking = true;
            agent.isStopped = true;
            agent.speed = attackMoveSpeed;
            StartCoroutine(ResumeChasingAfterDelay(attackCooldown));
        }
    }

    IEnumerator ResumeChasingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        agent.isStopped = false;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
