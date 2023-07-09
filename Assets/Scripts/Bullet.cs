using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public Enemy shooter;
    public float damage;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<DestructableTiles>(out DestructableTiles tilemap))
        {
            tilemap.Hit(collision);
        }

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.Hit(damage);
        }

        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerComponent))
        {
            if (playerComponent.tag == "Player")
            {
                if (shooter != null)
                {
                    shooter.TakeDamage(damage);
                }
            }
        }

        if (collision.gameObject.TryGetComponent<Tnt>(out Tnt tnt))
        {
            tnt.Hit();
        }

        //FindObjectOfType<AudioManager>().PlayOneShot("explosion");

        Destroy(gameObject);
    }
}
