using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //This is the code for only handling Player based movement
    Rigidbody rb;
    Renderer[] rends;
    Animator anim;
    Vector3 startScale;
    float flashTime = 0f;
    float flashLength = 0.25f;
    float lastHit = -1f;
    float turnSpeed = 360f;
    public float jumpForce = 6.5f;
    public float speed = 10;
    public bool isGrounded = false;
    public GameObject footDust;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rends = GetComponentsInChildren<Renderer>();
        anim = GetComponentInChildren<Animator>();
        startScale = transform.localScale;
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

            transform.localScale = new Vector3(startScale.x * 0.8f, startScale.y * 1.3f, startScale.z * 0.8f);
            GameManager.instance.Play("jump", 1f, 0.8f);
            CameraShake.instance.Kick(3f);
        }
        squashBack();
        flashing();
        anim.SetBool("isMoving", Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f);
        anim.SetBool("isGrounded", isGrounded);

    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        rb.MovePosition(rb.position + new Vector3(move, 0, 0));
        turn();
    }

    void turn()
    {
        float input = Input.GetAxis("Horizontal");
        Quaternion look = Quaternion.Euler(0f, input * 35f, -input * 8f);
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, look, turnSpeed * Time.deltaTime));
    }

    void squashBack()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startScale, 10f * Time.deltaTime);
    }

    void flashing()
    {
        if (flashTime > 0f)
        {
            flashTime -= Time.deltaTime;
            foreach (Renderer r in rends)
            {
                r.material.SetColor("_EmissionColor", Color.white * Mathf.Max(flashTime, 0f) / flashLength);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obsticle"))
        {
            if (Time.time - lastHit < 0.3f) return;
            lastHit = Time.time;

            CameraShake.instance.Shake(0.25f, 0.35f);

            Fx.Burst(collision.GetContact(0).point, new Color(1f, 0.8f, 0.3f), 1.5f);
            GameManager.instance.HitStop(0.07f);
            GameManager.instance.FlashScreen();
            GameManager.instance.Play("hit", 1f, 1f);
            flashTime = flashLength;
            transform.localScale = new Vector3(startScale.x * 1.3f, startScale.y * 0.7f, startScale.z * 1.3f);
            ObsticleMove wall = collision.collider.GetComponentInParent<ObsticleMove>();
            if (wall != null)
            {
                wall.Hit();
            }

        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (footDust != false)
            {
                Instantiate(footDust, transform.position + Vector3.down, footDust.transform.rotation);
            }
            if (isGrounded == false)
            {
                transform.localScale = new Vector3(startScale.x * 1.25f, startScale.y * 0.75f, startScale.z * 1.25f);
                GameManager.instance.Play("land", Random.Range(0.9f, 1.1f), 0.8f);
                CameraShake.instance.Shake(0.08f, 0.06f);
            }
            isGrounded = true;
        }

    }
}
