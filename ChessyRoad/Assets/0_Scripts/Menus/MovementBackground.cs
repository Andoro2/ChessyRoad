using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

public class MovementBackground : MonoBehaviour
{
    public GameObject m_FastImage, m_SlowImage,
        m_EasyModeImage, m_NormalModeImage, m_TimerModeImage, m_BulletModeImage;
    private void Update()
    {
        var easyColor = m_EasyModeImage.GetComponent<Image>().color;
        var normalColor = m_NormalModeImage.GetComponent<Image>().color;
        var timerColor = m_TimerModeImage.GetComponent<Image>().color;
        var bulletColor = m_BulletModeImage.GetComponent<Image>().color;

        switch (GameMode)
        {
            case GameModes.Easy:
                easyColor.a = 1f;
                normalColor.a = 0f;
                timerColor.a = 0f;
                bulletColor.a = 0f;
                break;
            case GameModes.Normal:
                easyColor.a = 0f;
                normalColor.a = 1f;
                timerColor.a = 0f;
                bulletColor.a = 0f;
                break;
            case GameModes.Timer:
                easyColor.a = 0f;
                normalColor.a = 0f;
                timerColor.a = 1f;
                bulletColor.a = 0f;
                break;
            case GameModes.Bullet:
                easyColor.a = 0f;
                normalColor.a = 0f;
                timerColor.a = 0f;
                bulletColor.a = 1f;
                break;
        }

        m_EasyModeImage.GetComponent<Image>().color = easyColor;
        m_NormalModeImage.GetComponent<Image>().color = normalColor;
        m_TimerModeImage.GetComponent<Image>().color = timerColor;
        m_BulletModeImage.GetComponent<Image>().color = bulletColor;
    }
    private void OnEnable()
    {
        var fastColor = m_FastImage.GetComponent<Image>().color;
        var slowColor = m_FastImage.GetComponent<Image>().color;
        if (MovementStyle == MovementStyles.Slow)
        {
            fastColor.a = 0f;
            fastColor.a = 1f;
            m_FastImage.GetComponent<Image>().color = fastColor;
            m_SlowImage.GetComponent<Image>().color = slowColor;
        }
        else if (MovementStyle == MovementStyles.Fast)
        {
            fastColor.a = 1f;
            fastColor.a = 0f;
            m_FastImage.GetComponent<Image>().color = fastColor;
            m_SlowImage.GetComponent<Image>().color = slowColor;
        }

        var easyColor = m_EasyModeImage.GetComponent<Image>().color;
        var normalColor = m_NormalModeImage.GetComponent<Image>().color;
        var timerColor = m_TimerModeImage.GetComponent<Image>().color;
        var bulletColor = m_BulletModeImage.GetComponent<Image>().color;

        GameObject.FindWithTag("GameController").GetComponent<GameController>().SetGameModeNormal();

        switch (GameMode)
        {
            case GameModes.Easy:
                easyColor.a = 1f;
                normalColor.a = 0f;
                timerColor.a = 0f;
                bulletColor.a = 0f;
                break;
            case GameModes.Normal:
                easyColor.a = 0f;
                normalColor.a = 1f;
                timerColor.a = 0f;
                bulletColor.a = 0f;
                break;
            case GameModes.Timer:
                easyColor.a = 0f;
                normalColor.a = 0f;
                timerColor.a = 1f;
                bulletColor.a = 0f;
                break;
            case GameModes.Bullet:
                easyColor.a = 0f;
                normalColor.a = 0f;
                timerColor.a = 0f;
                bulletColor.a = 1f;
                break;
        }

        m_EasyModeImage.GetComponent<Image>().color = easyColor;
        m_NormalModeImage.GetComponent<Image>().color = normalColor;
        m_TimerModeImage.GetComponent<Image>().color = timerColor;
        m_BulletModeImage.GetComponent<Image>().color = bulletColor;
    }
}
