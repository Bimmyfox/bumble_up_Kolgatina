using UnityEngine;

namespace Game
{
    public class GenerateStairway : MonoBehaviour
    {
        [SerializeField] GameObject stairway;
        Vector3 newPosition;

        void Start()
        {
            try
            {
                stairway.GetComponent<GameObject>();
            }
            catch(System.Exception e)
            {
                Debug.Log(e.ToString());
                return;
            }

            newPosition = transform.parent.GetChild(0).transform.position;
            newPosition.x -= transform.parent.childCount - 3; //новая лестница пересекается со старой
            newPosition.y += transform.parent.childCount - 3; //(незаметно игроку)
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Instantiate(stairway, newPosition, transform.rotation);
            }
        }
    }
}