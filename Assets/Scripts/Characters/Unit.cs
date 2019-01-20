using System.Collections;
using UnityEngine;


namespace Game
{
    public class Unit : MonoBehaviour
    {
        protected Rigidbody rb;
        protected Vector3 startPosition;
        [SerializeField] protected float coefForce = 500f;
        [SerializeField] protected float jumpForce = 100f;


        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            startPosition = transform.position;
        }

        protected virtual void Thrust(Vector3 direction, float jumpForce)
        {
            rb.AddForce(direction * jumpForce * coefForce * Time.fixedDeltaTime);
        }

        protected virtual void ResetState()
        {
            rb.velocity = Vector3.zero;
            StopAllCoroutines();
        }

        protected virtual IEnumerator Jump() { yield return null; }
    }
}