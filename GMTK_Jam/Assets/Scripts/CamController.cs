using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace jam_jam
{
    public class CamController : MonoBehaviour
    {

        public Transform target;
        public float speed;

        public float shakeInstensity;
        private float shakeVel;
        public float shakeDecay;


        private void FixedUpdate()
        {
            Vector2 targetPos = new Vector2(target.position.x, target.position.y);

            Vector2 XYPos = new Vector2(transform.position.x, transform.position.y);

            Vector2 DesiredPos = Vector2.Lerp(XYPos, targetPos, speed * Time.deltaTime);

            shakeVel = Mathf.Lerp(shakeVel, 0, shakeDecay * Time.deltaTime);

            transform.localScale = new Vector3(shakeVel, shakeVel, shakeVel);

            transform.position = new Vector3(DesiredPos.x, DesiredPos.y, transform.position.z);
        }

        public void CamShake()
        {
            shakeVel = shakeInstensity;

            Animator anim = transform.GetChild(0).GetComponent<Animator>();

            anim.SetTrigger("Shake");
        }

    }
}