using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    //public float m_Speed = 5;

    public Vector3 m_NextPos;

    public bool InPlace = true;

    public GameObject m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindWithTag("Player");
        m_NextPos = transform.position;
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

        // Revisa las posibles posiciones y añade las que estén libres
        List<Vector3> Movements = new List<Vector3>
        {
            new Vector3(transform.position.x, transform.position.y, transform.position.z - 2),
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

        if(m_Player.transform.position == Movements[0] && AvailablePositions.Contains(Movements[0]))
        {
            NextPos = transform.position;
        }
        else if (AvailablePositions.Contains(m_Player.transform.position))
        {
            NextPos = m_Player.transform.position;
        }
        else if (AvailablePositions.Contains(Movements[0]))
        {
            NextPos = Movements[0];
        }
        else
        {
            NextPos = transform.position;
        }

        NextPos = new Vector3(Mathf.Round(NextPos.x), Mathf.Round(NextPos.y), Mathf.Round(NextPos.z));
        MasterMovement.EnemyPositions.Add(NextPos);

        return NextPos;
    }
}
