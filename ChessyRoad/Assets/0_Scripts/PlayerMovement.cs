using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_Speed = 5f;
    public int zPos = 0;
    [SerializeField] public static Vector3 NextPosition;

    public bool HasMoved = false;

    List<string> EnemyTags = new List<string>
    {
        "Rook", "Knight", "BishopRandom", "BishopZigZag", "BishopFullZigZag", "Pawn"
    };

    private void Start()
    {
        Time.timeScale = 1f;
    }
    // Update is called once per frame
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

        if (GameController.Turn == GameController.Turns.Player && MasterMovement.InPlaceCheck(m_Enemies))
        {
            if (Input.GetKeyDown(KeyCode.W) && !HasMoved)
            {
                NextPosition = transform.position + new Vector3(0f, 0f, 2f);
                if (Input.GetKeyDown(KeyCode.A))
                {
                    NextPosition += new Vector3(-2f, 0f, 0f);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    NextPosition += new Vector3(2f, 0f, 0f);
                }

                HasMoved = true;
            }
            if (Input.GetKeyDown(KeyCode.S) && !HasMoved)
            {
                NextPosition = transform.position + new Vector3(0f, 0f, -2f);
                if (Input.GetKeyDown(KeyCode.A))
                {
                    NextPosition += new Vector3(-2f, 0f, 0f);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    NextPosition += new Vector3(2f, 0f, 0f);
                }
                HasMoved = true;
            }
            if (Input.GetKeyDown(KeyCode.D) && !HasMoved)
            {
                NextPosition = transform.position + new Vector3(2f, 0f, 0f);
                if (Input.GetKeyDown(KeyCode.W))
                {
                    NextPosition += new Vector3(0f, 0f, 2f);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    NextPosition += new Vector3(0f, 0f, -2f);
                }
                HasMoved = true;
            }
            if (Input.GetKeyDown(KeyCode.A) && !HasMoved)
            {
                NextPosition = transform.position + new Vector3(-2f, 0f, 0f);
                if (Input.GetKeyDown(KeyCode.W))
                {
                    NextPosition += new Vector3(0f, 0f, 2f);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    NextPosition += new Vector3(0f, 0f, -2f);
                }
                HasMoved = true;
            }

            if (transform.position != NextPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, NextPosition, m_Speed * Time.deltaTime);
            }
                
            if (transform.position == NextPosition && HasMoved)
            {
                if(!CheckGround(transform.position))
                {
                    Rigidbody rb = GetComponent<Rigidbody>();

                    rb.constraints = RigidbodyConstraints.None;
                }

                if (transform.position.z > zPos)
                {
                    GameController.ScorePoints(1);
                    zPos = (int)transform.position.z;
                }

                foreach (GameObject Enemy in m_Enemies)
                {
                    if (Enemy.transform.position == NextPosition) Enemy.GetComponent<InPlaceChecker>().Death();
                }

                GameController.Turn = GameController.Turns.Enemy;

                HasMoved = false;
            }
        }

        if (transform.position.y < -5)
        {
            GameObject.FindWithTag("GameController").GetComponent<GameController>().Death();
            if(transform.GetChild(0).gameObject.activeSelf) transform.GetChild(0).gameObject.SetActive(false);
            //if (transform.GetChild(1).gameObject.activeSelf) transform.GetChild(1).gameObject.SetActive(false);


            Rigidbody rb = GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezePositionY;

            //Destroy(gameObject);
        }
    }
    public void IsEnemyHere(Vector3 position)
    {
        Collider[] m_Intersecting = Physics.OverlapSphere(position, 0.5f);

        foreach(Collider c in m_Intersecting)
        {
            if (EnemyTags.Contains(c.tag))
            {
                // Debug.Log(c.gameObject.name);
                c.gameObject.GetComponent<InPlaceChecker>().Death();
            }
        }
    }
    static public bool CheckGround(Vector3 position)
    {
        Collider[] m_Intersecting = Physics.OverlapSphere(position, 0.5f);
        foreach(Collider c in m_Intersecting)
        {
            if (c.CompareTag("Ground"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}
