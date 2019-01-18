using UnityEngine;

namespace Game
{
    public class Enemy : Unit
    {

        protected override void Start()
        {
            base.Start();
        }

        void Spawn(Enemy enemy, Vector3 spawnPosition)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = spawnPosition;
        }

        //перемещение вниз
        void Movement()
        { }

        void OnTriggerEnter(Collider other)
        {
            //при выходе за пределы игрового поля встаёт в очередь на перерождение
            if (other.gameObject.CompareTag("FallTrigger"))
            {
                Main.self.enemies.Enqueue(this);
                gameObject.SetActive(false);
            }
        }
    }
}