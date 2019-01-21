using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class InputControll : MonoBehaviour
    {
        Vector3 firstTouchPosition;   //Первая позиция касания
        Vector3 lastTouchPosition;    //Последняя позиция касания
        [SerializeField] float dragDistance = 10f;  //Минимальная дистанция для определения свайпа

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

        void UnityEditorInput()
        {
            if (Input.GetKeyDown("space"))
            {
                Main.self.Player.State = PlayerFSM.Jump;
            }
            if (Input.GetKeyDown("left"))
            {
                Main.self.Player.State = PlayerFSM.SwipeLeft;
            }
            if (Input.GetKeyDown("right"))
            {
                Main.self.Player.State = PlayerFSM.SwipeRight;
            }
        }


        void MobileInput()
        {
            if (Input.touchCount <= 0)
            { return; }
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                touchPositions.Add(Input.touches[0].position);
            }

            //Касание
            if (Input.touches[0].phase == TouchPhase.Ended && touchPositions.Count == 0)
            {
                Main.self.Player.State = PlayerFSM.Jump;
            }

            //Свайп
            if (Input.touches[0].phase == TouchPhase.Ended && touchPositions.Count > 0)
            {
                firstTouchPosition = touchPositions[0];
                lastTouchPosition = touchPositions[touchPositions.Count - 1];


                if (Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x) > Mathf.Abs(lastTouchPosition.y - firstTouchPosition.y))
                {
                    //Если дистанция свайпа коротка
                    if (Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x) < dragDistance)
                    {
                        return;
                    }

                    if (lastTouchPosition.x < firstTouchPosition.x)
                    {
                        //Свайп влево
                        Main.self.Player.State = PlayerFSM.SwipeLeft;
                    }

                    if (lastTouchPosition.x > firstTouchPosition.x)
                    {
                        //Свайп вправо
                        Main.self.Player.State = PlayerFSM.SwipeRight;
                    }
                }
                touchPositions.Clear();
            }
        }
    }
}