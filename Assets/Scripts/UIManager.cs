using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] Text points;
        [SerializeField] Text pointsEndGame;
        [SerializeField] GameObject gameOverPanel;
        bool gameOverAndResultWasShown = false;


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
                StartCoroutine(ShowDefeat());
            }
        }

        IEnumerator ShowDefeat()
        {
            yield return new WaitForSeconds(.5f);
            pointsEndGame.text = string.Format("{0}", Main.self.Player.NumOvercomedStairs);
            gameOverPanel.SetActive(true);
            gameOverAndResultWasShown = true;
        }

        public void Restart()
        {
            SceneManager.LoadScene("Main");
        }

        public void Quit()
        {

            Application.Quit();


#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}