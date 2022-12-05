using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hellmade.Sound;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text ScoreText;
    [SerializeField] private AudioClip coinAudioClip;
    [SerializeField] private GameObject LevelFinish;
    private string scoreStr;
    private int score;
    public int Score
    {
        get { return score; }
    }
    private void OnEnable()
    {
        Coin.AddScore += AddScore;
    }

    public void Awake()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1f;
    }
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.5f);
    }

    public void AddScore() {
        score += 1;
        scoreStr = score.ToString();
        ScoreText.text = scoreStr;
        scoreStr = null;
        //play audio
        EazySoundManager.PlayMusic(coinAudioClip, 1, false, false, 0, 0);
    }
    private void OnDisable()
    {
        Coin.AddScore -= AddScore;
    }
    public void OnGameComplete() {
        Time.timeScale = 0.2f;
        LevelFinish.SetActive(true);
    }
    public void NextLevel() {
        // for now using level restart. 
        SceneManager.LoadScene("MainGame");
    }
}
