using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Mini.Core;

namespace Mini.Game
{
    public class Hud : View
    {
        [SerializeField]
        private Button m_PauseButton;
        [SerializeField]
        private TextMeshProUGUI m_ScoreText;
        [SerializeField]
        private AbstractGameEvent m_PauseEvent;

        private void Start()
        {
            SetScore(0);
        }

        private void OnEnable()
        {
            m_PauseButton.onClick.AddListener(OnClickPause);
        }

        private void OnDisable()
        {
            m_PauseButton.onClick.RemoveListener(OnClickPause);
        }

        private void OnClickPause()
        {
            m_PauseEvent.Raise();
        }

        public void SetScore(int score)
        {
            m_ScoreText.text = $"Score: {score}";
        }
    }
}
