using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
	public float speed = 10f;
	private float width;

	private GameManager gameManager;

	private void Start()
	{
		var sprite = gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
		width = sprite.rect.width / sprite.pixelsPerUnit;

		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		//var collider = gameObject.GetComponent<BoxCollider2D>();
		//width = collider.size.x;
	}

	// Update is called once per frame
	private void Update()
    {
		if (gameManager.isGameOver)
			return;

		transform.Translate(Vector3.left * speed * Time.deltaTime);

		if (transform.position.x <= -width)
			transform.position = Vector3.zero;
	}
}
