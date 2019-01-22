using UnityEngine;

//при пересечении игроком триггера спавнится новая лестница
//новая лестница частично накладывается на старую
//чтобы при уничтожении старой не было "дыры" на сцене
namespace Game.Environment
{
    public class StairwaySpawn : MonoBehaviour
    {
        [SerializeField] GameObject stairway;
        int numNotStairsObjectInStairway = 3;
        Vector3 newStairwayPosition;

        void Start()
        {
            try
            {
                stairway.GetComponent<GameObject>();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
                return;
            }

            newStairwayPosition = transform.parent.GetChild(0).transform.position;
            newStairwayPosition.x -= transform.parent.childCount - numNotStairsObjectInStairway; //новая лестница пересекается со старой
            newStairwayPosition.y += transform.parent.childCount - numNotStairsObjectInStairway; //(незаметно игроку)
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                Instantiate(stairway, newStairwayPosition, transform.rotation);
        }
    }
}