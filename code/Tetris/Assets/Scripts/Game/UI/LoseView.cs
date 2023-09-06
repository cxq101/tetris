using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mini.Core;
using TMPro;

namespace Mini.Game
{
    public class LoseView : View
    {
        [SerializeField]
        private TextMeshProUGUI m_ScoreText;
        [SerializeField]
        private Button m_ReplayButton;
        [SerializeField]
        private Button m_MainMenuButton;

        [SerializeField]
        private AbstractGameEvent m_ReplayEvent;
        [SerializeField]
        private AbstractGameEvent m_MainMenuEvent;

        private void OnEnable()
        {
            m_ReplayButton.onClick.AddListener(OnClickReplay);
            m_MainMenuButton.onClick.AddListener(OnClickMainMenu);
        }

        private void OnDisable()
        {
            m_ReplayButton.onClick.RemoveListener(OnClickReplay);
            m_MainMenuButton.onClick.RemoveListener(OnClickMainMenu);
        }

        private void OnClickReplay()
        {
            Hide();
            m_ReplayEvent.Raise();
        }

        private void OnClickMainMenu()
        {
            Hide();
            m_MainMenuEvent.Raise();
        }

        public void SetScore(int score)
        {
            m_ScoreText.text = $"Score: {score}";
        }
    }

}
