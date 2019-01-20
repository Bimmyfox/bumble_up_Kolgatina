using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class InputControll : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        bool swipe = false;

        public void OnBeginDrag(PointerEventData eventData)
        {
            swipe = true;
            if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            {
                if (eventData.delta.x > 0)
                {
                    Main.self.Player.State = PlayerFSM.SwipeRight;
                }
                else
                {
                    Main.self.Player.State = PlayerFSM.SwipeLeft;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            swipe = false;
        }

        void Update()
        {
            if (Input.touchCount == 2)
            {
                Main.self.Player.State = PlayerFSM.Jump;
            }

#if UNITY_EDITOR
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !swipe)
            {
                Main.self.Player.State = PlayerFSM.Jump;
            }
#endif
        }

        public void OnDrag(PointerEventData eventData) { }

    }
}