using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public Enemy shooter;
    public bool isPlayer = false;

    public void Fire(float damage, float fireForce, int soundType)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        if (!isPlayer)
        {
            bullet.GetComponent<Bullet>().shooter = shooter;
        }
        bullet.GetComponent<Bullet>().damage = damage;

        if (soundType == 0)
        {
            FindObjectOfType<AudioManager>().PlayOneShot("gunshot");
        }
        else if (soundType == 1)
        {
            FindObjectOfType<AudioManager>().PlayOneShot("sniper");
        }
        else if (soundType == 2)
        {
            FindObjectOfType<AudioManager>().PlayOneShot("pistol");
        }
    }
}