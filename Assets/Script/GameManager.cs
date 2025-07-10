using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score, explosive, time, targetScore, level;
    private bool isEndGame;
    [SerializeField] private GameObject targetPanel;
    
    public static GameManager instance { get; private set; }
    public TextMeshProUGUI scoreText, explosiveText, timeText, targetText, targetScoreText, levelText;

    public GameObject[] levels;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        targetScore = 1200;
        level = 1;
        score = 0;
        ResetGame();
        UpdateUI();
    }
    private void ResetGame()
    {
        time = 60;
        isEndGame = false;
        Time.timeScale = 1;
        StartCoroutine(CountDownTime());
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == level - 1);
        }
        AudioManager.instance.PlayBackgroundSound();
    }
    private IEnumerator CountDownTime()
    {
        while (!isEndGame)
        {
            yield return new WaitForSeconds(1f);
            if(time > 0)
            {
                time -= 1;
                if (time <= 10)
                {
                    AudioManager.instance.PlayTimeSound();
                }
            }
            else
            {
                time = 0;
                EndGame();
                AudioManager.instance.StopBackgroundSound();
            }
        }
    }
    private void EndGame() 
    {
        if (isEndGame) return;
        isEndGame = true;
        Time.timeScale = 0f;
        targetPanel.SetActive(true);

        if (score >= targetScore)
        {
            if (level < levels.Length)
            {
                AudioManager.instance.PlayTargetSound();
                targetText.text = $"Bạn đã lên cấp!\n Mục tiêu tiếp theo là :  {targetScore + 1000} điểm ";
                StartCoroutine(NextLevel());
            }
            else
            {
                AudioManager.instance.PlayWinSound();
                targetText.text = "Bạn đã hoàn thành tất cả cấp độ!";
                StartCoroutine(LoadScene(1));
            }
        }
        else
        {
            AudioManager.instance.PlayDefeatSound();
            targetText.text = "Bạn đã không hoàn thành mục tiêu!";
            StartCoroutine(LoadScene(0));
        }
    }
    private IEnumerator NextLevel()
    {
        yield return new WaitForSecondsRealtime(3f);
        level++;
        targetScore += 1000;
        ResetGame();
        targetPanel.SetActive(false);
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene (sceneIndex);
    }
    void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        scoreText.text = score.ToString();
        explosiveText.text = explosive.ToString();
        timeText.text = time.ToString();
        levelText.text = level.ToString();
        targetScoreText.text = targetScore.ToString();
    }
}
