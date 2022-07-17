using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private float gameOverTimer = 3.0f;

    private void Update()
    {
        if (gameOverTimer <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            gameOverTimer -= Time.deltaTime;
        }
    }
}
