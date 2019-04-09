using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject resetButton;
    [SerializeField]
    private GameObject quitButton;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private TextMeshProUGUI lifeText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Color A;
    [SerializeField]
    private Color B;

    private float speed = 2;
    private float playerHealth;
    private int playerScore;
    private bool playerDead;
    private bool paused;

    public float PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    public int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }

    public bool PlayerDead
    {
        get { return playerDead; }
        set { playerDead = value; }
    }

    private void Update()
    {
        lifeText.text = playerHealth + "%";
        scoreText.text = playerScore.ToString("000000");
        resetButton.SetActive(playerDead);
        quitButton.SetActive(playerDead);
        PauseMenu();
    }

    public IEnumerator ScoreColorLerp()
    {
        float currentLerpTime = 0;
        scoreText.color = A;

        while (scoreText.color != B)
        {
            currentLerpTime += Time.deltaTime;
            float perc = currentLerpTime * speed;
            scoreText.color = Color.Lerp(A, B, perc);
            yield return null;
        }
    }

    public IEnumerator LifeColorLerp()
    {
        float currentLerpTime = 0;
        lifeText.color = A;

        while (lifeText.color != B)
        {
            currentLerpTime += Time.deltaTime;
            float perc = currentLerpTime * speed;
            lifeText.color = Color.Lerp(A, B, perc);
            yield return null;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    void PauseMenu()
    {
        if (Input.GetButtonDown("Cancel") && !playerDead)
        {
            paused = !paused;
            pauseMenu.SetActive(paused);
        }
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
