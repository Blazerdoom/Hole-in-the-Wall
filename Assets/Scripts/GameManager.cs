using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text finalScoreText;
    float score = 0f;
    bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        if(isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        score += Time.deltaTime;
        scoreText.text = score.ToString("F1") + "s";
    }

    public void gameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        gameOverPanel.SetActive(true);
        finalScoreText.text = score.ToString("F1") + "s";
        Time.timeScale = 0f;
    }
    
    
}
