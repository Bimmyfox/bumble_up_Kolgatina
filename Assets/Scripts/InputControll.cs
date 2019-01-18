using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class InputControll : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        bool swipe = false;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            {
                if (eventData.delta.x > 0)
                {
                    swipe = true;
                    //      Main.self.Player.State = PlayerFSM.SwipeRight;
                }
                else
                {
                    swipe = true;
                    //      Main.self.Player.State = PlayerFSM.SwipeLeft;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            swipe = false;
        }

        public void OnDrag(PointerEventData eventData) { }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                //if (tapPosition == Input.touches[0].position) ;
                //Touch touch = Input.GetTouch(0);
                //if (touch.phase != TouchPhase.Moved && Input.touchCount == 1)

                if (Input.touchCount == 1)
                {
                    Main.self.Player.State = PlayerFSM.Jump;
                }
            }

#if UNITY_EDITOR
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !swipe)
            {
                Main.self.Player.State = PlayerFSM.Jump;
            }
#endif
        }
    }
}