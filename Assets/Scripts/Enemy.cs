using System.Collections;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyKilled;

    [SerializeField] float health, maxHealth = 5f;

    [SerializeField] FloatingHealthBar healthBar;

    public PlayerHealthBar playerHealhBar;

    public Animator animator;

    public Transform playerTransform;

    public Weapon weapon;

    private static readonly System.Random random = new System.Random();

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        StartCoroutine(Shoot());        
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f * GenerateRandomNumber(0, 1));
            weapon.Fire();
            yield return new WaitForSeconds(0.05f);
            weapon.Fire();
            yield return new WaitForSeconds(0.05f);
            weapon.Fire();
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
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }
}
