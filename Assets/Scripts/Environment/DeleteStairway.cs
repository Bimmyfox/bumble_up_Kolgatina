using UnityEngine;

//при пересечении игроком триггера лестница уничтожается со сцены
namespace Game.Environment
{
    public class DeleteStairway : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                Destroy(transform.parent.gameObject);
        }
    }
}