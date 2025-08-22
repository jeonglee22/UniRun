using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f;
	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}

	// Update is called once per frame
	private void Update()
    {
		if (gameManager.isGameOver)
			return;

		transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
