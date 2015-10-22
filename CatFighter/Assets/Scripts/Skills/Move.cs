using UnityEngine;

public class Move : MonoBehaviour{

    [HideInInspector]
    public Rigidbody2D playerRigidbody;
    [HideInInspector]
    public Animator anim;
    public bool facingRight = true;
    public float moveForce = 0.8f;
    public float maxSpeed = 5f;
    public float jumpForce = 280f;
    public AudioClip jumpClip;
    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    public bool grounded = false;			// Whether or not the player is grounded.

   void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"));
    }

    // 用來將角色轉向的method
    public void Flip()
    {
        // 反轉facingRight
        facingRight = !facingRight;

        // 將角色的x值乘上-1令角色反轉
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //左右移動
    public void Forward(float h)
    {
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Speed", Mathf.Abs(h));

        // 假如輸入是讓角色向右走，而角色朝左，則...
        if (h > 0 && !facingRight)
            // ... 將角色轉向
            Flip();
        // 假如輸入是讓角色向左走，而角色朝右，則...
        else if (h < 0 && facingRight)
            // ... 將角色轉向
            Flip();
        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * playerRigidbody.velocity.x < maxSpeed)
            // ... add a force to the player.
            playerRigidbody.AddForce(Vector2.right * h * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(playerRigidbody.velocity.x) > maxSpeed && maxSpeed > 0)
            // ... set the player's velocity to the maxSpeed in the x axis.
            playerRigidbody.velocity = new Vector2(Mathf.Sign(playerRigidbody.velocity.x) * maxSpeed, playerRigidbody.velocity.y);
        if (maxSpeed < 0)
            playerRigidbody.AddForce(-1 * playerRigidbody.velocity);
        bool walking = (h != 0f);
        anim.SetBool("IsWalking", walking);
    }

    public void Jump()
    {
        // Set the Jump animator trigger parameter.
        anim.SetTrigger("Jump");

        // Play a random jump audio clip.
        AudioSource.PlayClipAtPoint(jumpClip, transform.position);

        // Add a vertical force to the player.
        playerRigidbody.AddForce(new Vector2(0f, jumpForce));
    }
}
