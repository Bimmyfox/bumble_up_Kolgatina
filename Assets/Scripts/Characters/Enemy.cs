﻿using System.Collections;
using UnityEngine;


namespace Game
{
    public class Enemy : Unit
    {
        //jumpForce = 17f
        [SerializeField] float thrust = 5.08f;
        [SerializeField] float speed = 1.2f;
        float deltaForcesTime;

        protected override void Start()
        {
            base.Start();
            deltaForcesTime = 1 / thrust;
        }

        void OnCollisionEnter(Collision collision)
        {
            //после приземления на лестницу новый прыжок
            if (collision.gameObject.CompareTag("Floor"))
            {
                StopCoroutine(Jump());
                StartCoroutine(Jump());
            }
        }

        protected override IEnumerator Jump()
        {
            rb.velocity = Vector3.zero;
            Thrust(Vector3.up, jumpForce);
            yield return new WaitForSeconds(deltaForcesTime);
            Thrust(Vector3.right, thrust);
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}