using System.Collections;
using UnityEngine;

namespace Game.Characters
{
    [RequireComponent(typeof(Enemy))]
    public class Enemy : Unit
    {
        //jumpForce = 17f
        [SerializeField] float thrust = 5.08f;
        [SerializeField] float speed = 1.2f;

        protected override void Start()
        {
            base.Start();
            deltaForcesTime = 1 / thrust;
            SetRandomColor();
            SetRandomRotation();
        }

        IEnumerator Jump()
        {
            rb.velocity = Vector3.zero;
            Thrust(Vector3.up, jumpForce);
            yield return new WaitForSeconds(deltaForcesTime);
            Thrust(Vector3.right, thrust);
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

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("FallTrigger"))
            {
                Destroy(gameObject);
            }
        }

        void SetRandomRotation()
        {
            transform.rotation = Random.rotation;
        }

        void SetRandomColor()
        {
            GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }
}