using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] Text points;
        [SerializeField] Text pointsEndGame;
        [SerializeField] GameObject gameOverPanel;
        bool gameOverAndResultWasShown;

        
        void Update()
        {
            if (gameOverAndResultWasShown)
                return;

            if (Main.self.StateGame == StateGame.PLAY)
            {
                points.text = string.Format("{0}", Main.self.Player.NumOvercomedStairs);
            }

            if (Main.self.StateGame == StateGame.DEFEAT)
            {
                ShowDefeat();
                return;
            }
        }

        void ShowDefeat()
        {
            pointsEndGame.text = string.Format("{0}", Main.self.Player.NumOvercomedStairs);
            gameOverPanel.SetActive(true);
            gameOverAndResultWasShown = true;
        }
    }
}