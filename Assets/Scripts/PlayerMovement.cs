using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float playerSpeed = 50f;

    public Rigidbody2D rb;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontal * playerSpeed * Time.fixedDeltaTime, vertical * playerSpeed * Time.fixedDeltaTime);
    }
}
