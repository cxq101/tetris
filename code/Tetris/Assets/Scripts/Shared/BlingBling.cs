using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Shared
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BlingBling : MonoBehaviour
    {
        [SerializeField]
        private float m_FadaTime;

        private SpriteRenderer m_Renderer;
        private Color m_FromColor;  
        private Color m_ToColor;  
        private float m_CountTime;
        // Start is called before the first frame update
        void Start()
        {
            m_Renderer = GetComponent<SpriteRenderer>();    
        }

        // Update is called once per frame
        void Update()
        {
            //if (m_CountTime >= m_FadaTime)
            //{
            //                }
            //if ()
            //{
            //    m_Renderer.color = Color.Lerp(m_FromColor, m_ToColor, m_CountTime / m_FadaTime);
            //}
            //m_CountTime += Time.deltaTime;

        }

        public void Play()
        {
            m_FromColor = m_Renderer.color;
            m_ToColor = new Color(m_FromColor.r, m_FromColor.g, m_FromColor.b, 1);
        }

        private IEnumerator Fade(Color from, Color to, float time)
        {
            float countTime = 0;
            while (countTime < time)
            {
                m_Renderer.color = Color.Lerp(m_FromColor, m_ToColor, countTime / time);
                countTime += Time.deltaTime;
                yield return null;
            }
        }
     
        [ContextMenu(nameof(Demo))]
        private void Demo()
        {
            m_FromColor = m_Renderer.color;
            m_ToColor = new Color(m_FromColor.r, m_FromColor.g, m_FromColor.b, 0);
            StartCoroutine(Fade(m_FromColor, m_ToColor, m_FadaTime));
        }

        private IEnumerator Wait(float time)
        {
            float countTime = 0f;
            Debug.Log("Step0============");
            while (countTime < time)
            {
                countTime += Time.deltaTime;
                Debug.Log("Step1============"+ countTime + "xx:   " + time);
                yield return null;
            }
            Debug.Log("Step2============"+ countTime + "xx:   " + time);
        }


        public void Stop()
        {

        }
    }
}
