using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Shared
{
    public class FalllingPlatform : MonoBehaviour
    {
        [SerializeField]
        private float m_FallDelay;
        [SerializeField]
        private float m_DestroyDelay;

        private bool m_IsFalling;
        private Rigidbody2D m_Rigidbody2D;
        private void Start()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();   
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (m_IsFalling) return;
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(StartFall());
            }   
        }

        private IEnumerator StartFall()
        {
            m_IsFalling = true;
            yield return new WaitForSeconds(m_FallDelay);
            m_Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject, m_DestroyDelay);
        }
    }
}
