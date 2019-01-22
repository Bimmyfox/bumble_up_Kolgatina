using UnityEngine;
using System.Collections.Generic;
using Game.Characters;

namespace Game.InputControll
{
    public class InputControll : MonoBehaviour
    {
        [SerializeField] float dragDistance = 10f;  //минимальная дистанция для определения свайпа

        Vector3 firstTouchPosition;                 //позиция первого касания
        Vector3 lastTouchPosition;                  //позиция последнего касания
        List<Vector3> touchPositions = new List<Vector3>();


        void Start()
        {
            dragDistance *= Screen.height / 100;
        }

        void Update()
        {
            MobileInput();

#if UNITY_EDITOR
            UnityEditorInput();
#endif
        }

        void MobileInput()
        {
            if (Input.touchCount <= 0)
                return;

            if (Input.touches[0].phase == TouchPhase.Moved)
                touchPositions.Add(Input.touches[0].position);

            //Касание
            if (Input.touches[0].phase == TouchPhase.Ended && touchPositions.Count == 0)
                Main.self.Player.State = PlayerState.Jump;

            //Свайп
            if (Input.touches[0].phase == TouchPhase.Ended && touchPositions.Count > 0)
            {
                firstTouchPosition = touchPositions[0];
                lastTouchPosition = touchPositions[touchPositions.Count - 1];

                if (Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x) >
                    Mathf.Abs(lastTouchPosition.y - firstTouchPosition.y))
                {
                    //Если дистанция свайпа коротка
                    if (Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x) < dragDistance)
                        return;

                    if (lastTouchPosition.x < firstTouchPosition.x)
                        Main.self.Player.State = PlayerState.SwipeLeft;

                    if (lastTouchPosition.x > firstTouchPosition.x)
                        Main.self.Player.State = PlayerState.SwipeRight;
                }
                touchPositions.Clear();
            }
        }

        void UnityEditorInput()
        {
            if (Input.GetKeyDown("space"))
                Main.self.Player.State = PlayerState.Jump;

            if (Input.GetKeyDown("left"))
                Main.self.Player.State = PlayerState.SwipeLeft;

            if (Input.GetKeyDown("right"))
                Main.self.Player.State = PlayerState.SwipeRight;
        }
    }
}