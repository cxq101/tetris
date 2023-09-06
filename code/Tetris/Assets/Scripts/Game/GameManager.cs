using System.Collections;
using UnityEngine;
using Mini.Core;

namespace Mini.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance => s_Instance;
        private static GameManager s_Instance;
        [SerializeField]
        private AbstractGameEvent m_LoseEvent;

        private int m_Score;
        private BrickGroup m_CurrentBrickGroup;
        private Spawner m_Spawner;

        private float m_LoopTime = 1.0f;

        public Vector2Int SpawnPos => new Vector2Int(Level.Instance.Width / 2, Level.Instance.Height);
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            s_Instance = this;
            m_Spawner = GetComponent<Spawner>();
        }

        public void StartGame()
        {
            Level.Instance.Generate();
            StartGameLoop();
        }

        [ContextMenu(nameof(CheckScoreNow))]
        private void CheckScoreNow()
        {
            if (Level.Instance.CheckFull(out int[] fullLines))
            {
                Level.Instance.Bling(fullLines);
                StartCoroutine(BlingAndRemove(fullLines));
            }
        }

        private IEnumerator BlingAndRemove(int[] fullLines)
        {
            yield return new WaitForSeconds(0.6f);
            Level.Instance.RomoveLinesAndDrop(fullLines);
            // Add Score
            m_Score += fullLines.Length;
            UIManager.Instance.GetView<Hud>().SetScore(m_Score);
            Debug.Log($"Score add: {fullLines.Length}  total: {m_Score}");
        }

        private void StartGameLoop()
        {
            InvokeRepeating(nameof(Step), 0.0f, m_LoopTime);
        }

        private void StopGameLoop()
        {
            CancelInvoke(nameof(Step));
        }

        private void Step()
        {
            if (m_CurrentBrickGroup == null)
            {
                Spawn();
            }
            else
            {
                if (!TryMoveBy(Vector2Int.down))
                {
                    if (!TryStopMoveAddToLevel())
                    {
                        Debug.Log("====Game Over====");
                        m_LoseEvent.Raise();
                    }
                }
            }
            CheckScoreNow();
        }

        private bool TryStopMoveAddToLevel()
        {
            Vector2Int[] posArr = m_CurrentBrickGroup.PosArray();
            if (Level.Instance.IsValid(posArr, false))
            {
                Level.Instance.AddBrickGroup(posArr);
                Destroy(m_CurrentBrickGroup.gameObject);
                m_CurrentBrickGroup = null;
                return true;
            }
            return false;
        }

        public void OnInputMove(Vector2Int pos)
        {
            TryMoveBy(pos);
        }

        public void OnInputTransform()
        {
            TryTransForm();
        }

        private bool TryTransForm()
        {
            if (m_CurrentBrickGroup == null) return false;
            if (Level.Instance.IsValid(m_CurrentBrickGroup.NextTransformPosArray()))
            {
                m_CurrentBrickGroup.TransformNext();
                return true;
            }
            return false;
        }

        private bool TryMoveBy(Vector2Int pos)
        {
            if (m_CurrentBrickGroup == null) return false;
            if (Level.Instance.IsValid(m_CurrentBrickGroup.PosArray(pos)))
            {
                m_CurrentBrickGroup.MoveBy(pos);
                return true;
            }
            return false;
        }

        private void Spawn()
        {
            GameObject gameObject = m_Spawner.Generate();
            gameObject.transform.parent = transform;
            BrickGroup brickGroup = gameObject.GetComponent<BrickGroup>();
            m_CurrentBrickGroup = brickGroup;
            m_CurrentBrickGroup.SetPos(SpawnPos);
        }
    }
}

