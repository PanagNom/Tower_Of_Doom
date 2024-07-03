using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> spawners = new List<GameObject>();
    public GameObject enemy;
    public GameObject enemyBig;

    private float max;
    private float min;

    private float spawnTime;
    private float countTime;

    private int score;
    private int playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        max = 100;
        min = -100;

        spawnTime = 10f;
        countTime = 0f;

        playerHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;

        if (countTime > spawnTime)
        {
            countTime = 0f;

            foreach (GameObject spawner in spawners)
            {
                Transform transform = spawner.transform;

                float jitterX = (Random.Range(max, min) + transform.position.x);
                float jitterZ = (Random.Range(max, min) + transform.position.z);

                Vector3 pos = new Vector3((float)jitterX, 6, (float)jitterZ);

                Instantiate(enemyBig, pos, Quaternion.identity);
            }
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
    }

    public void DamagePlayer(int damage)
    {
        playerHealth -= damage;

        if (playerHealth < 0)
        {
            Debug.Log("Game Over");
        }
    }
}
