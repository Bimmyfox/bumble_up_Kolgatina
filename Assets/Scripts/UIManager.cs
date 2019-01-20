using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] Text points;

        void Update()
        {
            points.text = string.Format("{0}", Main.self.Player.NumOvercomedStairs);
        }
    }
}