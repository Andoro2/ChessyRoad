using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop_AddaptZigZag : MonoBehaviour
{
    public float m_Speed = 5f;
    public int Side;

    public Vector3 m_NextPos;

    public bool InPlace = true;

    public GameObject m_Player;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindWithTag("Player");
        m_NextPos = transform.position;

        Side = UnityEngine.Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<InPlaceChecker>().SetBool(InPlace);

        if (transform.position.z == 0) Destroy(gameObject);

        if (transform.position == m_NextPos) InPlace = true;
        else
        {
            InPlace = false;
            transform.position = Vector3.MoveTowards(transform.position, m_NextPos, m_Speed * Time.deltaTime);
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

        if (Math.Abs(transform.position.x - m_Player.transform.position.x) ==
            Math.Abs(transform.position.z - m_Player.transform.position.z))
        { // Si el jugador esá en la diagonal del alfil
            bool Diagonable = true;

            float incrementoX = (m_Player.transform.position.x - transform.position.x > 0) ? 2 : -2;
            float incrementoZ = (m_Player.transform.position.z - transform.position.z > 0) ? 2 : -2;

            Debug.Log("Diagonalizable mongolín");

            for (float x = transform.position.x + incrementoX, z = transform.position.z + incrementoZ; x != m_Player.transform.position.x && z != m_Player.transform.position.z; x += incrementoX, z += incrementoZ)
            {
                if (!MasterMovement.isObjectHere(new Vector3(x, 0, z)))
                {
                    Diagonable = false;
                }
            }

            if (Diagonable && !MasterMovement.EnemyPositions.Contains(m_Player.transform.position))
            {
                NextPos = m_Player.transform.position;
                MasterMovement.EnemyPositions.Add(NextPos);

                return NextPos;
            }
        }

        // Revisa las posibles posiciones y añade las que estén libres
        List<Vector3> Movements = new List<Vector3>
        {
            new Vector3(transform.position.x + 2, transform.position.y, transform.position.z - 2),
            new Vector3(transform.position.x - 2, transform.position.y, transform.position.z - 2)
        };
        foreach (Vector3 Move in Movements)
        {
            if (MasterMovement.isObjectHere(Move) && !MasterMovement.EnemyPositions.Contains(Move))
            {
                AvailablePositions.Add(Move);
            }
        }

        if (AvailablePositions.Count == 1)
        {
            NextPos = AvailablePositions[0];
        }
        else if (AvailablePositions.Count == 2)
        {
            if (Side == 0)
            {
                NextPos = AvailablePositions[Side];
                Side = 1;
            }
            else if (Side == 1)
            {
                NextPos = AvailablePositions[Side];
                Side = 0;
            }
        }
        else
        {
            NextPos = transform.position;
        }

        MasterMovement.EnemyPositions.Add(NextPos);

        return NextPos;
    }
}
