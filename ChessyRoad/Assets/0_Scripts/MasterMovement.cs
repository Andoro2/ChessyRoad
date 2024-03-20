using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MasterMovement : MonoBehaviour
{
    static public List<Vector3> EnemyPositions = new List<Vector3>();

    int Iteration = 0;

    public bool EnemiesInPlace = true;
    private GameObject m_Player;
    void Start()
    {
        m_Player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        GameObject[] m_Pawns = GameObject.FindGameObjectsWithTag("Pawn");
        GameObject[] m_Rooks = GameObject.FindGameObjectsWithTag("Rook");
        GameObject[] m_Knights = GameObject.FindGameObjectsWithTag("Knight");
        GameObject[] m_BishopsZigZag = GameObject.FindGameObjectsWithTag("BishopZigZag");
        GameObject[] m_BishopsRandom = GameObject.FindGameObjectsWithTag("BishopRandom");
        GameObject[] m_BishopsFullZigZag = GameObject.FindGameObjectsWithTag("BishopFullZigZag");

        GameObject[] m_Enemies = m_Pawns
            .Concat(m_Rooks)
            .Concat(m_Knights)
            .Concat(m_BishopsZigZag)
            .Concat(m_BishopsRandom)
            .Concat(m_BishopsFullZigZag).ToArray();

        EnemiesInPlace = InPlaceCheck(m_Enemies);

        if (GameController.Turn == GameController.Turns.Enemy)
        {
            if (Iteration == 0)
            {
                foreach (GameObject Enemy in m_Enemies)
                {
                    if (Enemy.GetComponent<InPlaceChecker>().PieceEnabled && Enemy.transform.position != GameObject.FindWithTag("Player").gameObject.transform.position)
                    {
                        switch (Enemy.tag)
                        {
                            case ("Rook"):
                                Enemy.GetComponent<Rook_Line>().SetNewPosition();
                                break;
                            case ("Knight"):
                                Enemy.GetComponent<Knight>().SetNewPosition();
                                break;
                            case ("BishopRandom"):
                                Enemy.GetComponent<Bishop_Random>().SetNewPosition();
                                break;
                            case ("BishopZigZag"):
                                Enemy.GetComponent<Bishop_AddaptZigZag>().SetNewPosition();
                                break;
                            case ("BishopFullZigZag"):
                                Enemy.GetComponent<Bishop_FullZigZag>().SetNewPosition();
                                break;
                            case ("Pawn"):
                                Enemy.GetComponent<Pawn>().SetNewPosition();
                                break;
                        }
                    }
                    else
                    {
                        Enemy.GetComponent<InPlaceChecker>().PieceEnable();
                    }
                }
                Iteration = 1;
            }

            if (Iteration == 1)
            {
                if (EnemiesInPlace)
                {
                    if(EnemyPositions.Contains(m_Player.transform.position))
                    {
                        GameObject.FindWithTag("GameController").GetComponent<GameController>().Death();
                    }
                    else
                    {
                        GameController.Turn = GameController.Turns.Player;
                        Iteration = 0;
                    }
                }
            }            
        }
        
        EnemyPositions.Clear();
    }
    static public bool InPlaceCheck(GameObject[] Enemigos)
    {
        foreach (GameObject Enemigo in Enemigos)
        {
            if (!Enemigo.GetComponent<InPlaceChecker>().InPlace) return false;
        }
        return true;

    }
    static public bool isObjectHere(Vector3 position)
    {
        Collider[] m_Intersecting = Physics.OverlapSphere(position, 0.5f);
        if (m_Intersecting.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}