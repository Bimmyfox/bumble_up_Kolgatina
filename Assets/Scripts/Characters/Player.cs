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
        // jumpForce = 25f;
        [SerializeField] float swipeForce = 15f;
        [SerializeField] GameObject die;

        int numStairs = 0;
        bool jumped = false;
        bool moving = false;
        PlayerFSM currentState = PlayerFSM.Idle;


        public int NumOvercomedStairs
        {
            get { return numStairs; }
        }

        //изменить состояние можно, только если игрок стоит на месте
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


        //передвижения
        protected void DoAction(PlayerFSM state)
        {
            if (moving)
                return;

            if (state == PlayerFSM.Jump)
                StartCoroutine(Jump());

            if (state == PlayerFSM.SwipeLeft)
                SwipeLeft();

            if (state == PlayerFSM.SwipeRight)
                SwipeRight();
        }

        protected override void Thrust(Vector3 direction, float jumpForce)
        {
            moving = true;
            base.Thrust(direction, jumpForce);
        }

        protected override IEnumerator Jump()
        {
            jumped = true;
            Thrust(Vector3.up, jumpForce);
            yield return new WaitForSeconds(.2f);
            Thrust(Vector3.left, jumpForce / 2);
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


        protected override void ResetState()
        {
            base.ResetState();
            currentState = PlayerFSM.Idle;
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}