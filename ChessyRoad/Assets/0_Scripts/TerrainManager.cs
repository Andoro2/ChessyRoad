using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    static public GameObject m_Player,
        WSquare, BSquare, Square,
        Enemies, Terrain;

    public int m_TableWidth, actualLength, m_TableMargin;

    private bool m_StartColor, m_ColorFlag;
    private Vector3 SquarePosition;
    private List<GameObject> m_Foes = new List<GameObject>();
    public float m_EnemyChance, m_EmptySquareChance;//, zPos;
    void Start()
    {
        m_Player = GameObject.Find("Player");

        transform.position = m_Player.transform.position;

        WSquare = Resources.Load<GameObject>("PreFabs/WhiteSquare");
        BSquare = Resources.Load<GameObject>("PreFabs/BlackSquare");

        // Load pieces to spawn
        m_Foes.Add(Resources.Load<GameObject>("PreFabs/Rook"));
        m_Foes.Add(Resources.Load<GameObject>("PreFabs/Knight"));
        m_Foes.Add(Resources.Load<GameObject>("PreFabs/Pawn"));
        m_Foes.Add(Resources.Load<GameObject>("PreFabs/BishopFZZ"));
        m_Foes.Add(Resources.Load<GameObject>("PreFabs/BishopZZ"));
        m_Foes.Add(Resources.Load<GameObject>("PreFabs/BishopRand"));

        Enemies = new GameObject();
        Enemies.name = "Enemigos";

        Terrain = new GameObject();
        Terrain.name = "Terreno";
    }
    void Update()
    {
        if (Vector3.Distance(m_Player.transform.position, transform.position) < (m_TableMargin * 2))
        {
            //zPos = m_Player.transform.position.z;
            CreateTerrain();
            transform.position += new Vector3(0f, 0f, 2f);
        }
    }
    void CreateTerrain()
    {
        actualLength = 0;
        SquarePosition = transform.position;

        List<Vector3> SquarePositions = new List<Vector3>();

        if (m_TableWidth % 2 == 0) //even/par
        {
            SquarePosition = new Vector3(((m_TableWidth/2)*(-1))*2, 0, SquarePosition.z);
        }
        else
        {
            SquarePosition = new Vector3((((m_TableWidth+1) / 2) * (-1))*2, 0, SquarePosition.z);
        }
        
        if(m_StartColor)
        {
            m_ColorFlag = false;
            m_StartColor = false;
        }
        else
        {
            m_ColorFlag = true;
            m_StartColor = true;
        }

        while (actualLength != m_TableWidth)
        {
            if (m_ColorFlag)
            {
                Square = WSquare;
                m_ColorFlag = false;
            }
            else
            {
                Square = BSquare;
                m_ColorFlag = true;
            }

            SquarePosition = new Vector3(SquarePosition.x + 2, 0, SquarePosition.z);

            float m_EmptySquareOption = 100;

            if (SquarePosition.z > 8)
            {
                m_EmptySquareOption = Random.Range(0, 100);
                if(m_EmptySquareOption > m_EmptySquareChance) SquarePositions.Add(SquarePosition);
            }

            if (m_EmptySquareOption > m_EmptySquareChance)
            {
                GameObject SquarePiece = Instantiate(Square, SquarePosition, Quaternion.identity);
                SquarePiece.transform.SetParent(Terrain.transform);
            }
            actualLength++;
        }
        SpawnFoes(SquarePositions);
    }

    void SpawnFoes(List<Vector3> SpawnPos)
    {
        int EnemyCount = 0;

        GameObject PrevEnemy = null;
        foreach (Vector3 pos in SpawnPos)
        {
            float m_EnemySpawn = Random.Range(0, 100);

            if (m_EnemySpawn < m_EnemyChance && EnemyCount < 2)
            {
                GameObject SpawnedEnemy = m_Foes[Random.Range(0, m_Foes.Count - 1)];

                if (EnemyCount == 1 && PrevEnemy.CompareTag("Rook"))
                {
                    SpawnedEnemy = m_Foes[Random.Range(1, m_Foes.Count - 1)];
                }

                GameObject Piece = Instantiate(SpawnedEnemy, pos, Quaternion.identity);
                Piece.transform.SetParent(Enemies.transform);
                MasterMovement.EnemyPositions.Add(pos);
                EnemyCount++;
                PrevEnemy = Piece;
            }
        }
    }
}
