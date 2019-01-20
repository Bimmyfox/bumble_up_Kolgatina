using System.Collections;
using UnityEngine;

namespace Game
{
    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField] float spawnDeltaTime = 3f;
        [SerializeField] float distanceToPlayer = 6f;
        [SerializeField] Enemy enemy;

        Vector3 spawnPosition;

        void Start()
        {
            spawnPosition = Vector3.up;
            enemy = enemy.GetComponent<Enemy>();

            StartCoroutine(SpawnEnemies());
        }

        IEnumerator Spawn()
        {
            Instantiate(enemy, spawnPosition, transform.rotation, transform);
            yield return new WaitForSeconds(spawnDeltaTime);
        }

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                spawnPosition.x = Main.self.Player.transform.position.x - distanceToPlayer;
                spawnPosition.y = Main.self.Player.transform.position.y + distanceToPlayer;
                yield return StartCoroutine(Spawn());
            }
        }
    }
}