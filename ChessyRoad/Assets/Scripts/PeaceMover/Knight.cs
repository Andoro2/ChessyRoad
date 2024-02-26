using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    //public Transform movePoint;
    //private List<Vector3> positions = new List<Vector3>();
    //public Rigidbody rigidbody;
    public int movement;

    public GameObject master;
    public GameObject target;

    private void Awake()
    {
        target = this.gameObject.transform.GetChild(0).gameObject;
        target.transform.parent = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.FindGameObjectWithTag("GameController");

        //target = this.gameObject.transform.GetChild(0).gameObject;
        //target.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && master.GetComponent<MasterMovement>().movable)
        {
            movement = Random.Range(0, 8);
            
            switch (movement)
            {
                case 0:
                    if (isObjectHere(target.transform.position + new Vector3(4f, 0f, -2f)))
                    {
                        target.transform.position += new Vector3(4f, 0f, -2f);
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 1:
                    if (isObjectHere(target.transform.position + new Vector3(4f, 0f, 2f)))
                    {
                        target.transform.position += new Vector3(4f, 0f, 2f);
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 2:
                    if (isObjectHere(target.transform.position + new Vector3(-4f, 0f, -2f)))
                    {
                        target.transform.position += new Vector3(-4f, 0f, -2f);
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 3:
                    if (isObjectHere(target.transform.position + new Vector3(-4f, 0f, 2f)))
                    {
                        target.transform.position += new Vector3(-4f, 0f, 2f);
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 4:
                    if (isObjectHere(target.transform.position + new Vector3(-2f, 0f, 4f)))
                    {
                        target.transform.position += new Vector3(-2f, 0f, 4f);
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 5:
                    if (isObjectHere(target.transform.position + new Vector3(2f, 0f, 4f)))
                    {
                        target.transform.position += new Vector3(2f, 0f, 4f);
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 6:
                    if (isObjectHere(target.transform.position + new Vector3(-2f, 0f, -4f)))
                    {
                        target.transform.position += new Vector3(-2f, 0f, -4f);
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 7:
                    if (isObjectHere(target.transform.position + new Vector3(2f, 0f, -4f)))
                    {
                        target.transform.position += new Vector3(2f, 0f, -4f);
                        break;
                    }
                    else
                    {
                        break;
                    }
            }
        }*/
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
