using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : MonoBehaviour, IHealth
{
    public float maxHealth = 1000f;
    [SerializeField] // để debug
    private float currentHealth;

    public float healValue = 3f; // chỉ số hồi máu của tòa thành
    public float timeToHeal = 1f; // thời gian để hồi máu ví dụ 1s hồi 3 máu 
    private float timeToHealTimer;

    public GameObject[] destroyEffect;

    // thuộc tính cho health bar 
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth < maxHealth * 0.33f)
        {
            foreach (GameObject effect in destroyEffect)
            {
                effect.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject effect in destroyEffect)
            {
                effect.SetActive(false);
            }
        }

        if (timeToHealTimer >= timeToHeal)
        {
            Heal(healValue);
        }
        timeToHealTimer += Time.deltaTime;
    }

    public void Heal(float value)
    {
        currentHealth += value;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        timeToHealTimer = 0;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        if (currentHealth <= 0)
        {
            CastleDestroy();
        }
    }

    protected void CastleDestroy()
    {
        healthBar.gameObject.SetActive(false);
        GameManager.instance.GameOver();
        SoundControl.instance.PlayCastleDie();
    }
}
