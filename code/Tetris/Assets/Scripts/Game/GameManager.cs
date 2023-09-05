using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance => s_Instance;
        private static GameManager s_Instance;

        [SerializeField]
        private Level m_Level;
        private int m_Score;
        private BrickGroup m_CurrentBrickGroup;
        [SerializeField]
        private SpawnBrickItem[] m_SpawnBricks;

        private float m_LoopTime = 1.0f;

        public Vector2Int SpawnPos => new Vector2Int(m_Level.Width / 2, m_Level.Height);
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            s_Instance = this;
        }

        private void Start()
        {
            m_Level.Generate();
            StartGameLoop();
        }

        [ContextMenu(nameof(CheckScoreNow))]
        private void CheckScoreNow()
        {
            if (m_Level.CheckFull(out int[] fullLines))
            {
                m_Level.Bling(fullLines);
                StartCoroutine(BlingAndRemove(fullLines));
            }
        }

        private IEnumerator BlingAndRemove(int[] fullLines)
        {
            yield return new WaitForSeconds(0.6f);
            m_Level.RomoveLinesAndDrop(fullLines);
            // Add Score
            m_Score += fullLines.Length;
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
                    }
                }
            }
            CheckScoreNow();
        }

        private bool TryStopMoveAddToLevel()
        {
            Vector2Int[] posArr = m_CurrentBrickGroup.PosArray();
            if (m_Level.IsValid(posArr, false))
            {
                m_Level.AddBrickGroup(posArr);
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
            if (m_Level.IsValid(m_CurrentBrickGroup.NextTransformPosArray()))
            {
                m_CurrentBrickGroup.TransformNext();
                return true;
            }
            return false;
        }

        private bool TryMoveBy(Vector2Int pos)
        {
            if (m_CurrentBrickGroup == null) return false;
            if (m_Level.IsValid(m_CurrentBrickGroup.PosArray(pos)))
            {
                m_CurrentBrickGroup.MoveBy(pos);
                return true;
            }
            return false;
        }


        private void Spawn()
        {
            GameObject randomPrefab = RandomBrickGroup();
            GameObject gameObject = Instantiate(randomPrefab);
            BrickGroup brickGroup = gameObject.GetComponent<BrickGroup>();
            Vector2Int[] posArr = brickGroup.PosArray(SpawnPos);
            m_CurrentBrickGroup = brickGroup;
            m_CurrentBrickGroup.SetPos(SpawnPos);
        }

        private GameObject RandomBrickGroup()
        {
            int totalWeight = m_SpawnBricks.Sum(e => e.Weight);
            int randomWeight = Random.Range(1, totalWeight + 1);
            var random = new System.Random();
            
            Debug.Log("Random==================" + randomWeight + "   " +  random.Next(1, totalWeight + 1));
            SpawnBrickItem target = null;
            int countWeight = 0;
            foreach (var element in m_SpawnBricks)
            {
                countWeight += element.Weight;
                if (randomWeight <= countWeight)
                {
                    target = element;
                    break;
                }
            }
            return target.BrickPref;
        }

    }
}

