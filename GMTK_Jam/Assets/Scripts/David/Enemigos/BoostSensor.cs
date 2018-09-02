using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jam_jam
{
    public class BoostSensor : MonoBehaviour
    {
        [HideInInspector]
        public bool boost = false;
        public float radius = 3;

        public LayerMask playerMask;

        private bool once = false;

        private Transform myTransform;

        private void Start()
        {
            myTransform = transform;
        }

        private void FixedUpdate()
        {
            if (Physics2D.OverlapCircle(myTransform.position, radius, playerMask))
            {
                transform.parent.GetComponent<Charger>().BoostSpeedUp();
                boost = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
