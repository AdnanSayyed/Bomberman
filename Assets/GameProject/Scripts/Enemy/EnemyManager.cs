using System.Collections.Generic;
using UnityEngine;
using LevelSystem;
using Common;

namespace EnemySystem
{
    public class EnemyManager 
    {
        private List<Enemy> enemies;

        private LevelManager levelManager;

        private Enemy enemyPrefab;

        private GameManager gameManager;

        private Transform enemyParent;

        public EnemyManager(Enemy enemyPrefab, GameManager gameManager,Transform enemyParent)
        {
            this.gameManager = gameManager;
            enemies = new List<Enemy>();
            this.enemyPrefab = enemyPrefab;
            this.enemyParent = enemyParent;
            this.gameManager.restartGame += ResetEnemyList;
        }

        ~EnemyManager()
        {
            this.gameManager.restartGame -= ResetEnemyList;
        }

        void ResetEnemyList()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                Object.Destroy(enemies[i].gameObject);
            }

            enemies.Clear();
        }

        public void SetLevelManager(LevelManager levelManager)
        {
            this.levelManager = levelManager;
        }

        public void SpawnEnemy(Vector3 pos)
        {
            GameObject enemy = Object.Instantiate(enemyPrefab.gameObject, pos, Quaternion.identity,enemyParent);
            enemy.GetComponent<Enemy>().SetData(levelManager, this);
            enemies.Add(enemy.GetComponent<Enemy>());
        }

        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
            gameManager.UpdateScore();
            if (enemies.Count <= 0)
            {
                //TODO: fire game won event
                gameManager.SetGameResult(true);
                return;
            }
        }


    }
}