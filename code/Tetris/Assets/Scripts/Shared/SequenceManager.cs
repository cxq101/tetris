using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mini.Core;
using Mini.Game;

namespace Mini.Shared
{
    public class SequenceManager : AbstractSingleton<SequenceManager>
    {
        [SerializeField]
        private GameObject[] m_PreloadedAssets;
        [SerializeField]
        private LevelDataDef m_LevelDataDef;

        [Header("State")]
        private State m_MainViewState;
        [Header("Event")]
        [SerializeField]
        private AbstractGameEvent m_ContinueEvent;
        [SerializeField]
        private AbstractGameEvent m_PauseEvent;        
        [SerializeField]
        private AbstractGameEvent m_LoseEvent;        
        [SerializeField]
        private AbstractGameEvent m_ReplayEvent;   
        [SerializeField]
        private AbstractGameEvent m_QuitEvent;

        private StateMachine m_StateMachine;
        private SceneController m_SceneController;

        public void Initialize()
        {
            m_StateMachine = new StateMachine();
            m_SceneController = new SceneController(SceneManager.GetActiveScene());

            InstantiatePreloadAssets();
            CreateMenuNavigationSequence();
        }

        private void InstantiatePreloadAssets()
        {
            foreach (var asset in m_PreloadedAssets)
            {
                Instantiate(asset);
            }
        }

        void CreateMenuNavigationSequence()
        {
            m_MainViewState = new State(OnMainMenuDisplayed);
            var m_GamePlayState = new State(OnGamePlay);
            var m_LoadLevelState = new State(OnLevelLoad);

            m_MainViewState.AddLink(new EventLink(m_ContinueEvent, m_LoadLevelState));
            m_MainViewState.EnableLinks();

            m_LoadLevelState.AddLink(new Link(m_GamePlayState));

            PauseState gamePauseState = new PauseState(OnGamePause);
            PauseState losePauseState = new PauseState(OnGameLose);
            m_GamePlayState.AddLink(new EventLink(m_PauseEvent, gamePauseState));
            m_GamePlayState.AddLink(new EventLink(m_LoseEvent, losePauseState));

            var reloadLevelState = new State(OnReLoadLevel);
            var exitLevelState = new State(OnReLoadLevel);

            gamePauseState.AddLink(new EventLink(m_ContinueEvent, m_GamePlayState));
            gamePauseState.AddLink(new EventLink(m_ReplayEvent, reloadLevelState));
            gamePauseState.AddLink(new EventLink(m_QuitEvent, exitLevelState));

            losePauseState.AddLink(new EventLink(m_ReplayEvent, reloadLevelState));
            losePauseState.AddLink(new EventLink(m_QuitEvent, exitLevelState));

            reloadLevelState.AddLink(new Link(m_LoadLevelState));
            exitLevelState.AddLink(new Link(m_MainViewState));
            m_StateMachine.Run(m_MainViewState);

        }
        void OnMainMenuDisplayed()
        {
            UIManager.Instance.Show<MainView>();
            //AudioManager.Instance.PlayMusic(SoundID.MenuMusic);
            PlayController.Instance.EnableJump = false;
            CameraManager.Instance.SetFollow(PlayController.Instance.transform);
        }
        void OnLevelLoad()
        {
            UIManager.Instance.GoBack();
            LevelLoader.Instance.Load(m_LevelDataDef);
        }
        void OnGamePlay()
        {
            UIManager.Instance.Show<Hud>();
            PlayController.Instance.EnableJump = true;
        }        
        void OnGamePause()
        {
            UIManager.Instance.Show<PauseView>();
        }
        void OnGameLose()
        {
            UIManager.Instance.Show<LoseView>();
        }

        void OnReLoadLevel()
        {
            LevelLoader.Instance.Unload();
            PlayController.Instance.OnLevelReset();
            GameManager.OnLevelReset();
        }
    }
}
