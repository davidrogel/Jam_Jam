﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jam_jam
{
    public class PauseScript : MonoBehaviour
    {
        public bool isPaused;

        public void Pause()
        {
            if (isPaused)
            {
                isPaused = false;
                Time.timeScale = 1;
            }
            else
            {
                isPaused = true;
                Time.timeScale = 0;
            }
        }
    }
}