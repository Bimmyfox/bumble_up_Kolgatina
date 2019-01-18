using UnityEngine;

namespace Game
{
    public class StairwayGeneration : MonoBehaviour
    {
        Animator anim;
        int moveHash = Animator.StringToHash("Move");

        Transform[] stairs;
        Vector3 shift;
        int numStair = 1;

        void Start()
        {
            Main.self.Stairway = this;
            anim = GetComponent<Animator>();
            stairs = GetComponentsInChildren<Transform>();
            shift = stairs[0].position;
        }

        public void Movement()
        {
            anim.SetTrigger(moveHash);
        }
    }
}