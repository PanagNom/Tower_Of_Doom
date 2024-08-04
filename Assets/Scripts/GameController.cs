using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int score;
    private int playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        playerHealth = 100;
    }

    public void UpdateScore(int points)
    {
        Debug.Log("Score");
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
