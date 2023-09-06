using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Game.Shared.UICompoents
{
    /// <summary>
    /// A base class for the buttons of the hyper casual game template that
    /// contains basic functionalities like button sound effect
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class SoundButton : MonoBehaviour
    {
        [SerializeField]
        Button m_Button;
        [SerializeField]
        //SoundID m_ButtonSound = SoundID.ButtonSound;

        Action m_Action;

        protected virtual void OnEnable()
        {
            m_Button.onClick.AddListener(OnClick);
        }

        protected virtual void OnDisable()
        {
            m_Button.onClick.RemoveListener(OnClick);
        }

        protected virtual void OnClick()
        {
            m_Action?.Invoke();
            PlayButtonSound();
        }

        protected void PlayButtonSound()
        {
            //AudioManager.Instance.PlayEffect(m_ButtonSound);
        }

        public void AddListener(Action handler)
        {
            m_Action += handler;
        }

        public void RemoveListener(Action handler)
        {
            m_Action -= handler;
        }

    }
}
