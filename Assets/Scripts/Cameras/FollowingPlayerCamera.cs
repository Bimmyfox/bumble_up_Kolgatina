using UnityEngine;

namespace Game.Cameras
{
    [RequireComponent(typeof(FollowingPlayerCamera))]
    public class FollowingPlayerCamera : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 3f;
        [SerializeField] Transform target;
        [SerializeField] Vector3 distance = new Vector3(6.6f, 1f, 4f);
        Vector3 destination = Vector3.zero;


        void Start()
        {
            FindAndTargetPlayer();
            if (target == null)
                return;

            destination.z = distance.z;
        }

        void FixedUpdate()
        {
            if (target == null)
                return;

            destination.x = target.position.x + distance.x;
            destination.y = target.position.y + distance.y;
            transform.position = Vector3.Lerp(transform.position, destination, Time.fixedDeltaTime * moveSpeed);
        }

        void FindAndTargetPlayer()
        {
            var targetObj = GameObject.FindGameObjectWithTag("Player");

            if (targetObj)
                SetTarget(targetObj.transform);
        }

        void SetTarget(Transform newTransform)
        {
            target = newTransform;
        }
    }
}