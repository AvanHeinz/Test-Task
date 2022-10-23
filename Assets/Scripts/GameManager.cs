using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text playerScoreText;
    public Text enemyScoreText;
    public static int playerScore { get; private set; } = 1;
    public static int enemyScore { get; private set; } = 1;
    bool gameHasEnded = false;
    public float restartDelay = 1f;

    public void PlayerWin()
    {
        //Отвечает за добавление единички к переменной и перезапускает уровень
        if (gameHasEnded == false)
        {
            FindObjectOfType<GameManager>().PlayerScore();
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }

    }

    public void EnemyWin()
    {
        //Отвечает за добавление единички к переменной и перезапускает уровень
        if (gameHasEnded == false)
        {
            FindObjectOfType<GameManager>().EnemyScore();
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }

    }
    
    public void EnemyScore()
    {
        //Добавляет единицу к переменной с очками ИИ
        enemyScoreText.text = enemyScore++.ToString();
    }

    public void PlayerScore()
    {
        //Добавляет единицу к переменной с очками игрока
        playerScoreText.text = playerScore++.ToString();
    }

    void Restart()
    {
        //Перезагружает сцену, то бишь уровень
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
