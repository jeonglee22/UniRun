using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float jumpForce = 300f;

	private int jumpCountMax = 2;
	private int jumpCount = 0;

	public AudioClip dieAudioClip;

    private Animator animator;
    private Rigidbody2D rb;
	private AudioSource audioSource;

	private GameManager gameManager;

    private bool isGrounded = true;
	private bool isDead = false;

	private void Awake()
	{
		animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start()
    {
		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}

    // Update is called once per frame
    private void Update()
    {
		if(isDead)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0) && jumpCount < jumpCountMax)
		{
			rb.linearVelocity = Vector2.zero;
			rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			jumpCount++;
			audioSource.Play();
		}

		if(Input.GetMouseButtonUp(0) && rb.linearVelocity.y > 0)
		{
			rb.linearVelocity *= 0.5f;
		}

		animator.SetBool("Grounded", isGrounded);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Platform") &&
			collision.contacts[0].normal.y > 0.7f)
		{ 
			isGrounded = true;
			jumpCount = 0;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Platform"))
			isGrounded = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("DeadZone") && !isDead)
		{
			Die();
			gameManager.GameOver();
		}
	}

	private void Die()
	{
		animator.SetTrigger("Die");
		rb.bodyType = RigidbodyType2D.Kinematic;
		rb.linearVelocity = Vector2.zero;
		isDead = true;

		audioSource.PlayOneShot(dieAudioClip);
	}
}
