using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
	// Start is called before the first frame update

	[SerializeField] private LayerMask WhatIsGround;
	[SerializeField] private Transform GroundCheck_0;
	[SerializeField] private Transform GroundCheck_1;
	const float GroundedRadius = .2f;
	private bool grounded;
	
	public Transform playertransform;
	Vector2 targetpos;
	private float speed = 5;

	void Start()
    {
		//transform.position = new Vector3(transform.position.x + movespeed, transform.position.y);
		 targetpos = playertransform.position;
		Debug.Log(targetpos);
	}
	 

	// Update is called once per frame
	void Update()
    {
		if(grounded == true)
        {
			StartCoroutine(WaitTillDestroy());
        }
        else
        {
			float step = speed * Time.deltaTime;
			transform.Rotate(Vector3.forward * 450 * Time.deltaTime);
			//speed *= Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, targetpos, step);
		}
	}
	void OnCollisionEnter2D( Collision2D other )
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("Collision detected"); 
		}
	}

	IEnumerator WaitTillDestroy()
	{
		yield return new WaitForSeconds(1.5f);
		Destroy(gameObject);
	}


	private void FixedUpdate()
	{
		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck_0.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
			}
		}
		Collider2D[] colliders1 = Physics2D.OverlapCircleAll(GroundCheck_1.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders1.Length; i++)
		{
			if (colliders1[i].gameObject != gameObject)
			{
				grounded = true;
			}
		}
	}
}
