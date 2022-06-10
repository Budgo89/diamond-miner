using System.Collections.Generic;
using UnityEngine;

namespace MB
{
    public class EnemyScanner : MonoBehaviour
    {
        private List<GameObject> _enemys { get; set; }

        public List<GameObject> GetEnemy()
        {
            DoGetEmeny();
            return _enemys;
        }

        private void DoGetEmeny()
        {
            _enemys = new List<GameObject>();
            EnemyView[] enemys = FindObjectsOfType(typeof(EnemyView)) as EnemyView[];
            foreach (var enemy in enemys)
            {
                _enemys.Add(enemy.gameObject);
            }
        }
    }
}
