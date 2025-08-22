using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private GameObject[] platforms;
    private int platformCount = 0;

    public GameObject platform;

    private float spawnPosX = 21f;
    public float spawnPosRangeY = 3f;
    public float spawnTimeMax = 2f;
    public float spawnTimeMin = 1f;

    private float spawnTimeInterval;
    private float spawnTime = 0f;

    private float despawnPosX = -30f;

	private GameManager gameManager;

	private void Start()
	{
        spawnTimeInterval = Random.Range(spawnTimeMin, spawnTimeMax);
        spawnTime = spawnTimeInterval;
        platforms = new GameObject[10];

		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update()
    {
        if(gameManager.isGameOver)
        {
            return;
        }

        spawnTime += Time.deltaTime;
        if(spawnTime > spawnTimeInterval)
        {
            SpawnPlatform();

            spawnTimeInterval = Random.Range(spawnTimeMin, spawnTimeMax);
			spawnTime = 0;
		}

        DisablePlatform();
    }

    private void SpawnPlatform()
    {
		float spawnPosY = Random.Range(-spawnPosRangeY, spawnPosRangeY);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0);

        GameObject spawned = FindDisablePlatform();
        if (spawned == null)
        {
			spawned = Instantiate(platform, spawnPos, Quaternion.identity);
            platforms[platformCount++] = spawned;
        }
        else
        {
            spawned.transform.position = spawnPos;
			spawned.SetActive(true);
        }
    }

    private void DisablePlatform()
    {
		for (int i = 0; i < platforms.Length; i++)
		{
            if (platforms[i] != null && platforms[i].gameObject.transform.position.x <= despawnPosX)
            {
                platforms[i].gameObject.SetActive(false);
            }
        }
    }

    private GameObject FindDisablePlatform()
    {
		for (int i = 0; i < platforms.Length; i++)
		{
			if (platforms[i] != null && !platforms[i].gameObject.activeSelf)
			{
                return platforms[i];
			}
		}
        return null;
	}
}
