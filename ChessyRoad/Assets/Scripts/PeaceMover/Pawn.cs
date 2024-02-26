using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public float speed = 5;
    public Transform movePoint;
    //public Rigidbody rigidbody;

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
        if (Vector3.Distance(transform.position, target.transform.position) >= 10f)
        {
            Destroy(target);
            Destroy(this.gameObject);
        }

        /*transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        

        if (Vector3.Distance(transform.position, target.transform.position) <= .5f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (isObjectHere(transform.position + new Vector3(0f, 0f, -2f)) && master.GetComponent<MasterMovement>().movable)
                {
                    target.transform.position += new Vector3(0f, 0f, -2f);
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
