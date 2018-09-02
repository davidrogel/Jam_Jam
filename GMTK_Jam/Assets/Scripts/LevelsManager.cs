using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jam_jam
{
    public class LevelsManager : MonoBehaviour
    {
        #region SINGLETON

        private static LevelsManager instance = null;

        public static LevelsManager Instance
        {
            get
            {
                return instance;
            }
        }

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }

            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        #endregion

        private int currentLevel;

        void Start()
        {
            currentLevel = 1;
        }

        public void SetCurrenLevel(int value)
        {
            currentLevel = value;
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }

        public int NextLevel()
        {
            return currentLevel++;
        }

        public int PreviousLevel()
        {
            return currentLevel--;
        }
    }
}
