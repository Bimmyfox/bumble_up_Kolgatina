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

        void Awake()
        {
            Time.timeScale = 1;
        }

        public void Exit()
        {

            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        public void ReturnStartScreen()
        {
            SceneManager.LoadScene("Start", LoadSceneMode.Single);
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
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