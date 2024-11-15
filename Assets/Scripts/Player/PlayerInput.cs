using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;

    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private Animator anim;
    public Camera cam;
    public GameObject clickEffect; // gameobj click effect 

    // Thuộc tính attack của nhân vật 
    [Header("Attack Properties")]
    public float attackSpeed;
    public float rangeDetect = 5;
    public Transform firePoint;
    public LayerMask enemyLayer;
    // biến cho detect enemy
    private List<GameObject> currentEnemies = new List<GameObject>();
    
    public NavMeshAgent Agent { get => agent; }
    public Animator Anim { get => anim; }

    // mục đích debug 
    private string currentState;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Hủy đối tượng này nếu đã có một instance tồn tại
        }
        else
        {
            instance = this;
        }

        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        stateMachine.Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            stateMachine.ChangeState(new MovingState());
        }
        currentState = stateMachine.activeState.ToString();
    }
    public Ray InputClickPoint()
    {
        return cam.ScreenPointToRay(Input.mousePosition);
    }

    public List<GameObject> EnemiesAroundPlayer()
    {
        // clear lại giá trị trước khi xử lý 
        currentEnemies.Clear();

        Collider[] results = new Collider[10]; // tạo ra mảng để giới hạn số lượng detect được
        // sử dụng cách này để giới hạn số enemy detect 
        // int numberEnemies = Physics.OverlapSphereNonAlloc(transform.position, rangeDetect, results, enemyLayer);
        int numberEnemies = Physics.OverlapCapsuleNonAlloc(transform.position, transform.position - new Vector3(0, 8, 0), rangeDetect, results, enemyLayer);

        for (int i = 0; i < numberEnemies; i++)
        {
            currentEnemies.Add(results[i].transform.gameObject);
        }
        return currentEnemies;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangeDetect);
    }
}

/*
 sơ đồ của State sẽ là click thì chuyển sang movingstate => IdleState => AttackState
 */