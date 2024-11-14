using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team11
{
    public class T11_AttackBall : MonoBehaviour
    {
        public float life = 3;

        private void Awake()
        {
            Destroy(gameObject, life);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Collider"))
            {
                Destroy(gameObject);
            }
        }
    }
}
