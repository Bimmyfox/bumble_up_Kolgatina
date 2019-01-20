using UnityEngine;

namespace Game
{
    public class StairwayGeneration : MonoBehaviour
    {
        Animator anim;
        int moveHash = Animator.StringToHash("Move");


        void Start()
        {
            Main.self.Stairway = this;
            anim = GetComponent<Animator>();
        }

        public void Movement()
        {
            anim.SetTrigger(moveHash);
        }
    }
}