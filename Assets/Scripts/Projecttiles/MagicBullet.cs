using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    private float lifeTime = 5;
    private float lifeTimer = 0;
    public float damage = 50f;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Castle"))
        {
            CastleHealth castleHealth = other.GetComponent<CastleHealth>();
            castleHealth.TakeDamage(damage);

            gameObject.SetActive(false);
        }
    }
}
