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
        m_FinalScoreText;

    static public int m_Score = 0;

    public enum Turns { Player, Enemy }
    [SerializeField] static public Turns Turn = Turns.Player;
    [SerializeField] public Turns Turno = Turns.Player;

    void Start()
    {
        Time.timeScale = 1f;
        m_Player = GameObject.FindWithTag("Player").gameObject;
    }
    void Update()
    {
        Turno = Turn;

        m_ScoreText.text = m_Score.ToString();

        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(1);
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
}
