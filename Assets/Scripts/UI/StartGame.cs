using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class StartGame : MonoBehaviour
    {
        void Awake()
        {
            Time.timeScale = 1;
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
#endif
            if (Input.touchCount > 0)
                SceneManager.LoadScene("Main", LoadSceneMode.Single);

        }
    }
}