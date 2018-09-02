using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace jam_jam
{
    public class LevelButtons : MonoBehaviour
    {

        public Transform levelsTransform;

        public List<Button> levelButtons;

        void Awake()
        {
            levelButtons = new List<Button>();

            GetAllButtons();
            SetUpLevelButtons();
        }

        void GetAllButtons()
        {
            byte count = (byte)levelsTransform.childCount;
            levelButtons.Clear();
            for (byte i = 0; i < count; i++)
            {
                levelButtons.Add(levelsTransform.GetChild(i).GetComponent<Button>());
            }
        }

        void SetUpLevelButtons()
        {
            byte count = (byte)levelButtons.Count;

            for (byte i = 0; i < count; i++)
            {
                int temp = i + 1;
                levelButtons[i].onClick.AddListener(() => LoadSceneWithNumber(temp));
                levelButtons[i].transform.GetChild(0).GetComponent<Text>().text = temp + "";
            }
        }

        void LoadSceneWithNumber(int index)
        {
            LevelsManager.Instance.SetCurrenLevel(index);
            SceneManager.LoadScene(index);
        }
    }
}
