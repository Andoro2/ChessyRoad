using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    private GameObject player, Wsquare, Bsquare, square;
    public int tableLength, actualLength, depth;
    private bool startColor, colorFlag;
    private Vector3 squarePosition;
    private List<GameObject> foes = new List<GameObject>();
    public float enemy;

    private bool colorStart = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = player.transform.position;
        Wsquare = Resources.Load<GameObject>("PreFabs/WhiteSquare");
        Bsquare = Resources.Load<GameObject>("PreFabs/BlackSquare");
        //loading pieces to spawn
        foes.Add(Resources.Load<GameObject>("PreFabs/B_RookL"));
        foes.Add(Resources.Load<GameObject>("PreFabs/B_Knight"));
        foes.Add(Resources.Load<GameObject>("PreFabs/B_Pawn"));
        foes.Add(Resources.Load<GameObject>("PreFabs/B_BishopFZZ"));
        foes.Add(Resources.Load<GameObject>("PreFabs/B_BishopZZ"));
        foes.Add(Resources.Load<GameObject>("PreFabs/B_BishopR"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < (depth*2))
        {
            createTerrain();
            transform.position = transform.position + new Vector3(0f, 0f, 2f);
        }
    }

    void createTerrain()
    {
        //Debug.Log("Transform.position: " + transform.position);
        actualLength = 0;
        squarePosition = transform.position;

        if (tableLength % 2 == 0) //even/par
        {
            squarePosition = new Vector3(((tableLength/2)*(-1))*2, 0, squarePosition.z);
        }
        else
        {
            squarePosition = new Vector3((((tableLength+1) / 2) * (-1))*2, 0, squarePosition.z);
        }
        
        if(startColor)
        {
            colorFlag = false;
            startColor = false;
        }
        else
        {
            colorFlag = true;
            startColor = true;
        }

        while (actualLength != tableLength)
        {
            if (colorFlag)
            {
                square = Wsquare;
                colorFlag = false;
            }
            else
            {
                square = Bsquare;
                colorFlag = true;
            }

            squarePosition = new Vector3(squarePosition.x + 2, 0, squarePosition.z);

            if(squarePosition.z >= 8)
            {
                spawnFoe();
            }

            Instantiate(square, squarePosition, Quaternion.identity);
            //Debug.Log("Square.position: " + squarePosition);
            actualLength++;
        }
    }

    void spawnFoe()
    {
        enemy = Random.Range(0, 50);

        if (enemy > 40)
        {
            Instantiate(foes[Random.Range(0, foes.Count - 1)], squarePosition, Quaternion.identity);
        }
    }
}
