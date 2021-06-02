using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update+
    Rigidbody2D rigid;
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D( Collision2D other )
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(Waitfforpos());
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance == false)
            {
                
                GameObject.Find("GameHandler").GetComponent<GameHandler>().Health -= 1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance = true;
                StartCoroutine(WaitForResistance());
            }
        }

    }
    IEnumerator Waitfforpos()
    {
        yield return new WaitForSeconds(10);
        rigid = GetComponent<Rigidbody2D>();
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;


    }
    IEnumerator WaitForResistance()
    {
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance = false;
    }
}

