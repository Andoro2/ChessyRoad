using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Pause Menu
    public static bool GameIsPaused = false;
    public GameObject MainMenu, PauseMenu, InGameUI, InGameTimeText, InGameTimer;
    private GameObject m_Player, GC;
    MasterMovement MM;
    PlayerMovement PM;
    TerrainManager TM;
    void Start()
    {
        m_Player = GameObject.FindWithTag("Player");
        PM = m_Player.GetComponent<PlayerMovement>();

        GC = GameObject.FindWithTag("GameController");
        MM = GC.GetComponent<MasterMovement>();
        TM = GC.GetComponent<TerrainManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !MainMenu.activeSelf)
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
            || GameController.GameMode == GameController.GameModes.Stress)
        {
            InGameTimeText.SetActive(true);
            InGameTimer.SetActive(true);
        }
        else
        {
            InGameTimeText.SetActive(false);
            InGameTimer.SetActive(false);
        }
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
        //if (!m_Player.transform.GetChild(1).gameObject.activeSelf) m_Player.transform.GetChild(1).gameObject.SetActive(true);

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
            || GameController.GameMode == GameController.GameModes.Stress)
        {
            InGameTimeText.SetActive(true);
            InGameTimer.SetActive(true);
        }
        else
        {
            InGameTimeText.SetActive(false);
            InGameTimer.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}