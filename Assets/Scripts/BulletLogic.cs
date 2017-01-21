﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour {
    new Rigidbody rigidbody;
    // Use this for initialization
    bool collided = false;
    float bulletTime;

	public void Init (float _bulletTime) {        
        rigidbody = GetComponent<Rigidbody>();
        bulletTime = 1;
        rigidbody.AddForce(transform.right * Shooting.Instance.BulletForce * _bulletTime);
        StartCoroutine("Explode");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collided)
            return;
        collision.gameObject.SendMessage("Stun", SendMessageOptions.DontRequireReceiver);
        collided = true;
        rigidbody.velocity *= 0;
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(bulletTime);
        EmpController.Instance.NewEmp(transform.position, Shooting.Instance.EmpForce,
            Shooting.Instance.EmpRadius);
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
