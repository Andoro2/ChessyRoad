using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Pause Menu
    public static bool GameIsPaused = false;
    public GameObject MainMenu, PauseMenu, DeathMenu, InGameUI, InGameTimeText, InGameTimer;
    public TextMeshProUGUI m_ExplanationText;
    [TextArea(3, 10)]
    public string m_EasyExplanation, m_NormalExplanation, m_TimerExplanation, m_BulletExplanation;
    private GameObject m_Player, GC;
    MasterMovement MM;
    PlayerMovement PM;
    //TerrainManager TM;

    public GameObject UpLeftButton, UpButton, UpRightButton,
        LeftButton, RightButton,
        DownLeftButton, DownButton, DownRightButton;
    void Start()
    {
        m_Player = GameObject.FindWithTag("Player");
        PM = m_Player.GetComponent<PlayerMovement>();

        GC = GameObject.FindWithTag("GameController");
        MM = GC.GetComponent<MasterMovement>();
        //TM = GC.GetComponent<TerrainManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !MainMenu.activeSelf && !DeathMenu.activeSelf)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        switch(GameController.GameMode)
        {
            case GameController.GameModes.Easy:
            {
                m_ExplanationText.text = m_EasyExplanation;
                break;
            }
            case GameController.GameModes.Normal:
            {
                m_ExplanationText.text = m_NormalExplanation;
                break;
            }
            case GameController.GameModes.Timer:
            {
                m_ExplanationText.text = m_TimerExplanation;
                break;
            }
            case GameController.GameModes.Bullet:
            {
                m_ExplanationText.text = m_BulletExplanation;
                break;
            }
        }
        if (DeathMenu.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
            DeathMenu.SetActive(false);
            InGameUI.SetActive(true);
        }
    }
    public void Main()
    {
        MainMenu.SetActive(true);
        InGameUI.SetActive(false);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        PM.enabled = true;
        GameIsPaused = false;
    }
    public void NewGame()
    {
        Time.timeScale = 1;
        if (TerrainManager.Enemies != null)
        {
            if (TerrainManager.Enemies.transform.childCount != 0)
            {
                for (int i = 0; i < TerrainManager.Enemies.transform.childCount; i++)
                {
                    GameObject enemigo = TerrainManager.Enemies.transform.GetChild(i).gameObject;

                    Destroy(enemigo);
                }
            }
        }
        if (TerrainManager.Terrain != null)
        {
            if (TerrainManager.Terrain.transform.childCount != 0)
            {
                for (int i = 0; i < TerrainManager.Terrain.transform.childCount; i++)
                {
                    GameObject cuadro = TerrainManager.Terrain.transform.GetChild(i).gameObject;

                    Destroy(cuadro);
                }
            }
        }

        GC.transform.position = Vector3.zero;
        GC.GetComponent<GameController>().enabled = true;
        GameController.m_Score = 0;
        GC.GetComponent<GameController>().ResetTime();
        GC.GetComponent<GameController>().NewEnemyTurn();
        GC.GetComponent<MasterMovement>().enabled = true;
        GC.GetComponent<TerrainManager>().enabled = true;

        PlayerMovement.NextPosition = Vector3.zero;
        PlayerMovement.TargetPosition = Vector3.zero;
        m_Player.transform.position = Vector3.zero;
        m_Player.transform.rotation = Quaternion.identity;

        Rigidbody rb = m_Player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        PM.zPos = 0;
        PM.enabled = true;

        if (GameController.GameMode == GameController.GameModes.Timer
            || GameController.GameMode == GameController.GameModes.Bullet)
        {
            InGameTimeText.SetActive(true);
            InGameTimer.SetActive(true);
        }
        else
        {
            InGameTimeText.SetActive(false);
            InGameTimer.SetActive(false);
        }

        UpLeftButton.SetActive(true);
        UpButton.SetActive(true);
        UpRightButton.SetActive(true);
        LeftButton.SetActive(true);
        RightButton.SetActive(true);
        DownLeftButton.SetActive(true);
        DownButton.SetActive(true);
        DownRightButton.SetActive(true);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        PM.enabled = false;
        GameIsPaused = true;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        if (TerrainManager.Enemies.transform.childCount != 0)
        {
            for (int i = 0; i < TerrainManager.Enemies.transform.childCount; i++)
            {
                GameObject enemigo = TerrainManager.Enemies.transform.GetChild(i).gameObject;

                Destroy(enemigo);
            }
        }
        if (TerrainManager.Terrain.transform.childCount != 0)
        {
            for (int i = 0; i < TerrainManager.Terrain.transform.childCount; i++)
            {
                GameObject cuadro = TerrainManager.Terrain.transform.GetChild(i).gameObject;

                Destroy(cuadro);
            }
        }

        if (!m_Player.transform.GetChild(0).gameObject.activeSelf) m_Player.transform.GetChild(0).gameObject.SetActive(true);

        GC.transform.position = Vector3.zero;
        GC.GetComponent<GameController>().enabled = true;
        GameController.m_Score = 0;
        GC.GetComponent<GameController>().ResetTime();
        GC.GetComponent<GameController>().NewEnemyTurn();
        GC.GetComponent<MasterMovement>().enabled = true;
        GC.GetComponent<TerrainManager>().enabled = true;

        PlayerMovement.NextPosition = Vector3.zero;
        PlayerMovement.TargetPosition = Vector3.zero;
        m_Player.transform.position = Vector3.zero;
        m_Player.transform.rotation = Quaternion.identity;

        Rigidbody rb = m_Player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        PM.zPos = 0;
        PM.enabled = true;

        if (GameController.GameMode == GameController.GameModes.Timer
            || GameController.GameMode == GameController.GameModes.Bullet)
        {
            InGameTimeText.SetActive(true);
            InGameTimer.SetActive(true);
        }
        else
        {
            InGameTimeText.SetActive(false);
            InGameTimer.SetActive(false);
        }

        UpLeftButton.SetActive(true);
        UpButton.SetActive(true);
        UpRightButton.SetActive(true);
        LeftButton.SetActive(true);
        RightButton.SetActive(true);
        DownLeftButton.SetActive(true);
        DownButton.SetActive(true);
        DownRightButton.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}