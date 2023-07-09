using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Timeline;

public class Hitbox : MonoBehaviour
{
    public float damage = 3f;

    public int count = 0;

    public Rigidbody2D rb;

    public Transform basicTransform;

    public GameManager manager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<DestructableTiles>(out DestructableTiles tilemap))
        {
            tilemap.Hit(collision);
        }

        if (collision.gameObject.TryGetComponent<NormalWalls>(out NormalWalls normalTilemap))
        {
            normalTilemap.Hit(collision);
        }

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.Hit(damage);
        }

        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerComponent))
        {
            if (playerComponent.tag == "Player")
            {
                if (manager.enemies.Count != 0)
                {
                    manager.enemies[0].TakeDamage(damage);
                }
            }
        }

        if (collision.gameObject.TryGetComponent<Tnt>(out Tnt tnt))
        {
            tnt.Hit();
        }

        if (count == 0)
        {
            count = 0;
        }
    }

    private void Update()
    {
        if (count > 0)
        {
            transform.position = basicTransform.position;
            transform.rotation = basicTransform.rotation;
            count--;
        }
        if (count == 0)
        {
            gameObject.SetActive(false);
        }
    }

}
