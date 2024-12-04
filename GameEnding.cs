using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public CanvasGroup outOfTimeBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    public AudioSource outOfTimeAudio;

    bool m_HasAudioPlayed;
    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    public float time;
    bool timeFlag = false;
    float m_Timer;

    public TextMeshProUGUI timeText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }
    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
    void Update()
    {
        if (timeFlag == false)
        {
            timeText.text = "Time: " + time;
            timeFlag = true;
        }
        if(time <= 0)
        {
            timeText.alpha = 0;
            EndLevel(outOfTimeBackgroundImageCanvasGroup, true, outOfTimeAudio);
        }
        if (m_IsPlayerAtExit)
        {
            timeText.alpha = 0;
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if(m_IsPlayerCaught)
        {
            timeText.alpha = 0;
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
        time -= Time.deltaTime;
        timeText.text = "Time: " + time.ToString("F0");
    }
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        m_Timer += Time.deltaTime;

        imageCanvasGroup.alpha = m_Timer / fadeDuration;
        if(m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
        
    }
}
