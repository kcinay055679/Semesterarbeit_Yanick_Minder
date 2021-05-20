using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    
    private GameObject player;
    private Rigidbody2D rigid;
    private Vector3 targetpos;
    public Vector3 startpos;
    private float speed = 3.0f;
    private float step;
    public bool hited = false;
    public bool grounded = false;
    PolygonCollider2D collider;

    Vector3 heading;
    Vector3 dir;
    public Vector3 Homeheading;
    public Vector3 Homedir;

    void Start()
    {

        startpos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        targetpos = player.transform.position;

        heading = new Vector3(transform.position.x-20f+ UnityEngine.Random.Range(0,10),transform.position.y,transform.position.z) - player.transform.position;
        dir = heading / heading.magnitude;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (grounded && !hited)
        {
            transform.Rotate(Vector3.forward * 450 * Time.deltaTime);
            transform.Translate(-Homedir * speed * Time.deltaTime, Space.World);

            if (transform.position.y >= startpos.y)
            {
                Destroy(gameObject);
            }
        }

        if (!grounded && !hited)
        {
            transform.Translate(-dir *speed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward * 450 * Time.deltaTime);
        }

        if (transform.position.y < -9)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D( Collider2D other )
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance == false)
            {
                hited = true;
                rigid.isKinematic = false;
                collider.isTrigger = false;
                GameObject.Find("GameHandler").GetComponent<GameHandler>().Health -= 1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance = true;
                StartCoroutine(WaitForResistance());
            }
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;

            Homeheading = transform.position - startpos;

            if (Homeheading.x != 0  && Homeheading.x != 0 && Homeheading.x != 0 && Homeheading.magnitude != 0)
            {
                Homedir = Homeheading / Homeheading.magnitude;
                //new Vector3(Homeheading.x / Homeheading.magnitude, Homeheading.y / Homeheading.magnitude, Homeheading.z / Homeheading.magnitude);
            }

            if (hited)
            {
                StartCoroutine(WaitTillDestroy());
            }
        }
    }

    IEnumerator WaitTillDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        while (GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance == false)
        {
            Destroy(gameObject);
            break;
        }
    }
    IEnumerator WaitForResistance()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance = false;
    }
}
