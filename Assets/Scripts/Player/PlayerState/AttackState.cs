using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackState : BaseState
{
    private PlayerInput playerInput;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject closestEnemy;

    // thuộc tính cho tấn công 
    private float attackSpeed;
    private float attackTimer;
    private Transform firePoint;
    private Vector3 shootDirection;
    // thuộc tính để viên đạn bay xiên
    private float V = 0;
    private int Angle = 45;
    private float Angle_Rad = 45 * Mathf.Deg2Rad;
    private float Config = 0.1f; // dùng để đặt lại giữa đơn vị thực tế và đơn vị của unity 

    public override void Enter()
    {
        playerInput = PlayerInput.instance;
        attackSpeed = playerInput.attackSpeed;
        firePoint = playerInput.firePoint;
        // kích hoạt animation
        playerInput.Anim.SetBool("isAttack", true);
    }

    public override void Execute()
    {
        // kiểm tra xem có enemy trong tầm detect hay không 
        if (playerInput.EnemiesAroundPlayer().Count() == 0)
        {
            stateMachine.ChangeState(new IdleState());
        }
        else
        {
            enemies = playerInput.EnemiesAroundPlayer();
            //  Debug.Log(enemies.Count); đã debug chính xác

            // thực hiện hàm này để tìm ra được khoảng cách nhỏ nhất từ player đến enemy 
            EnemyClosest(enemies);
            RotatePlayerToEnemy(playerInput.transform, EnemyClosest(enemies).transform);

            attackTimer += Time.deltaTime;
            if (attackTimer > attackSpeed)
            {
                Attack(EnemyClosest(enemies));
            }
        }
    }

    public override void Exit()
    {
        // tắt animation 
        playerInput.Anim.SetBool("isAttack", false);
    }

    public GameObject EnemyClosest(List<GameObject> enemies)
    {
        // Gán closestEnemy là phần tử đầu tiên của mảng
        closestEnemy = enemies[0];

        // khai báo biến closest Distance
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            // kiểm tra khoảng cách từ player đến enemy
            float distanceToPlayer = Vector3.Distance(playerInput.transform.position, enemy.transform.position);

            // kiểm tra nấu khoảng cách từ player đến enemy mà nhỏ hơn khoảng cách gần nhất 
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public void Attack(GameObject target)
    {
        // ------------------------------BẮN THEO NÉM XIÊN------------------------// cần cải tiến 
        //CalV(firePoint.position, target.transform.position);
        //Vector3 shootDirection = (target.transform.position - firePoint.position).normalized;

        //Vector3 Force = Vector3.zero;
        //Force.x = 50 * V * Mathf.Cos(Angle_Rad) * shootDirection.x;
        //Force.y = 50 * V * Mathf.Sin(Angle_Rad);
        //Force.z = 50 * V * Mathf.Cos(Angle_Rad) * shootDirection.z;

        //GameObject arrow = ArrowPool.instance.GetPooledObject();
        //if (arrow != null)
        //{
        //    // khởi tạo trạng thái ban đầu
        //    arrow.transform.position = firePoint.transform.position;
        //    arrow.transform.rotation = Quaternion.LookRotation(shootDirection);
        //    arrow.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    Vector3 clampedRotation = arrow.transform.eulerAngles;

        //    clampedRotation.x = Mathf.Clamp(clampedRotation.x, 90f, 90f);
        //    arrow.transform.eulerAngles = clampedRotation;

        //    arrow.SetActive(true);
        //    arrow.GetComponent<Rigidbody>().AddForce(Force);
        //}

        //----------------------------------Bắn thằng theo dir từ player tới enemy---------------------------------//
        SoundControl.instance.PlayShootArrow();
        
        GameObject arrow = ArrowPool.instance.GetPooledObject();
        // lấy hướng bắn điểm bắn tới enemy
        shootDirection = (target.transform.position - firePoint.position).normalized;

        if (arrow != null)
        {
            arrow.transform.position = firePoint.transform.position;
            arrow.transform.rotation = Quaternion.LookRotation(shootDirection);
            arrow.GetComponent<Rigidbody>().velocity = Vector3.zero;
            arrow.transform.Rotate(new Vector3(-90, 0, 0));

            arrow.SetActive(true);
            arrow.GetComponent<Rigidbody>().velocity = shootDirection * 50f;
        }
        //-----------------------------------------------------------------------------------------------------------//

        attackTimer = 0;
        //Debug.Log("Attack" + target.name);
    }

    void CalV(Vector3 firePoint, Vector3 target)
    {
        /*
        start point: playerInput.transform.position -> có rồi
        end point: target.transform.position -> có rồi 
        int angle: 45 -> contants
        float Angle_Rad: 45 * Mathf.Deg2Rad;
        float V -> phải tính -----
        float Config = 0.1f; -> contants
        */

        float X = Vector3.Distance(new Vector3(firePoint.x, 0, firePoint.z), new Vector3(target.x, 0, target.z));
        float Y = target.y - firePoint.y;

        if (X < 0)
        {
            Angle_Rad = -Math.Abs(Angle) * Mathf.Deg2Rad;
            Config = -Math.Abs(Config);
        }
        else
        {
            Angle_Rad = Math.Abs(Angle) * Mathf.Deg2Rad;
            Config = Math.Abs(Config);
        }

        float v2 = 10 / (-(Y - Mathf.Tan(Angle_Rad) * X) / (X * X)) / (2 * Mathf.Cos(Angle_Rad) * Mathf.Cos(Angle_Rad));
        v2 = Mathf.Abs(v2);
        V = Mathf.Sqrt(v2) + .8f;
    }

    void RotatePlayerToEnemy(Transform player, Transform enemy)
    {
        // Tính hướng từ người chơi tới enemy
        Vector3 directionToEnemy = enemy.position - player.position;

        // Đặt hướng y của vector thành 0 để chỉ quay trên trục y
        directionToEnemy.y = 0;

        // Tính toán góc quay dựa trên hướng về enemy
        Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);

        // Áp dụng góc quay cho người chơi
        player.rotation = Quaternion.Slerp(player.rotation, targetRotation, Time.deltaTime * 5f);
    }
}
