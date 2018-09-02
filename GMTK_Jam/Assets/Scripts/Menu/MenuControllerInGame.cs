using UnityEngine;
using UnityEngine.SceneManagement;

namespace jam_jam
{
    public class MenuControllerInGame : MonoBehaviour
    {

        public void NextLevel()
        {
            LevelsManager.Instance.NextLevel();
            SceneManager.LoadScene(LevelsManager.Instance.GetCurrentLevel());
        }

        public void ReturnMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
