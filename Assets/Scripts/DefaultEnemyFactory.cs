using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class DefaultEnemyFactory : MonoBehaviour
    {
        [SerializeField] private DefaultEnemy defaultEnemyPrefab;
        
        private List<DefaultEnemy> enemyList;

        public static DefaultEnemyFactory Instance;

        private void Awake()
        {
            Instance = this;
            Create(Vector3.zero);
        }

        public DefaultEnemy Create(Vector3 position) {
            Transform enemyTransform = Instantiate(defaultEnemyPrefab, position, Quaternion.identity).transform;

            var enemy = enemyTransform.GetComponent<DefaultEnemy>();

            if (enemyList == null) enemyList = new List<DefaultEnemy>();
            enemyList.Add(enemy);

            return enemy;
        }
        
        public DefaultEnemy GetClosestEnemy(Vector3 position, float range) {
            if (enemyList == null) return null; // No enemies!

            DefaultEnemy closestEnemy = null;

            for (int i = 0; i < enemyList.Count; i++) {
                DefaultEnemy testEnemy = enemyList[i];
                if (Vector3.Distance(position, testEnemy.GetPosition()) > range) {
                    // Enemy too far, skip
                    continue;
                }

                if (closestEnemy == null) {
                    // No closest enemy
                    closestEnemy = testEnemy;
                } else {
                    // Already has a closest enemy, get which is closer
                    if (Vector3.Distance(position, testEnemy.GetPosition()) < Vector3.Distance(position, closestEnemy.GetPosition())) {
                        // Test Enemy is closer than Closest Enemy
                        closestEnemy = testEnemy;
                    }
                }
            }

            return closestEnemy;
        }
    }
}