using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Normal.Realtime;

public class BossBehaviorStateManager : MonoBehaviour
{
    public event Action OnAttack; 
    public string currentState;
    public Animator animator;
    public NavMeshAgent nav;
    public GameObject aimingObj;
    public GameObject player;
    public GameObject superAttackObj;
    public LayerMask whatIsGround, whatIsPlayer;
    private float sightRange, attackRange;
    public float fieldOfViewAngle;
    public bool playerInAttackRange;
    private float timeBetweenAttacks;
    private bool alreadyAttacked;
    public bool isSuperAttack,canRotate;
    private int countNormalAttack;
    private float superAttackTime;
    float rotationTime = 5f; 
    private GameObject[] playersInSight;
    private EnemySyncData enemySyncData;
    private RealtimeTransform _realtimeTransform;
    
    void Awake()
    {
        _realtimeTransform = GetComponent<RealtimeTransform>(); 
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        enemySyncData = GetComponent<EnemySyncData>();
        timeBetweenAttacks = 1.5f;
        sightRange = 8f;
        attackRange = 8f;
        fieldOfViewAngle = 120f;
        currentState = "Idle";
        countNormalAttack = 0;
        isSuperAttack = false;
        canRotate = false;
        rotationTime = 5f;
        superAttackTime = rotationTime;
    }

    void Start()
    {
        if (_realtimeTransform.isUnownedInHierarchy)
        GetComponent<RealtimeTransform>().SetOwnership(0);
    }

    void Update()
    {
        if (canRotate)
        {
            Rotate();
        }
       CheckState();
    }

    private void CheckState()
    {
        switch(currentState)
        {
            case "Idle":
            Idle();
            break;

            case "Chase":
            ChasePlayer();
            break;

            case "Attack":
            AttackPlayer();
            break;

            case "Die":
            Death();
            break;

        } 
    }

    private void Idle()
    {
        animator.SetBool("isChase",false);
        animator.SetBool("isAim",false);

        CheckPlayerInSigthRange();

        if (player != null)
        {
            currentState = "Chase";
            enemySyncData.ChangeBehaviorState(currentState);
        }
    }

    private void CheckPlayerInSigthRange()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);

        foreach (Collider target in targets)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle <= fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit, sightRange))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green);
                }
                else
                {
                    Debug.DrawLine(transform.position, transform.position + direction * sightRange, Color.red);
                    if (target.gameObject.GetComponent<PlayerSyncData>()._playerHP > 0)
                    {
                        SetTarget(target.gameObject);
                    }
                }
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        if (player == null)
        {
            player = target;
        }
        else
        {
            float distanceBetweenPlayer1 = Vector3.Distance(player.transform.position, transform.position);
            float distanceBetweenPlayer2 = Vector3.Distance(target.transform.position, transform.position);
            if (distanceBetweenPlayer1 > distanceBetweenPlayer2)
            {
                player = target;
            }
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("isChase",true);

        CheckPlayerInSigthRange();

        nav.SetDestination(player.transform.position);

        float distanceBetweenTarget = Vector3.Distance(player.transform.position, transform.position);
        
        playerInAttackRange = distanceBetweenTarget <= attackRange;

        if (playerInAttackRange) 
        {
            if (player.GetComponent<PlayerSyncData>()._playerHP > 0)
            {
                currentState = "Attack";
                enemySyncData.ChangeBehaviorState(currentState);
            }
            else 
            {
                player = null;
                currentState = "Idle";
                enemySyncData.ChangeBehaviorState(currentState);
            }
        }
    }

    private void AttackPlayer()
    {
        animator.SetBool("isChase",false);
        animator.SetBool("isAim",true);
        nav.SetDestination(transform.position);

        if (!isSuperAttack)
        {
        transform.LookAt(player.transform);
        }

        if (!alreadyAttacked)
        {
            if (player.GetComponent<PlayerSyncData>()._playerHP > 0 && countNormalAttack < 4)
            {
                Debug.Log("Mill Cute Attack");
                countNormalAttack += 1;
                animator.SetTrigger("Attack");
                Shoot();
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            else if (player.GetComponent<PlayerSyncData>()._playerHP > 0 && countNormalAttack >= 4)
            {
                alreadyAttacked = true;
                isSuperAttack = true;
                StartCoroutine(DeleySuperAtk());
            }
        }

        float distanceBetweenTarget = Vector3.Distance(player.transform.position, transform.position);
        playerInAttackRange = distanceBetweenTarget <= attackRange;

        if (!playerInAttackRange)
        {
            if (player.GetComponent<PlayerSyncData>()._playerHP > 0)
            {
                animator.SetBool("isAim",false);
                currentState = "Chase";
                enemySyncData.ChangeBehaviorState(currentState);
            }
            else 
            {
                player = null;
                currentState = "Idle";
                enemySyncData.ChangeBehaviorState(currentState);
            }
        }
        else if (playerInAttackRange)
        {
            if (player.GetComponent<PlayerSyncData>()._playerHP <= 0)
            {
                player = null;
                currentState = "Idle";
                enemySyncData.ChangeBehaviorState(currentState);
            }
        }
    }

    void Shoot()
    {
        //audioSource.PlayOneShot(audioSource.clip);
        RaycastHit hitInfo;
        if(Physics.Raycast(aimingObj.transform.position, aimingObj.transform.forward, out hitInfo, attackRange))
        {
            Debug.DrawRay(transform.position, transform.forward * attackRange, Color.green, 3);

            if (hitInfo.transform.CompareTag("Player"))
            {
                hitInfo.transform.gameObject.GetComponent<PlayerSyncData>().DecreasePlayerHP(0);
            }
        }
        //OnAttack?.Invoke();
    }

    IEnumerator DeleySuperAtk()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Mill SuperCute Attack");
        countNormalAttack = 0;
        animator.SetTrigger("SuperAttack");
        SuperShoot();
        canRotate = true;
    } 

    void SuperShoot()
    {
        superAttackObj.SetActive(true);
    }

    private void ResetAttack()
    {
        Debug.Log("Reset Attack");
        animator.SetBool("isChase",false);
        alreadyAttacked = false;
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        StartCoroutine(RemoveBody());
    }

    public void Die()
    {
        currentState = enemySyncData._enemyBehaviorState;
    }
    
    private IEnumerator RemoveBody()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    void Rotate()
    {   
        float rotationSpeed = 360f / rotationTime; 
        transform.Rotate(Vector3.up, 360f); 
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); 

        superAttackTime -= Time.deltaTime;

        if (superAttackTime <= 0)
        {
            isSuperAttack = false;
            canRotate = false;
            superAttackTime = rotationTime;
            superAttackObj.SetActive(false);
            Invoke(nameof(ResetAttack), 3);
        }
    }
}