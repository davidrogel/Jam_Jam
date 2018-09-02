using UnityEngine;

namespace jam_jam
{
    public class SetUp : MonoBehaviour
    {

        void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DontDestroyOnLoad(this.gameObject);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }

        void Close()
        {
            Application.Quit();
        }
    }
}
