using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mini.Core;

namespace Mini.Game
{
    public class MainView : View
    {
        [SerializeField]
        private Button m_PlayButton;
        [SerializeField]
        private AbstractGameEvent m_PlayEvent;

        private void OnEnable()
        {
            m_PlayButton.onClick.AddListener(OnClickPlay);
        }

        private void OnDisable()
        {
            m_PlayButton.onClick.RemoveListener(OnClickPlay);
        }

        private void OnClickPlay()
        {
            m_PlayEvent.Raise();
            Hide();
        }
    }
}
