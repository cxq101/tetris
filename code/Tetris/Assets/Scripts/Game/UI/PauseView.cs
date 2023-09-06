using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mini.Core;

namespace Mini.Game
{
    public class PauseView : View
    {
        [SerializeField]
        private Button m_ResumeButton;
        [SerializeField]
        private Button m_ReplayButton;
        [SerializeField]
        private Button m_MainMenuButton;

        [SerializeField]
        private AbstractGameEvent m_ResumeEvent;
        [SerializeField]
        private AbstractGameEvent m_ReplayEvent;
        [SerializeField]
        private AbstractGameEvent m_MainMenuEvent;

        private void OnEnable()
        {
            m_ReplayButton.onClick.AddListener(OnClickReplay);
            m_ResumeButton.onClick.AddListener(OnClickResume);
            m_MainMenuButton.onClick.AddListener(OnClickMainMenu);
        }

        private void OnDisable()
        {
            m_ReplayButton.onClick.RemoveListener(OnClickReplay);
            m_ResumeButton.onClick.RemoveListener(OnClickResume);
            m_MainMenuButton.onClick.RemoveListener(OnClickMainMenu);
        }

        private void OnClickResume()
        {
            Hide();
            m_ResumeEvent.Raise();
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
    }

}
