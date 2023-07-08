using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] float health, maxHealth = 5f;


    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    private void Start()
    {
        health = maxHealth;
        this.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        this.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Debug.Log("a³a");
            // restart levelu
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    void Update()
    {

    }
}
