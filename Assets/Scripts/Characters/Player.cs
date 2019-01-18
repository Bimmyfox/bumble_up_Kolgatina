using System.Collections;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField] float jumpForce = 350f;
        [SerializeField] float swipeForce = 35f;

        void Start()
        {
            Main.self.Player = this;
            rb = GetComponent<Rigidbody>();
        }


        public void Jump()
        {
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime);
            StartCoroutine(ResetForces());
        }

        public void SwipeLeft()
        {
            rb.AddForce(Vector3.back * swipeForce * Time.deltaTime);
            StartCoroutine(ResetForces());
        }

        public void SwipeRight()
        {
            rb.AddForce(Vector3.forward * swipeForce * Time.deltaTime);
            StartCoroutine(ResetForces());
        }

        IEnumerator ResetForces()
        {
            yield return new WaitForSeconds(1f);
            rb.velocity = Vector3.zero;
        }
    }
}