
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask jumpableGround;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D coll;
    private bool grounded;
    private bool isJumping;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {   
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if(horizontalInput < 0.0f)
        
            transform.localScale = new Vector3(2,2,2);
        
        else if(horizontalInput >= 0.01f)
        
            transform.localScale = new Vector3(-2,2,2);    
        
            grounded = true;
        
        if(Input.GetKey(KeyCode.Space)&& isGrounded())
        
            Jump();
           

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);

    }


    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        grounded = false;
    }

    private void OnCollisonEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
            
    }
    private void OnCollisonExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = false;
            
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.2f, jumpableGround);
    }

}
