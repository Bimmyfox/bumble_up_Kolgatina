using System.Collections;
using UnityEngine;

namespace Game.Characters
{
    [RequireComponent(typeof(Player))]
    public class Player : Unit
    {
        // jumpForce = 25f;
        [SerializeField] float swipeForce = 15f;
        [SerializeField] GameObject beautifulDieEffect;

        int numStairs = 0;
        bool jumped = false;
        bool moving = false;
        PlayerState currentState = PlayerState.Idle;


        public int NumOvercomedStairs
        {
            get { return numStairs; }
        }

        public PlayerState State
        {
            get { return currentState; }
            set
            {
                //изменить состояние можно, только если игрок стоит на месте
                if (currentState == PlayerState.Idle)
                    currentState = value;
            }
        }


        protected override void Start()
        {
            base.Start();
            Main.self.Player = this;
        }

        protected override void Thrust(Vector3 direction, float jumpForce)
        {
            moving = true;
            base.Thrust(direction, jumpForce);
        }

        void FixedUpdate()
        {
            DoAction(currentState);
        }

        //передвижения
        void DoAction(PlayerState state)
        {
            if (moving)
                return;

            if (state == PlayerState.Jump)
                StartCoroutine(Jump());

            if (state == PlayerState.SwipeLeft)
                SwipeLeft();

            if (state == PlayerState.SwipeRight)
                SwipeRight();
        }


        void SwipeLeft()
        {
            StartCoroutine(Swipe(Vector3.back));
        }

        void SwipeRight()
        {
            StartCoroutine(Swipe(Vector3.forward));
        }

 
        IEnumerator Swipe(Vector3 destination)
        {
            Thrust(Vector3.up, jumpForce / 2f); // при движении в сторону высота прыжка меньше
            yield return new WaitForSeconds(0.01f);
            Thrust(destination, swipeForce);
        }

        IEnumerator Jump()
        {
            jumped = true;
            rb.velocity = Vector3.zero;
            Thrust(Vector3.up, jumpForce);
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpeedUpJump());
        }

        IEnumerator SpeedUpJump()
        {
            Thrust(Vector3.left, jumpForce);
            yield return new WaitForSeconds(0.1f);
            Thrust(Vector3.down, jumpForce / 2f);
        }

        //столкновения
        void OnCollisionEnter(Collision collision)
        {
            //после соприкосания с лестницей
            //перемещение вновь доступно
            //+1 очко
            if (collision.gameObject.CompareTag("Floor") && jumped)
            {
                numStairs++;
                jumped = false;
            }

            if (collision.gameObject.CompareTag("Floor"))
            {
                moving = false;
                ResetState();
            }

            //столкнулся с врагом - "смерть"
            if (collision.gameObject.CompareTag("Enemy"))
            {
                gameObject.SetActive(false);
                Instantiate(beautifulDieEffect, transform.position, transform.rotation);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            //выпал за пределы лестницы - "смерть"
            if (other.gameObject.CompareTag("FallTrigger"))
                gameObject.SetActive(false);
        }

        protected override void ResetState()
        {
            base.ResetState();
            currentState = PlayerState.Idle;
        }
    }
}