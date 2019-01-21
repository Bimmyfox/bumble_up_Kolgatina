using UnityEngine;

public class FollowingPlayerCamera : MonoBehaviour {

    [SerializeField] float move_speed = 3;        
    [SerializeField] Transform target;            
    [SerializeField] Vector3 distance = new Vector3(6.6f, 1f, 5f);

    void Start()
    {
        FindAndTargetPlayer();
        if (target == null)
            return;
    }

    void FixedUpdate()
    {
        if (target == null)
            return;
        transform.position = Vector3.Lerp(transform.position, target.position + distance, Time.fixedDeltaTime * move_speed);
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
