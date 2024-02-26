using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMovement : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();
    //public List<Transform> pawnMovePoints = new List<Transform>();
    Transform movePoint;
    private int bishopRSide, bishopZZSide, bishopFZZSide, towerSide, knightSide;
    private List<Vector3> knightPositions = new List<Vector3>();

    public bool movable, playerMoving;//, moving;

    public string gameState, peaceState;

    private Transform target;

    //private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        peaceState = "STOP";
    }

    // Update is called once per frame
    void Update()
    {
        playerMoving = GameObject.FindWithTag("Player").gameObject.GetComponent<Movement_King>().moving;

        gameState = this.gameObject.GetComponent<gameController>().gameState;

        GameObject[] pawns = GameObject.FindGameObjectsWithTag("Pawn");
        GameObject[] rooks = GameObject.FindGameObjectsWithTag("Rook");
        GameObject[] knights = GameObject.FindGameObjectsWithTag("Knight");
        GameObject[] bishopszigzag = GameObject.FindGameObjectsWithTag("BishopZigZag");
        GameObject[] bishopsrandom = GameObject.FindGameObjectsWithTag("BishopRandom");
        GameObject[] bishopsfullzigzag = GameObject.FindGameObjectsWithTag("BishopFullZigZag");
                
        pieceMove(pawns);
        pieceMove(rooks);
        pieceMove(bishopszigzag);
        pieceMove(bishopsfullzigzag);
        pieceMove(bishopsrandom);
        pieceMove(knights);

        if (peacesInPlace(pawns) && peacesInPlace(rooks) && peacesInPlace(bishopszigzag)
            && peacesInPlace(bishopsfullzigzag) && peacesInPlace(bishopsrandom) && peacesInPlace(knights)
            && peaceState == "STOP")
        {
            //moving = false;
            this.gameObject.GetComponent<gameController>().gameState = "TURN_KING";
            //Debug.Log("king turn");
        }
        /*else
        {
            moving = true;
            this.gameObject.GetComponent<gameController>().gameState = "TURN_ENEMY";
        }*/

        /*if (peacesInPlace(pawns) && peacesInPlace(rooks) && peacesInPlace(bishopszigzag)
            && peacesInPlace(bishopsfullzigzag)&& peacesInPlace(bishopsrandom) && peacesInPlace(knights) &&*/
        if (!playerMoving && gameState == "TURN_ENEMY" && peaceState == "MOVE")
        {
            //moving = true;
            peaceState = "STOP";
            //Debug.Log("enemy turn");
            
            pawnTarget(pawns);
            rookTarget(rooks);
            bishopZigZagTarget(bishopszigzag);
            bishopFullZigZagTarget(bishopsfullzigzag);
            bishopRandomTarget(bishopsrandom);
            knightTarget(knights);
            
            //this.gameObject.GetComponent<gameController>().gameState = "TURN_KING";
        }
        
        positions.Clear();
    }
    void knightTarget(GameObject[] knights)
    {
        foreach(GameObject knight in knights)
        {
            //knightSide = knight.GetComponent<Knight>().movement;

            movePoint = knight.GetComponent<Knight>().target.transform;
            
            //Debug.Log(knightSide);
            if (isObjectHere(new Vector3(knight.transform.position.x + 4, knight.transform.position.y, knight.transform.position.z - 2))
                && !positions.Contains(new Vector3(knight.transform.position.x + 4, knight.transform.position.y, knight.transform.position.z - 2)))
            {
                knightPositions.Add(new Vector3(knight.transform.position.x + 4, knight.transform.position.y, knight.transform.position.z - 2));
                //Debug.Log("caso 0");
            }
            if (isObjectHere(new Vector3(knight.transform.position.x + 4, knight.transform.position.y, knight.transform.position.z + 2))
                && !positions.Contains(new Vector3(knight.transform.position.x + 4, knight.transform.position.y, knight.transform.position.z + 2)))
            {
                knightPositions.Add(new Vector3(knight.transform.position.x + 4, knight.transform.position.y, knight.transform.position.z + 2));
                //Debug.Log("caso 1");
            }
            if (isObjectHere(new Vector3(knight.transform.position.x - 4, knight.transform.position.y, knight.transform.position.z - 2))
                && !positions.Contains(new Vector3(knight.transform.position.x - 4, knight.transform.position.y, knight.transform.position.z - 2)))
            {
                knightPositions.Add(new Vector3(knight.transform.position.x - 4, knight.transform.position.y, knight.transform.position.z - 2));
                //Debug.Log("caso 2");
            }
            if (isObjectHere(new Vector3(knight.transform.position.x - 4, knight.transform.position.y, knight.transform.position.z + 2))
                && !positions.Contains(new Vector3(knight.transform.position.x - 4, knight.transform.position.y, knight.transform.position.z + 2)))
            {
                knightPositions.Add(new Vector3(knight.transform.position.x - 4, knight.transform.position.y, knight.transform.position.z + 2));
                //Debug.Log("caso 3");
            }
            if (isObjectHere(new Vector3(knight.transform.position.x - 2, knight.transform.position.y, knight.transform.position.z + 4))
                && !positions.Contains(new Vector3(knight.transform.position.x - 2, knight.transform.position.y, knight.transform.position.z + 4)))
            {
                knightPositions.Add(new Vector3(knight.transform.position.x - 2, knight.transform.position.y, knight.transform.position.z + 4));
                //Debug.Log("caso 4");
            }
            if (isObjectHere(new Vector3(knight.transform.position.x + 2, knight.transform.position.y, knight.transform.position.z + 4))
                && !positions.Contains(new Vector3(knight.transform.position.x + 2, knight.transform.position.y, knight.transform.position.z + 4)))
            {
                knightPositions.Add(new Vector3(knight.transform.position.x + 2, knight.transform.position.y, knight.transform.position.z + 4));
                //Debug.Log("caso 5");
            }
            if (isObjectHere(new Vector3(knight.transform.position.x - 2, knight.transform.position.y, knight.transform.position.z - 4))
                && !positions.Contains(new Vector3(knight.transform.position.x - 2, knight.transform.position.y, knight.transform.position.z - 4)))
            {
                knightPositions.Add(new Vector3(knight.transform.position.x - 2, knight.transform.position.y, knight.transform.position.z - 4));
                //Debug.Log("caso 6");
            }
            if (isObjectHere(new Vector3(knight.transform.position.x + 2, knight.transform.position.y, knight.transform.position.z - 4))
                && !positions.Contains(new Vector3(knight.transform.position.x + 2, knight.transform.position.y, knight.transform.position.z - 4)))
            {
                knightPositions.Add(new Vector3(knight.transform.position.x + 2, knight.transform.position.y, knight.transform.position.z - 4));
                //Debug.Log("caso 7");
            }
            

            if (knightPositions.Count == 0)
            {
                positions.Add(movePoint.position);
            }
            else
            {
                knightSide = Random.Range(0, knightPositions.Count);
                movePoint.position = knightPositions[knightSide];
                positions.Add(knightPositions[knightSide]);
            }
            knightPositions.Clear();
        }
    }

    void bishopRandomTarget(GameObject[] bishopRand)
    {
        foreach (GameObject bishopR in bishopRand)
        {
            bishopRSide = bishopR.GetComponent<Bishop_Random>().side;
            movePoint = bishopR.GetComponent<Bishop_Random>().target.transform;

            if (bishopRSide == 1)
            {
                Vector3 bishopTargetPos = new Vector3(bishopR.transform.position.x + 2, bishopR.transform.position.y, bishopR.transform.position.z - 2);

                if (!positions.Contains(bishopTargetPos))
                {
                    if (isObjectHere(bishopTargetPos))
                    {
                        movePoint.position = bishopTargetPos;
                        positions.Add(bishopTargetPos);
                    }
                    else
                    {
                        bishopTargetPos = new Vector3(bishopR.transform.position.x - 2, bishopR.transform.position.y, bishopR.transform.position.z - 2);
                        if (!positions.Contains(bishopTargetPos))
                        {
                            if (isObjectHere(bishopTargetPos))
                            {
                                movePoint.position = bishopTargetPos;
                                positions.Add(bishopTargetPos);
                            }
                            else
                            {
                                positions.Add(bishopTargetPos);
                                Destroy(bishopR);
                            }
                        }
                        else
                        {
                            positions.Add(bishopTargetPos);
                        }
                    }
                }
                else
                {
                    bishopTargetPos = new Vector3(bishopR.transform.position.x - 2, bishopR.transform.position.y, bishopR.transform.position.z - 2);
                    if (!positions.Contains(bishopTargetPos))
                    {
                        if (isObjectHere(bishopTargetPos))
                        {
                            movePoint.position = bishopTargetPos;
                            positions.Add(bishopTargetPos);
                        }
                        else
                        {
                            positions.Add(bishopTargetPos);
                        }
                    }
                    else
                    {
                        positions.Add(bishopTargetPos);
                    }
                }
            }
            else
            {
                Vector3 bishopTargetPos = new Vector3(bishopR.transform.position.x - 2, bishopR.transform.position.y, bishopR.transform.position.z - 2);

                if (!positions.Contains(bishopTargetPos))
                {
                    if (isObjectHere(bishopTargetPos))
                    {
                        movePoint.position = bishopTargetPos;
                        positions.Add(bishopTargetPos);
                    }
                    else
                    {
                        bishopTargetPos = new Vector3(bishopR.transform.position.x + 2, bishopR.transform.position.y, bishopR.transform.position.z - 2);
                        if (!positions.Contains(bishopTargetPos))
                        {
                            if (isObjectHere(bishopTargetPos))
                            {
                                movePoint.position = bishopTargetPos;
                                positions.Add(bishopTargetPos);
                            }
                            else
                            {
                                positions.Add(bishopTargetPos);
                                Destroy(bishopR);
                            }
                        }
                        else
                        {
                            positions.Add(bishopTargetPos);
                        }
                    }
                }
                else
                {
                    bishopTargetPos = new Vector3(bishopR.transform.position.x + 2, bishopR.transform.position.y, bishopR.transform.position.z - 2);
                    if (!positions.Contains(bishopTargetPos))
                    {
                        if (isObjectHere(bishopTargetPos))
                        {
                            movePoint.position = bishopTargetPos;
                            positions.Add(bishopTargetPos);
                        }
                        else
                        {
                            positions.Add(bishopTargetPos);
                        }
                    }
                    else
                    {
                        positions.Add(bishopTargetPos);
                    }
                }
            }
        }
    }

    void bishopZigZagTarget(GameObject[] bishopZ)
    {
        foreach (GameObject bishopZZ in bishopZ)
        {
            bishopZZSide = bishopZZ.GetComponent<Bishop_ZigZag>().side;
            movePoint = bishopZZ.GetComponent<Bishop_ZigZag>().target.transform;

            if (bishopZZSide == 1)
            {
                Vector3 bishopTargetPos = new Vector3(bishopZZ.transform.position.x + 2, bishopZZ.transform.position.y, bishopZZ.transform.position.z - 2);

                if (isObjectHere(bishopTargetPos))
                {
                    if (!positions.Contains(bishopTargetPos))
                    {
                        movePoint.position = bishopTargetPos;
                        positions.Add(bishopTargetPos);
                    }
                    else
                    {
                        positions.Add(bishopZZ.transform.position);
                    }
                }
                else
                {
                    Destroy(bishopZZ);
                }
            }
            else
            {
                Vector3 bishopTargetPos = new Vector3(bishopZZ.transform.position.x - 2, bishopZZ.transform.position.y, bishopZZ.transform.position.z - 2);

                if (isObjectHere(bishopTargetPos))
                {
                    if (!positions.Contains(bishopTargetPos))
                    {
                        movePoint.position = bishopTargetPos;
                        positions.Add(bishopTargetPos);
                    }
                    else
                    {
                        positions.Add(bishopZZ.transform.position);
                    }
                }
                else
                {
                    Destroy(bishopZZ);
                }
            }
        }
    }
    
    void bishopFullZigZagTarget(GameObject[] bishopF)
    {
        foreach (GameObject bishopFZZ in bishopF)
        {
            bishopFZZSide = bishopFZZ.GetComponent<Bishop_FullZigZag>().side;
            movePoint = bishopFZZ.GetComponent<Bishop_FullZigZag>().target.transform;

            if (bishopFZZSide == 1)
            {
                Vector3 bishopTargetPos = new Vector3(bishopFZZ.transform.position.x + 2, bishopFZZ.transform.position.y, bishopFZZ.transform.position.z - 2);

                if (!positions.Contains(bishopTargetPos))
                {
                    if (isObjectHere(bishopTargetPos))
                    {
                        movePoint.position = bishopTargetPos;
                        positions.Add(bishopTargetPos);
                    }
                    else
                    {
                        bishopTargetPos = new Vector3(bishopFZZ.transform.position.x - 2, bishopFZZ.transform.position.y, bishopFZZ.transform.position.z - 2);
                        if (!positions.Contains(bishopTargetPos))
                        {
                            if (isObjectHere(bishopTargetPos))
                            {
                                movePoint.position = bishopTargetPos;
                                positions.Add(bishopTargetPos);
                                //bishopFZZSide = 0;
                            }
                            else
                            {
                                positions.Add(bishopTargetPos);
                                Destroy(bishopFZZ);
                            }
                        }
                        else
                        {
                            positions.Add(bishopTargetPos);
                            //bishopFZZSide = 0;
                        }
                    }
                }
                else
                {
                    bishopTargetPos = new Vector3(bishopFZZ.transform.position.x - 2, bishopFZZ.transform.position.y, bishopFZZ.transform.position.z - 2);
                    if (!positions.Contains(bishopTargetPos))
                    {
                        if (isObjectHere(bishopTargetPos))
                        {
                            movePoint.position = bishopTargetPos;
                            positions.Add(bishopTargetPos);
                            //bishopFZZSide = 0;
                        }
                        else
                        {
                            positions.Add(bishopTargetPos);
                        }
                    }
                    else
                    {
                        positions.Add(bishopTargetPos);
                    }
                }
            }
            else
            {
                Vector3 bishopTargetPos = new Vector3(bishopFZZ.transform.position.x - 2, bishopFZZ.transform.position.y, bishopFZZ.transform.position.z - 2);

                if (!positions.Contains(bishopTargetPos))
                {
                    if (isObjectHere(bishopTargetPos))
                    {
                        movePoint.position = bishopTargetPos;
                        positions.Add(bishopTargetPos);
                    }
                    else
                    {
                        bishopTargetPos = new Vector3(bishopFZZ.transform.position.x + 2, bishopFZZ.transform.position.y, bishopFZZ.transform.position.z - 2);
                        if (!positions.Contains(bishopTargetPos))
                        {
                            if (isObjectHere(bishopTargetPos))
                            {
                                movePoint.position = bishopTargetPos;
                                positions.Add(bishopTargetPos);
                                //bishopFZZSide = 1;
                            }
                            else
                            {
                                positions.Add(bishopTargetPos);
                                Destroy(bishopFZZ);
                            }
                        }
                        else
                        {
                            positions.Add(bishopTargetPos);
                            //bishopFZZSide = 1;
                        }
                    }
                }
                else
                {
                    bishopTargetPos = new Vector3(bishopFZZ.transform.position.x + 2, bishopFZZ.transform.position.y, bishopFZZ.transform.position.z - 2);
                    if (!positions.Contains(bishopTargetPos))
                    {
                        if (isObjectHere(bishopTargetPos))
                        {
                            movePoint.position = bishopTargetPos;
                            positions.Add(bishopTargetPos);
                            //bishopFZZSide = 1;
                        }
                        else
                        {
                            positions.Add(bishopTargetPos);
                        }
                    }
                    else
                    {
                        positions.Add(bishopTargetPos);
                    }
                }
            }
        }
    }

    void rookTarget(GameObject[] rooks)
    {
        foreach(GameObject rook in rooks)
        {
            towerSide = rook.GetComponent<Rook_Line>().side;
            movePoint = rook.GetComponent<Rook_Line>().target.transform;

            if (towerSide == 1)
            {
                Vector3 rookTargetPos = new Vector3(Mathf.Round(rook.transform.position.x) + 2, rook.transform.position.y, rook.transform.position.z);
                
                if (!positions.Contains(rookTargetPos))
                {
                    if (isObjectHere(rookTargetPos))
                    {
                        movePoint.position = rookTargetPos;
                        positions.Add(rookTargetPos);
                    }
                    else
                    {
                        rookTargetPos = new Vector3(Mathf.Round(rook.transform.position.x - 2), rook.transform.position.y, rook.transform.position.z);
                        if (!positions.Contains(rookTargetPos)){
                            if (isObjectHere(rookTargetPos))
                            {
                                movePoint.position = rookTargetPos;
                                positions.Add(rookTargetPos);
                            }
                            else
                            {
                                positions.Add(rookTargetPos);
                            }
                        }
                        else
                        {
                            positions.Add(rookTargetPos);
                        }
                    }
                }
                else
                {
                    rookTargetPos = new Vector3(Mathf.Round(rook.transform.position.x - 2), rook.transform.position.y, rook.transform.position.z);
                    if (!positions.Contains(rookTargetPos))
                    {
                        if (isObjectHere(rookTargetPos))
                        {
                            movePoint.position = rookTargetPos;
                            positions.Add(rookTargetPos);
                        }
                        else
                        {
                            positions.Add(rookTargetPos);
                        }
                    }
                    else
                    {
                        positions.Add(rookTargetPos);
                    }
                }
            }
            else
            {
                Vector3 rookTargetPos = new Vector3(Mathf.Round(rook.transform.position.x - 2), rook.transform.position.y, rook.transform.position.z);

                if (!positions.Contains(rookTargetPos))
                {
                    if (isObjectHere(rookTargetPos))
                    {
                        movePoint.position = rookTargetPos;
                        positions.Add(rookTargetPos);
                    }
                    else
                    {
                        rookTargetPos = new Vector3(Mathf.Round(rook.transform.position.x + 2), rook.transform.position.y, rook.transform.position.z);
                        if (!positions.Contains(rookTargetPos))
                        {
                            if (isObjectHere(rookTargetPos))
                            {
                                movePoint.position = rookTargetPos;
                                positions.Add(rookTargetPos);
                            }
                            else
                            {
                                positions.Add(rookTargetPos);
                            }
                        }
                        else
                        {
                            positions.Add(rookTargetPos);
                        }
                    }
                }
                else
                {
                    rookTargetPos = new Vector3(Mathf.Round(rook.transform.position.x + 2), rook.transform.position.y, rook.transform.position.z);
                    if (!positions.Contains(rookTargetPos))
                    {
                        if (isObjectHere(rookTargetPos))
                        {
                            movePoint.position = rookTargetPos;
                            positions.Add(rookTargetPos);
                        }
                        else
                        {
                            positions.Add(rookTargetPos);
                        }
                    }
                    else
                    {
                        positions.Add(rookTargetPos);
                    }
                }
            }
        }
    }

    void pawnTarget(GameObject[] pawns)
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").gameObject.transform.position;

        foreach (GameObject pawn in pawns)
        {
            movePoint = pawn.GetComponent<Pawn>().target.transform;
            Vector3 pawnTargetPos;

            if (playerPos == new Vector3(Mathf.Round(movePoint.position.x + 2), movePoint.position.y, Mathf.Round(movePoint.position.z - 2)))
            {
                pawnTargetPos = new Vector3(Mathf.Round(movePoint.position.x + 2), movePoint.position.y, Mathf.Round(movePoint.position.z - 2));
            }
            else if (playerPos == new Vector3(Mathf.Round(movePoint.position.x - 2), movePoint.position.y, Mathf.Round(movePoint.position.z - 2)))
            {
                pawnTargetPos = new Vector3(Mathf.Round(movePoint.position.x - 2), movePoint.position.y, Mathf.Round(movePoint.position.z - 2));
            }
            else if (playerPos != movePoint.position + new Vector3(movePoint.position.x, movePoint.position.y, Mathf.Round(movePoint.position.z - 2)))
            {
                pawnTargetPos = new Vector3(movePoint.position.x, movePoint.position.y, Mathf.Round(movePoint.position.z - 2));
            }
            else
            {
                pawnTargetPos = movePoint.position;
            }


            //Vector3 pawnTargetPos = new Vector3(movePoint.transform.position.x, movePoint.transform.position.y, Mathf.Round(movePoint.transform.position.z - 2));

            //Debug.Log(pawnTargetPos);

            //pawnMovePoints.Add(movePoint.transform);

            if (!positions.Contains(pawnTargetPos))
            {
                //Debug.Log(posi);
                if (isObjectHere(pawnTargetPos))
                {
                    movePoint.position = pawnTargetPos;
                    positions.Add(pawnTargetPos);
                }

            }
        }
    }

    void pieceMove(GameObject[] pieces)
    {
        foreach (GameObject piece in pieces)
        {
            float speed = 5f;
            if (piece.tag == "Knight")
            {
                speed = Mathf.Sqrt(20) / 0.4f;
            }
            else if (piece.tag == "BishopRandom" || piece.tag == "BishopZigZag" || piece.tag == "BishopFullZigZag")
            {
                speed = Mathf.Sqrt(8) / 0.4f;
            }

            switch (piece.tag)
            {
                case ("Rook"):
                    target = piece.GetComponent<Rook_Line>().target.transform;
                    break;
                case ("Knight"):
                    target = piece.GetComponent<Knight>().target.transform;
                    break;
                case ("BishopRandom"):
                    target = piece.GetComponent<Bishop_Random>().target.transform;
                    break;
                case ("BishopZigZag"):
                    target = piece.GetComponent<Bishop_ZigZag>().target.transform;
                    break;
                case ("BishopFullZigZag"):
                    target = piece.GetComponent<Bishop_FullZigZag>().target.transform;
                    break;
                case ("Pawn"):
                    target = piece.GetComponent<Pawn>().target.transform;
                    break;
            }

            if (Vector3.Distance(piece.transform.position, target.position) != 0)
            {
                piece.transform.position = Vector3.MoveTowards(piece.transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }

    bool isObjectHere(Vector3 position)
    {
        Collider[] intersecting = Physics.OverlapSphere(position, 0.5f);
        if (intersecting.Length == 0)
        {
            //Debug.Log("No overlap");
            return false;
        }
        else
        {
            //Debug.Log("Overlap");
            return true;
        }
    }

    bool peacesInPlace(GameObject[] pieces)
    {
        int trues = 0;

        foreach (GameObject piece in pieces)
        {
            bool inPlace = piece.gameObject.transform.GetChild(0).gameObject.GetComponent<CheckMovable>().inPlace;

            if (inPlace)
            {
                trues++;
            }
        }

        //Debug.Log("Fitxes:"+pieces.Length+"\nEn el lloc:"+trues);

        if(trues == pieces.Length)
        {
            //Debug.Log("gud");
            return true;
        }
        else
        {
            //Debug.Log("bado");
            return false;
        }
    }
}