using System.Collections;
using UnityEngine;

namespace Game
{
    public enum PlayerFSM
    {
        Idle,
        Jump,
        SwipeLeft,
        SwipeRight
    }

    public class Player : Unit
    {
        [SerializeField] float jumpForce = 25f;
        [SerializeField] float swipeForce = 5f;
        [SerializeField] float coefForce = 500f;
        [SerializeField] GameObject die;

        bool moving = false;
        PlayerFSM currentState = PlayerFSM.Idle;

        //поменять можно, только если игрок стоит на месте
        public PlayerFSM State
        {
            get { return currentState; }
            set
            {
                if (currentState == PlayerFSM.Idle)
                    currentState = value;
            }
        }

        protected override void Start()
        {
            base.Start();
            Main.self.Player = this;
        }

        void FixedUpdate()
        {
            DoAction(currentState);
        }

        protected void DoAction(PlayerFSM state)
        {
            if (moving)
                return;

            if (state == PlayerFSM.Jump)
                Jump();

            if (state == PlayerFSM.SwipeLeft)
                SwipeLeft();

            if (state == PlayerFSM.SwipeRight)
                SwipeRight();
        }

        void Jump()
        {
            Main.self.Stairway.Movement(); //!!!
            Up(jumpForce);
        }

        void Up(float jumpForce)
        {
            moving = true;
            rb.AddForce(Vector3.up * jumpForce * coefForce * Time.deltaTime);
        }

        void SwipeLeft()
        {
            moving = true;
            StartCoroutine(Swipe(Vector3.back));
        }

        void SwipeRight()
        {
            moving = true;
            StartCoroutine(Swipe(Vector3.forward));
        }

        IEnumerator Swipe(Vector3 destination)
        {
            Up(jumpForce / 2f); // при движении в сторону высота прыжка меньше
            yield return new WaitForSeconds(0.01f);
            rb.AddForce(destination * swipeForce * coefForce * Time.deltaTime);
        }

        void OnCollisionEnter(Collision collision)
        {
            //после соприкосания с лестницей
            //перемещение вновь доступно
            if (collision.gameObject.CompareTag("Floor"))
            {
                moving = false;
                ResetState();
            }

            //столкнулся с врагом - "смерть"
            if (collision.gameObject.CompareTag("Enemy"))
            {
                gameObject.SetActive(false);
                Instantiate(die, transform.position, transform.rotation);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            //выпал за пределы лестницы - "смерть"
            if (other.gameObject.CompareTag("FallTrigger"))
            {
                gameObject.SetActive(false);
            }
        }

        void ResetState()
        {
            rb.velocity = Vector3.zero;
            currentState = PlayerFSM.Idle;
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}