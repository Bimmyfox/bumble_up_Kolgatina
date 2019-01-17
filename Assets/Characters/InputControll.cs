using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class InputControll : MonoBehaviour, IBeginDragHandler, IDragHandler
    {

        public void OnBeginDrag(PointerEventData eventData)
        {
            if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
            {
                if (eventData.delta.x > 0)
                {
                    Main.self.Player.SwipeRight();
                }
                else
                {
                    Main.self.Player.SwipeLeft();
                }
            }
        }

        public void OnDrag(PointerEventData eventData) { }


        void Update()
        {
            if (Input.touches.Length == 1)
            {
                Main.self.Player.Jump();
            }

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Main.self.Player.Jump();
            }
#endif
        }
    }
}