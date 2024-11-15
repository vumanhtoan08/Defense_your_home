using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float lifeTime = 5;
    private float lifeTimer = 0;
    public float damage = 10f;

    private void OnEnable()
    {
        lifeTimer = 0;
    }

    private void Update()
    {
        // Destroy(gameObject, 5f);
        if (lifeTimer > lifeTime)
        {
            gameObject.SetActive(false);
        }
        lifeTimer += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            enemy.TakeDamage(damage);

            gameObject.SetActive(false);
        }
    }
}
