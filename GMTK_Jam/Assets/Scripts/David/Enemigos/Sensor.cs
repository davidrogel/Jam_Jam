using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jam_jam
{
    public enum Direction
    {
        N, S, E, W,
    }

    public class Sensor : MonoBehaviour
    {
        public Direction dir;

        [HideInInspector]
        public bool go;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // player
            if(collision.gameObject.layer == 11)
            {
                go = true;
            }
        }
    }
}