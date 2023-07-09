using System.Collections;
using System;
using UnityEngine;
using Unity.Profiling;

public class Enemy : MonoBehaviour
{
    public int type = 0;

    public static event Action<Enemy> OnEnemyKilled;

    float health, maxHealth = 5f;

    [SerializeField] FloatingHealthBar healthBar;

    public PlayerHealthBar playerHealhBar;

    public Animator animator;

    public Transform playerTransform;

    public Weapon weapon;

    private static readonly System.Random random = new System.Random();

    private int count = 15;
    private Quaternion randomRotation; 

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    private void Start()
    {
        if (type == 0)
        {
            maxHealth = 5f;
        }
        else if (type == 1)
        {
            maxHealth = 3f;
        }
        else if (type == 2)
        {
            maxHealth = 1f;
        }


        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        StartCoroutine(Shoot());        
    }

    IEnumerator Shoot()
    {
        if (type == 0)
        {
            while (true)
            {
                yield return new WaitForSeconds(3f * GenerateRandomNumber(0, 1));
                weapon.Fire(1, 5f);
                yield return new WaitForSeconds(0.05f);
                weapon.Fire(1, 5f);
                yield return new WaitForSeconds(0.05f);
                weapon.Fire(1, 5f);
            }
        }
        else if(type == 1)
        {
            while (true)
            {
                yield return new WaitForSeconds(3f * GenerateRandomNumber(0.7f, 1));
                weapon.Fire(3, 30f);
            }
        }
        if (type == 2)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.66f);
                weapon.Fire(0.5f, 1.5f);
            }
        }
    }

    public static float GenerateRandomNumber(float min, float max)
    {
        return (float)(min + random.NextDouble() * (max - min));
    }

    public void Hit(float damageAmount)
    {
        animator.Play("alert");
        FindObjectOfType<AudioManager>().PlayOneShot("alert");

        playerHealhBar.TakeDamage(damageAmount);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }

    void Update()
    {
        Vector2 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (type == 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
        }
        if (type == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
        if (type == 2)
        {
            if (count >= 40)
            {
                randomRotation = Quaternion.Euler(0, 0, (int) GenerateRandomNumber(0, 360));
                count = 0;
            }
            else
            {
                count++;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, randomRotation, Time.deltaTime * 6f);
        }

    }
}
