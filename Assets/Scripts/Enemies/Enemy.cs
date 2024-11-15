using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected StateMachineEnemy stateMachineEnemy;
    protected NavMeshAgent agent;
    public Path path;
    public NavMeshAgent Agent { get => agent; }
    public StateMachineEnemy StateMachineEnemy { get => stateMachineEnemy; }

    // thuộc tính phục vụ cho attackState
    public float damage = 50f;
    public float detectRange = 5f;
    public LayerMask castleLayer;
    public float attackRange;
    protected Collider[] hits;

    public Collider[] Hits { get => hits; }

    // thuộc tính cho animation 
    protected Animator anim;

    public Animator Anim { get => anim; }

    [SerializeField]
    private string currentState;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        stateMachineEnemy = GetComponent<StateMachineEnemy>();
        agent = GetComponent<NavMeshAgent>();
        stateMachineEnemy.Initialise();
        anim = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        path = GameObject.FindWithTag("Path").GetComponent<Path>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (stateMachineEnemy != null)
        {
            if (stateMachineEnemy.activeState == null)
            {
                currentState = stateMachineEnemy.activeState.ToString();
            }
        }
    }

    public bool DetectCastle() // phát hiện 
    {
        hits = Physics.OverlapSphere(transform.position, detectRange, castleLayer);
        if (hits.Count() > 0)
        {
            return true;
        }
        return false;
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public virtual void Attack()
    {

    }
}
