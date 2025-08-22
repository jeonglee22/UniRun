using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver { get; set; } = false;

    private GameObject gameover;
    private TextMeshProUGUI scoreText;

    private int score = 0;

	private void Start()
	{
		gameover = GameObject.FindWithTag("GameOver");
		gameover.SetActive(false);

		scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	private void Update()
    {
		if (isGameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
    }

    public void AddScore(int s)
    {
        if(!isGameOver)
        {
            score += s;
            scoreText.text = $"Score : {score}";
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameover.SetActive(true);
    }
}
