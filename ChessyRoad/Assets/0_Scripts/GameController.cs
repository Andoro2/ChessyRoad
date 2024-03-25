using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    private GameObject m_Player;
    public GameObject m_DeathMenu, m_ScoreMenu;

    public TMP_Text m_ScoreText,
        m_FinalScoreText,
        m_TimeText;

    public float m_MaxTime = 50f;
    [SerializeField] static public float m_Timer;

    static public int m_Score = 0;

    public enum Turns { Player, EnemyMove, WaitForEnemy }
    [SerializeField] static public Turns Turn = Turns.Player;
    [SerializeField] public Turns Turno = Turns.Player;

    public enum PlayerColors { Black, White}
    [SerializeField] static public PlayerColors PlayerColor = PlayerColors.Black;

    public enum GameModes { Easy, Timer }
    [SerializeField] public GameModes GameMode = GameModes.Easy;
    void Start()
    {
        Time.timeScale = 1f;
        m_Player = GameObject.FindWithTag("Player").gameObject;

        m_Timer = m_MaxTime;
    }
    void Update()
    {
        Turno = Turn;
        m_ScoreText.text = m_Score.ToString();

        if(GameMode == GameModes.Timer && Turn == Turns.Player
            && MasterMovement.EnemiesReachedPlace())
        {
            if(m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;
                m_TimeText.text = m_Timer.ToString("F1") + " s";
            }
            else
            {
                Death();
            }
        }
    }
    public void Death()
    {
        m_ScoreMenu.SetActive(false);
        m_FinalScoreText.text = m_Score.ToString();
        PlayerMovement.NextPosition = Vector3.zero;
        m_Player.GetComponent<PlayerMovement>().enabled = false;

        Rigidbody rb = m_Player.GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.None;

        m_DeathMenu.SetActive(true);

        m_Score = 0;

        GameObject GC = GameObject.FindWithTag("GameController");
        GC.GetComponent<MasterMovement>().enabled = false;
        GC.GetComponent<TerrainManager>().enabled = false;
        GC.GetComponent<GameController>().enabled = false;
    }
    static public void ScorePoints(int points)
    {
        m_Score += points;
    }
    public void SetPlayerColor(bool Option)
    {
        if (Option) PlayerColor = PlayerColors.Black;
        else PlayerColor = PlayerColors.White;
    }
    public void ResetTime()
    {
        m_Timer = m_MaxTime;
    }
    static public void GetTime(float BonusTime)
    {
        m_Timer += BonusTime;
    }
}
