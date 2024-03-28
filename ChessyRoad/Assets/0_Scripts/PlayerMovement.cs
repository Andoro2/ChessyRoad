using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float m_Speed = 5f;
    public int zPos = 0;
    public Vector3 HighlightPosition;
    public static Vector3 NextPosition, TargetPosition;

    public bool HasMoved = false;

    List<string> EnemyTags = new List<string>
    {
        "Rook", "Knight", "BishopRandom", "BishopZigZag", "BishopFullZigZag", "Pawn"
    };

    private void Start()
    {
        Time.timeScale = 1f;
        TargetPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        HighlightPosition = TargetPosition;
        if (GameController.Turn == GameController.Turns.Player && MasterMovement.EnemiesReachedPlace())
        {
            if(GameController.MovementStyle == GameController.MovementStyles.Fast)
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
            }

            if (GameController.MovementStyle == GameController.MovementStyles.Slow)
            {
                if (Input.GetKeyDown(KeyCode.W) && TargetPosition.z <= transform.position.z + 1)
                {
                    DeactivateHighlights();
                    if (TargetPosition == transform.position + Vector3.back * 2)
                    {
                        TargetPosition = transform.position + Vector3.forward * 2;
                    }
                    else
                    {
                        TargetPosition += Vector3.forward * 2;
                    }
                }
                if (Input.GetKeyDown(KeyCode.S) && TargetPosition.z >= transform.position.z - 1)
                {
                    DeactivateHighlights();
                    if(TargetPosition == transform.position + Vector3.forward * 2)
                    {
                        TargetPosition = transform.position + Vector3.back * 2;
                    }
                    else
                    {
                        TargetPosition += Vector3.back * 2;
                    }
                }
                if (Input.GetKeyDown(KeyCode.D) && TargetPosition.x <= transform.position.x + 1)
                {
                    DeactivateHighlights();
                    if (TargetPosition == transform.position + Vector3.left * 2)
                    {
                        TargetPosition = transform.position + Vector3.right * 2;
                    }
                    else
                    {
                        TargetPosition += Vector3.right * 2;
                    }
                }
                if (Input.GetKeyDown(KeyCode.A) && TargetPosition.x >= transform.position.x - 1)
                {
                    DeactivateHighlights();
                    if (TargetPosition == transform.position + Vector3.right * 2)
                    {
                        TargetPosition = transform.position + Vector3.left * 2;
                    }
                    else
                    {
                        TargetPosition += Vector3.left * 2;
                    }
                }

                if(new Vector3(Mathf.Round(TargetPosition.x), Mathf.Round(TargetPosition.y), Mathf.Round(TargetPosition.z))
                    != new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z)))
                {
                    if (MasterMovement.isObjectHere(TargetPosition))
                    {
                        Collider[] m_Intersecting = Physics.OverlapSphere(TargetPosition, 0.5f);
                        if(m_Intersecting[0].gameObject.transform.parent.GetComponent<HighlightSquare>() != null) m_Intersecting[0].gameObject.transform.parent.GetComponent<HighlightSquare>().SquareHighlighted();
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        DeactivateHighlights();
                        NextPosition = TargetPosition;
                        HasMoved = true;
                    }
                }
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

                foreach (GameObject Enemy in MasterMovement.m_Enemies)
                {
                    if (Enemy.transform.position == NextPosition) Enemy.GetComponent<InPlaceChecker>().Death();
                }

                transform.position = new Vector3(Mathf.Round(transform.position.x),
                    Mathf.Round(transform.position.y),
                    Mathf.Round(transform.position.z));
                GameController.Turn = GameController.Turns.EnemyMove;

                if (GameController.GameMode == GameController.GameModes.Stress)
                {
                    GameObject.FindWithTag("GameController").GetComponent<GameController>().NewEnemyTurn();
                }

                HasMoved = false;
            }
        }

        if (transform.position.y < -5)
        {
            GameObject.FindWithTag("GameController").GetComponent<GameController>().Death();
            if (GameController.PlayerColor == GameController.PlayerColors.Black) transform.GetChild(0).gameObject.SetActive(false);
            else transform.GetChild(1).gameObject.SetActive(false);


            Rigidbody rb = GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }

        if (GameController.PlayerColor == GameController.PlayerColors.Black)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void IsEnemyHere(Vector3 position)
    {
        Collider[] m_Intersecting = Physics.OverlapSphere(position, 0.5f);

        foreach(Collider c in m_Intersecting)
        {
            if (EnemyTags.Contains(c.tag))
            {
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
    void DeactivateHighlights()
    {
        List<Vector3> CheckPlaces = new List<Vector3>
        {
            transform.position,
            new Vector3(transform.position.x, transform.position.y, transform.position.z + 2),
            new Vector3(transform.position.x + 2, transform.position.y, transform.position.z + 2),
            new Vector3(transform.position.x - 2, transform.position.y, transform.position.z + 2),
            new Vector3(transform.position.x, transform.position.y, transform.position.z - 2),
            new Vector3(transform.position.x + 2, transform.position.y, transform.position.z - 2),
            new Vector3(transform.position.x - 2, transform.position.y, transform.position.z - 2),
            new Vector3(transform.position.x + 2, transform.position.y, transform.position.z),
            new Vector3(transform.position.x - 2, transform.position.y, transform.position.z)
        };

        foreach (var checkPlace in CheckPlaces)
        {
            if (MasterMovement.isObjectHere(checkPlace))
            {
                Collider[] m_Intersected = Physics.OverlapSphere(checkPlace, 0.5f);
                m_Intersected[0].gameObject.transform.parent.GetComponent<HighlightSquare>().SquareNotSelected();
            }
        }
    }
}
