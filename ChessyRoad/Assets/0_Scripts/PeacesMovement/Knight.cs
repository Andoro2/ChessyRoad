using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float m_Speed = 5f;

    public List<Vector3> m_PositionsOptions = new List<Vector3>();

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

        // Revisa las ocho posibles posiciones y añade las que estén libres
        List<Vector3> Movements = new List<Vector3>
        {
            new Vector3(transform.position.x + 4, transform.position.y, transform.position.z - 2),
            new Vector3(transform.position.x + 4, transform.position.y, transform.position.z + 2),
            new Vector3(transform.position.x - 4, transform.position.y, transform.position.z - 2),
            new Vector3(transform.position.x - 4, transform.position.y, transform.position.z + 2),
            new Vector3(transform.position.x - 2, transform.position.y, transform.position.z + 4),
            new Vector3(transform.position.x + 2, transform.position.y, transform.position.z + 4),
            new Vector3(transform.position.x - 2, transform.position.y, transform.position.z - 4),
            new Vector3(transform.position.x + 2, transform.position.y, transform.position.z - 4)
        };
        foreach(Vector3 Move in Movements)
        {
            if (MasterMovement.isObjectHere(Move) && !MasterMovement.EnemyPositions.Contains(Move))
            {
                AvailablePositions.Add(Move);
            }
        }

        if (AvailablePositions.Count == 0)
        {
            MasterMovement.EnemyPositions.Add(transform.position);
            AvailablePositions.Add(transform.position);
        }
        else
        {
            foreach(Vector3 Position in AvailablePositions)
            {
                if (Position == m_Player.transform.position)
                {
                    NextPos = Position;
                }
            }
            if (NextPos != m_Player.transform.position)
            {
                NextPos = AvailablePositions[Random.Range(0, AvailablePositions.Count)];
            }
            MasterMovement.EnemyPositions.Add(NextPos);
        }

        return NextPos;
    }
}
