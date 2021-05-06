using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    
    private GameObject player;
    private Rigidbody2D rigid;
    private Vector2 targetpos;
    public Vector2 startpos;
    private float speed = 3f;
    private float step;
    public bool hited = false;
    public bool grounded = false;
    
    void Start()
    {
        startpos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        targetpos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded && !hited)
        {   
            rigid.isKinematic = true;
            step = speed * Time.deltaTime;
            transform.Rotate(Vector3.forward * 140 * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, startpos, step);
            
            if (transform.position.y == startpos.y)
            {
                Destroy(gameObject);
            }
        }
        
        if (!grounded && !hited)
        {
            step = speed * Time.deltaTime;
            transform.Rotate(Vector3.forward * 450 * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, targetpos, step);
        }
    }
    void OnCollisionEnter2D( Collision2D other )
    {
        //resistance = GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance;
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance == false)
            {
                hited = true;
                rigid.isKinematic = false;
                GameObject.Find("GameHandler").GetComponent<GameHandler>().Health -= 1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance = true;
                StartCoroutine(WaitForResistance());
            }
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
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
