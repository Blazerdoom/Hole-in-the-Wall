using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //This is the code for only handling Player based movement 
    Rigidbody rb;
    public float jumpForce = 6.5f;
    public float speed = 10;
    public bool isGrounded = false;
    public GameObject footDust;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y <-5)
        {
            GameManager.instance.gameOver();
        }    
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            if (footDust != false)
            {
                Instantiate(footDust, transform.position + Vector3.down, footDust.transform.rotation);
            }
            
        }
        

    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        rb.MovePosition(rb.position + new Vector3(move, 0, 0));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obsticle"))
        {
            CameraShake.instance.Shake();
            
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (footDust != false)
            {
                Instantiate(footDust, transform.position + Vector3.down, footDust.transform.rotation);
            }
            isGrounded = true;
        }
        
    }
}
