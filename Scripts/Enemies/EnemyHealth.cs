using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float curHealth;
    private Knockback knockback;
    private FlashOnHit flash;

    private void Awake()
    {
        flash = GetComponent<FlashOnHit>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, 5f);
        StartCoroutine(flash.FlashRoutine());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (curHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
