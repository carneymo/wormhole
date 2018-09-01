using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] asteroidPrefabs;
    public int numHazardSpawn;
    public int waveDiff = 6;
    public float spawnStart;
    public float spawnWait;
    public Text scoreText;
    public Text gameOverText;
    public Text waveText;
    public float spawnDistanceThreshold = 15.0f;

    private int wave = 0;
    private int score = 0;

    private ScoreController scoreController;

	void Start () {
        StartSpawnWave();
	}

    private void FixedUpdate()
    {
        if(Input.GetButtonUp("Back"))
        {
            restartGame();
        }
    }

    void restartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void StartSpawnWave()
    {
        StartCoroutine(DisplayWave());
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(spawnStart);
        
        for (int i=0; i < numHazardSpawn; i++)
        {
            SpawnAsteroid();
        }
        numHazardSpawn += waveDiff;

        yield return new WaitForSeconds(spawnWait);
    }

    public GameObject GetRandomPrefab()
    {
        return asteroidPrefabs[
            Random.Range(0, asteroidPrefabs.Length)
        ];
    }

    public GameObject SpawnAsteroid()
    {
        Vector3 spawnPosition = GetGoodSpawnLocation();
        Quaternion spawnRotation = Quaternion.identity;
        return Instantiate(GetRandomPrefab(), spawnPosition, spawnRotation) as GameObject;
    }

    private Vector3 GetGoodSpawnLocation()
    {
        bool nearPlayer = false;

        Vector3 playerPosition;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Ensure player model exists and get its position
        if (player) {
             playerPosition = player.transform.position;

            // Create spawn position not ontop of the player
            Vector3 spawnPosition;
            int iterator = 0;
            do
            {
                spawnPosition = new Vector3(
                    Random.Range(Boundary.xMin, Boundary.xMax),
                    -0.2f,
                    Random.Range(Boundary.zMin, Boundary.zMax)
                );
                float dist = Mathf.Abs(spawnPosition.magnitude - playerPosition.magnitude);
                nearPlayer = (dist < spawnDistanceThreshold);
                if(nearPlayer)
                {
                    Debug.LogFormat("Distance from player too close = {0}", dist);
                }
                
            } while (nearPlayer && iterator < 10);

            return spawnPosition;
        }
        else
        {
            return new Vector3(
                Random.Range(Boundary.xMin, Boundary.xMax),
                -0.2f,
                Random.Range(Boundary.zMin, Boundary.zMax)
            );
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void UpdateWave()
    {
        wave++;
        waveText.text = "Wave " + wave;
    }

    public IEnumerator DisplayWave()
    {
        UpdateWave();

        FadeTextIn(waveText, 2.0f);

        yield return new WaitForSeconds(3);

        FadeTextOut(waveText, 2.0f);

        yield return new WaitForSeconds(1);
    }

    public IEnumerator DisplayGameOver()
    {
        FadeTextIn(gameOverText, 6.0f);
        
        Invoke("restartGame", 6.0f);

        yield return null;
    }

    void FadeTextIn(Text t, float fadeInSpeed = 1.0f)
    {
        FadeText(t, fadeInSpeed, 1.0f);
    }

    void FadeTextOut(Text t, float fadeOutSpeed = 1.0f)
    {
        FadeText(t, fadeOutSpeed, 0.0f);
    }

    void FadeText(Text t, float fadeSpeed, float alpha)
    {
        t.color = new Color(
            t.color.r,
            t.color.g,
            t.color.b,
            Mathf.Abs(1.0f - alpha)
        );
        Color fixedColor = t.color;
        fixedColor.a = 1;
        t.color = fixedColor;
        t.CrossFadeAlpha(Mathf.Abs(1.0f-alpha), 0f, true);

        t.CrossFadeAlpha(alpha, fadeSpeed, false);
    }
}
