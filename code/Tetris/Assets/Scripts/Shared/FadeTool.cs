using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Shared
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FadeTool : MonoBehaviour
    {
        [SerializeField]
        private float m_FadeTime = 1.0f;
        [SerializeField]
        private float m_FromAlpha = 0.0f;
        [SerializeField]
        private float m_ToAlpha = 1.0f;
        [SerializeField]
        private int m_RepeatTimes = -1;

        private Color m_ToColor;
        private Color m_FromColor;
        private SpriteRenderer m_Renderer;
        private Coroutine m_FadeCoroutine;

        void Start()
        {
            m_Renderer = GetComponent<SpriteRenderer>();
            Color color = m_Renderer.color;
            m_FromColor = new Color(color.r, color.g, color.b, m_FromAlpha);
            m_ToColor = new Color(color.r, color.g, color.b, m_ToAlpha);
        }
        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.A))
            {
                FadeIn();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                FadeOut();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Bling();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Stop();
            }
#endif
        }
        private IEnumerator Fade(Color fromColor, Color toColor, float time, bool pingpong = false)
        {
            float countTime = 0;
            while (countTime < time)
            {
                m_Renderer.color = Color.Lerp(fromColor, toColor, countTime / time);
                countTime += Time.deltaTime;
                yield return null;
            }
            m_Renderer.color = toColor;
            if (pingpong)
            {
                yield return Fade(toColor, fromColor, time);
            }
        }

        private IEnumerator Bling(Color fromColor, Color toColor, float fadeTime, int repeatTimes = -1)
        {
            while (repeatTimes == -1 || repeatTimes > 0)
            {
                yield return Fade(fromColor, toColor, fadeTime, true);
                if (repeatTimes > 0)
                    repeatTimes--;
            }
        }

        public void FadeIn()
        {
            m_FadeCoroutine = StartCoroutine(Fade(m_FromColor, m_ToColor, m_FadeTime));
        }

        public void FadeOut()
        {
            m_FadeCoroutine = StartCoroutine(Fade(m_ToColor, m_FromColor, m_FadeTime));
        }

        public void Bling()
        {
            m_FadeCoroutine = StartCoroutine(Bling(m_FromColor, m_ToColor, m_FadeTime, m_RepeatTimes));
        }

        public void Stop()
        {
            StopCoroutine(m_FadeCoroutine);
        }

    }
}
