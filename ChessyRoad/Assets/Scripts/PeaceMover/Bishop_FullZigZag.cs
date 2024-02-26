using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop_FullZigZag : MonoBehaviour
{
    public float speed;
    public Transform movePoint;
    //public Rigidbody rigidbody;
    public int side;

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
        side = Random.Range(0, 2);
        speed = 5 / 2 * Mathf.Sqrt(8);

        //target = this.gameObject.transform.GetChild(0).gameObject;
        //target.transform.parent = null;
        //movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

        /*transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) >= 10f)
        {
            Destroy(this.gameObject);
        }*/

        /*if (Vector3.Distance(transform.position, movePoint.position) <= .5f)
        {*/
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (side == 1)
            {
                if (!isObjectHere(movePoint.position + new Vector3(2f, 0f, -2f)) && master.GetComponent<MasterMovement>().movable)
                {
                    side = 0;
                    //Debug.Log("Border");
                }
            }
            else
            {
                if (!isObjectHere(movePoint.position + new Vector3(-2f, 0f, -2f)) && master.GetComponent<MasterMovement>().movable)
                {
                    side = 1;
                    //Debug.Log("Border");
                }
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
