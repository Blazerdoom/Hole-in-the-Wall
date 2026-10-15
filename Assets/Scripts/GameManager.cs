using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text finalScoreText;
    public Text gameOverLabel;
    public Text bestText;
    public Image hitFlash;
    AudioSource sfx;
    Camera cam;
    Light sun;
    float sunLight;
    float sunFlash = 0f;
    float hitStopTime = 0f;
    float score = 0f;
    float nextTick = 10f;
    bool isGameOver = false;

    public Color skyEnd = new Color(0.32f, 0.12f, 0.2f);
    public float skySeconds = 60f;
    Color skyStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        sfx = gameObject.AddComponent<AudioSource>();
        cam = Camera.main;
        skyStart = cam.backgroundColor;
        sun = FindAnyObjectByType<Light>();
        sunLight = sun.intensity;
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.transform.localScale = Vector3.Lerp(scoreText.transform.localScale, Vector3.one, 12f * Time.unscaledDeltaTime);
        finalScoreText.transform.localScale = Vector3.Lerp(finalScoreText.transform.localScale, Vector3.one, 12f * Time.unscaledDeltaTime);
        gameOverLabel.transform.localScale = Vector3.Lerp(gameOverLabel.transform.localScale, Vector3.one, 12f * Time.unscaledDeltaTime);
        screenFlashing();
        hitStopping();

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

        if (score >= nextTick)
        {
            nextTick += 10f;
            scoreText.transform.localScale = Vector3.one * 1.4f;
            Play("tick", 1f, 0.6f);
        }
        UpdateSky();
    }

    public void gameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        gameOverPanel.SetActive(true);
        finalScoreText.text = score.ToString("F1") + "s";
        finalScoreText.transform.localScale = Vector3.one * 0.7f;
        gameOverLabel.transform.localScale = Vector3.one * 0.7f;

        float best = PlayerPrefs.GetFloat("best", 0f);
        if (score > best)
        {
            PlayerPrefs.SetFloat("best", score);
            bestText.text = "NEW BEST!";
        }
        else
        {
            bestText.text = "BEST " + best.ToString("F1") + "s";
        }

        Play("gameover", 1f, 1f);
        CameraShake.instance.Shake(0.4f, 0.3f);
        Time.timeScale = 0f;
    }

    public void Play(string clipName, float pitch, float volume)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + clipName);
        sfx.pitch = pitch;
        sfx.PlayOneShot(clip, volume);
    }

    public void HitStop(float time)
    {
        if (isGameOver) return;
        Time.timeScale = 0.05f;
        hitStopTime = time;
    }

    void hitStopping()
    {
        if (hitStopTime > 0f)
        {
            hitStopTime -= Time.unscaledDeltaTime;
            if (hitStopTime <= 0f && isGameOver == false)
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void FlashScreen()
    {
        hitFlash.color = new Color(1f, 0.1f, 0.1f, 0.35f);
        sunFlash = 1f;
    }

    void screenFlashing()
    {
        Color c = hitFlash.color;
        c.a = Mathf.MoveTowards(c.a, 0f, 1.2f * Time.unscaledDeltaTime);
        hitFlash.color = c;

        sunFlash = Mathf.MoveTowards(sunFlash, 0f, 3f * Time.unscaledDeltaTime);
        sun.intensity = sunLight + sunFlash * 1.5f;
    }

    void UpdateSky()
    {
        Color sky = Color.Lerp(skyStart, skyEnd, score / skySeconds);
        cam.backgroundColor = sky;
        RenderSettings.fogColor = sky;
    }
}
