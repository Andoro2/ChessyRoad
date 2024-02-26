using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_King : MonoBehaviour
{
    public float speed = 5f;
    //public Transform movePoint;
    //public Rigidbody rigidbody;

    public bool movable, moving;//, enemyMoving;
    public GameObject target, controller;

    public string gameState, enemyState;

    private void Awake()
    {
        target = this.gameObject.transform.GetChild(0).gameObject;
        target.transform.parent = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindWithTag("GameController").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        gameState = controller.GetComponent<gameController>().gameState;

        enemyState = controller.GetComponent<MasterMovement>().peaceState;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) >= 10f)
        {
            target.transform.position = new Vector3(0f,0f,0f);
            transform.position = new Vector3(0f, 0f, 0f);
            //rigidbody.velocity = new Vector3(0f, 0f, 0f);
        }

        if (transform.position == target.transform.position && moving && enemyState == "STOP")
        {
            moving = false;
            controller.gameObject.GetComponent<gameController>().gameState = "TURN_ENEMY";
            controller.GetComponent<MasterMovement>().peaceState = "MOVE";
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isObjectHere(target.transform.position + new Vector3(0f, 0f, 2f)) && gameState == "TURN_KING")//movable)
            {
                target.transform.position += new Vector3(0f, 0f, 2f);
                //controller.gameObject.GetComponent<gameController>().gameState = "TURN_ENEMY";
                //Debug.Log("enemy turn");
                moving = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isObjectHere(target.transform.position + new Vector3(0f, 0f, -2f)) && gameState == "TURN_KING")//movable)
            {
                target.transform.position += new Vector3(0f, 0f, -2f);
                //controller.gameObject.GetComponent<gameController>().gameState = "TURN_ENEMY";
                //Debug.Log("enemy turn");
                moving = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isObjectHere(target.transform.position + new Vector3(-2f, 0f, 0f)) && gameState == "TURN_KING")//movable)
            {
                target.transform.position += new Vector3(-2f, 0f, 0f);
                //controller.gameObject.GetComponent<gameController>().gameState = "TURN_ENEMY";
                //Debug.Log("enemy turn");
                moving = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isObjectHere(target.transform.position + new Vector3(2f, 0f, 0f)) && gameState == "TURN_KING")//movable)
            {
                target.transform.position += new Vector3(2f, 0f, 0f);
                //controller.gameObject.GetComponent<gameController>().gameState = "TURN_ENEMY";
                //Debug.Log("enemy turn");
                moving = true;
            }
        }
        //}
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
}
