using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Enemy shooter;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.Hit(1);
        }

        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerComponent))
        {
            if (playerComponent.tag == "Player")
            {
                if (shooter != null)
                {
                    shooter.TakeDamage(1);
                }
            }
        }

        //FindObjectOfType<AudioManager>().PlayOneShot("explosion");

        Destroy(gameObject);
    }
}
