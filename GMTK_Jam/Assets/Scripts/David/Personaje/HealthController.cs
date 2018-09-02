using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jam_jam
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField]
        private int health = 3;
        private int currentHealth = 0;

        private void Start()
        {
            currentHealth = health;
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public int GetHealth()
        {
            return health;
        }

        public void ApplyDmg(int value = 1)
        {
            currentHealth -= value;
        }
    }
}