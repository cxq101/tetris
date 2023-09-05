using System.Linq;
using UnityEngine;

namespace Mini.Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private SpawnBricks m_GeneratePool;
        public GameObject Generate()
        {
            GameObject randomPrefab = RandomBrickGroup();
            GameObject gameObject = Instantiate(randomPrefab);
            return gameObject;
        }

        private GameObject RandomBrickGroup()
        {
            SpawnBrickItem[] items = m_GeneratePool.Bricks;
            int totalWeight = items.Sum(e => e.Weight);
            int randomWeight = Random.Range(1, totalWeight + 1);
            var random = new System.Random();

            SpawnBrickItem? target = null;
            int countWeight = 0;
            foreach (var element in items)
            {
                countWeight += element.Weight;
                if (randomWeight <= countWeight)
                {
                    target = element;
                    break;
                }
            }
            return target?.Prefab;
        }
    }
}
