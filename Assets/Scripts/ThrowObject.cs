using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThrowObject : MonoBehaviour
{
    public Transform startPoint = null, endPoint = null;
    public int Angle = 0, Trajection_num = 100;
    private float Angle_Rad = 0;
    [SerializeField]
    float V = 0, Config = 0.1f;

    private void Update()
    {
        Debug.Log(Mathf.Cos(Angle));
        Debug.Log(Mathf.Sin(Angle));
    }

    //public void Fire()
    //{
    //    CalV();
    //    GameObject B = Instantiate(Bullet, StartPoint.transform.position, Quaternion.identity);
    //    Vector3 Force = Vector3.zero;
    //    Force.x = V * 50 * Mathf.Cos(Angle_Rad);
    //    Force.y = V * 50 * Mathf.Sin(Angle_Rad);
    //    B.GetComponent<Rigidbody2D>().AddForce(Force);
    //}

    private void CalV()
    {
        float X = endPoint.transform.position.x - startPoint.position.x;
        float Y = endPoint.transform.position.y - startPoint.position.y;
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
        V = Mathf.Sqrt(v2);
    }

    private void OnDrawGizmosSelected()
    {

        CalV();

        Gizmos.color = Color.red;

        for (int i = 0; i < Trajection_num; i++)
        {
            float time = i * Config;
            float X = V * Mathf.Cos(Angle_Rad) * time;
            float Y = V * Mathf.Sin(Angle_Rad) * time - 0.5f * (10 * time * time);

            Vector3 pos1 = startPoint.position + new Vector3(X, Y, 0);

            time = (i + 1) * Config;
            X = V * Mathf.Cos(Angle_Rad) * time;
            Y = V * Mathf.Sin(Angle_Rad) * time - 0.5f * (10 * time * time);

            Vector3 pos2 = startPoint.position + new Vector3(X, Y, 0);

            Gizmos.DrawLine(pos1, pos2);
        }
    }
}
