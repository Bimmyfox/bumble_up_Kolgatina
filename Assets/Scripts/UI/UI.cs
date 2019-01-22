using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] Text points;
        [SerializeField] Text pointsEndGame;
        [SerializeField] GameObject gameOverPanel;


        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }

        public void Exit()
        {

            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        void Update()
        {
            if (Main.self.StateGame == StateGame.PLAY)
                points.text = string.Format("{0}", Main.self.Player.NumOvercomedStairs);

            if (Main.self.StateGame == StateGame.DEFEAT)
                StartCoroutine(ShowDefeat());
        }

        IEnumerator ShowDefeat()
        {
            yield return new WaitForSeconds(.5f);
            pointsEndGame.text = string.Format("{0}", Main.self.Player.NumOvercomedStairs);
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}