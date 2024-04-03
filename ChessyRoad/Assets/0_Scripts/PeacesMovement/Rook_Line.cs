using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook_Line : MonoBehaviour
{
    //public float m_Speed = 5;
    private int Side;

    public Vector3 m_NextPos;

    public bool InPlace = true;

    public GameObject m_Player;

    void Start()
    {
        m_Player = GameObject.FindWithTag("Player");
        m_NextPos = transform.position;

        Side = Random.Range(0, 2);
    }

    void Update()
    {
        GetComponent<InPlaceChecker>().SetBool(InPlace);

        if (Vector3.Distance(transform.position, m_Player.transform.position) > 15) Destroy(gameObject);

        if (transform.position == m_NextPos) InPlace = true;
        else
        {
            InPlace = false;
            transform.position = Vector3.MoveTowards(transform.position, m_NextPos, GameController.m_RivalSpeed * Time.deltaTime);
        }

        if (transform.position.y < -5f) Destroy(gameObject);
    }
    public void SetNewPosition()
    {
        m_NextPos = GetPosition();
    }
    public Vector3 GetPosition()
    {
        InPlace = false;
        Vector3 NextPos = transform.position;

        List<Vector3> AvailablePositions = new List<Vector3>();

        if (transform.position.z == m_Player.transform.position.z)
        { // Si el jugador esá en la horizontal de la torre
            bool Horizontable = true;

            float incrementoX = (m_Player.transform.position.x - transform.position.x > 0) ? 2 : -2;

            for (float x = transform.position.x + incrementoX; x != m_Player.transform.position.x; x += incrementoX)
            {
                if (!MasterMovement.isObjectHere(new Vector3(x, 0, transform.position.z)))
                {
                    Horizontable = false;
                }
            }

            if (Horizontable && !MasterMovement.EnemyPositions.Contains(m_Player.transform.position))
            {
                if (GameController.GameMode != GameController.GameModes.Easy)
                {
                    NextPos = m_Player.transform.position;
                    MasterMovement.EnemyPositions.Add(NextPos);

                    return NextPos;
                }
                else if (GameController.GameMode == GameController.GameModes.Easy
                    && Mathf.Abs(transform.position.z - m_Player.transform.position.z) <= 2)
                {
                    NextPos = m_Player.transform.position;
                    MasterMovement.EnemyPositions.Add(NextPos);

                    return NextPos;
                }
            }
        }

        // Revisa las posibles posiciones y añade las que estén libres
        List<Vector3> Movements = new List<Vector3>
        {
            new Vector3(transform.position.x + 2, transform.position.y, transform.position.z),
            new Vector3(transform.position.x - 2, transform.position.y, transform.position.z)
        };
        foreach (Vector3 Move in Movements)
        {
            if (MasterMovement.isObjectHere(Move) && !MasterMovement.EnemyPositions.Contains(Move))
            {
                AvailablePositions.Add(Move);
            }
        }

        if (Side == 0)
        {
            if (AvailablePositions.Contains(Movements[Side]))
            {
                NextPos = Movements[Side];
            }
            else if (!AvailablePositions.Contains(Movements[Side]) && AvailablePositions.Contains(Movements[1]))
            {
                NextPos = Movements[1];
                Side = 1;
            }
            else
            {
                NextPos = transform.position;
            }
        }
        else if (Side == 1)
        {
            if (AvailablePositions.Contains(Movements[Side]))
            {
                NextPos = Movements[Side];
            }
            else if (!AvailablePositions.Contains(Movements[Side]) && AvailablePositions.Contains(Movements[0]))
            {
                NextPos = Movements[0];
                Side = 0;
            }
            else
            {
                NextPos = transform.position;
            }
        }

        NextPos = new Vector3(Mathf.Round(NextPos.x), Mathf.Round(NextPos.y), Mathf.Round(NextPos.z));
        MasterMovement.EnemyPositions.Add(NextPos);

        return NextPos;
    }
}
