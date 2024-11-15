using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    private Enemy enemy;
    private CapsuleCollider capsuleCollider;
    public float maxHealth;

    public float currentHealth; // để debug 

    // thuộc tính cho health bar 
    public HealthBar healthBar;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // giới hạn máu 
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Heal(float value) // hồi máu
    {
        currentHealth += value;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    public void TakeDamage(float damage) // nhận sát thương
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        GameManager.instance.SubEnemy();
        capsuleCollider.enabled = false;
        enemy.StateMachineEnemy.ChangeState(new DieStateEnemy());
        healthBar.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        // 
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        capsuleCollider.enabled = true;
        healthBar.gameObject.SetActive(true);
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
}
