using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Normal.Realtime;
public class EnemyBehaviorStateManager : MonoBehaviour
{
    //public EnemyBehaviorState currentState;
    private string currentState;
    public Animator animator;
    public NavMeshAgent nav;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    private float highestDistance;
    private float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private float timeBetweenAttacks;
    private bool alreadyAttacked;
    
    private GameObject[] playersInSight;

    private EnemySyncData enemySyncData;

    private RealtimeTransform _realtimeTransform;

    void Awake()
    {
        _realtimeTransform = GetComponent<RealtimeTransform>(); 
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        enemySyncData = GetComponent<EnemySyncData>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenAttacks = 1.5f;
        highestDistance = 10f;
        sightRange = 5f;
        attackRange = 1.5f;
        currentState = "Idle";
    }
    // Update is called once per frame

    void Start()
    {
        if (_realtimeTransform.isUnownedInHierarchy)
        GetComponent<RealtimeTransform>().SetOwnership(0);
    }

    void Update()
    {
       //RunStateMachine();
       //SwitchAnimation();  
       CheckState();
    }

    // private void RunStateMachine()
    // {
    //     EnemyBehaviorState nextState = currentState?.RunCurrentState();

    //     if (nextState != null)
    //     {
    //         SwitchToTheNextState(nextState);
    //     }
    // }

    // private void SwitchToTheNextState(EnemyBehaviorState nextState)
    // {
    //     currentState = nextState;
    // }

    // private void SwitchAnimation()
    // {
    //     if (currentState == GameObject.Find("ChaseState").GetComponent<EnemyIdleState>())
    //     {
    //         animator.SetBool("isChase",false);
    //     }
    //     else if (currentState == GameObject.Find("ChaseState").GetComponent<EnemyChaseState>())
    //     {
    //         animator.SetBool("isChase",true);
    //         ChasePlayer();
    //     }
    //      else if (currentState == GameObject.Find("AttackState").GetComponent<EnemyAttackState>())
    //     {
    //         animator.SetBool("canAttack",true);
    //     }
    // }

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
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); 
        Debug.Log("playerInSightRange = " + playerInSightRange);
        if (playerInSightRange)
        {
            playersInSight = GameObject.FindGameObjectsWithTag("Player"); 
            // foreach (GameObject _player in playersInSight)
            // {
            //     float distanceBetweenPlayer = Vector3.Distance(_player.transform.position, transform.position);
            //     Debug.Log("distanceBetweenPlayer = " + distanceBetweenPlayer);
            //     Debug.Log("highestDistance = " + highestDistance);
            //     if (distanceBetweenPlayer > highestDistance)
            //     {
            //         player = _player.transform;
            //         highestDistance = distanceBetweenPlayer;
            //     }
            // }
            currentState = "Chase";
            enemySyncData.ChangeBehaviorState(currentState);
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("isChase",true);

        foreach (GameObject _player in playersInSight)
        {
            float distanceBetweenPlayer = Vector3.Distance(_player.transform.position, this.transform.position);
            // Debug.Log("distanceBetweenPlayer = " + distanceBetweenPlayer);
            // Debug.Log("highestDistance = " + highestDistance);
            if (distanceBetweenPlayer < 10)
            {
                player = _player.transform;
                nav.SetDestination(player.position);
                //highestDistance = distanceBetweenPlayer;
            }
        }
        //nav.SetDestination(player.position);
        //transform.LookAt(player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); 

        if (playerInAttackRange) 
        {
            currentState = "Attack";
            enemySyncData.ChangeBehaviorState(currentState);
        }
    }

    private void AttackPlayer()
    {
        animator.SetBool("isChase",false);
        nav.SetDestination(transform.position);
        transform.LookAt(player);
        //animator.SetBool("canAttack",true);
        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attack");
            alreadyAttacked = true;
            //animator.SetBool("canAttack",false);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); 

        if (!playerInAttackRange)
        {
            currentState = "Chase";
            enemySyncData.ChangeBehaviorState(currentState);
        }
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
}
