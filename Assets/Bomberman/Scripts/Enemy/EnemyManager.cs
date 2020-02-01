using System.Collections.Generic;
using UnityEngine;
using LevelSystem;
using Common;

namespace EnemySystem
{
    /// <summary>
    /// Responsible for spawning,destruction of enemy
    /// </summary>
    public class EnemyManager 
    {
        #region private fields

        private List<Enemy> enemies;
        private LevelManager levelManager;
        private Enemy enemyPrefab;
        private GameManager gameManager;
        private Transform enemyParent;

        #endregion

        #region constructors

        public EnemyManager(Enemy enemyPrefab, GameManager gameManager,Transform enemyParent)
        {
            this.gameManager = gameManager;
            enemies = new List<Enemy>();
            this.enemyPrefab = enemyPrefab;
            this.enemyParent = enemyParent;
            this.gameManager.restartGame += ResetEnemyList;
        }

        #endregion


        ~EnemyManager()
        {
            this.gameManager.restartGame -= ResetEnemyList;
        }

        /// <summary>
        /// Destroy all enemies when restarting the game
        /// </summary>
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

        /// <summary>
        /// spawns an enemy
        /// </summary>
        /// <param name="pos"> position to spawn</param>
        public void SpawnEnemy(Vector3 pos)
        {
            GameObject enemy = Object.Instantiate(enemyPrefab.gameObject, pos, Quaternion.identity,enemyParent);
            enemy.GetComponent<Enemy>().SetData(levelManager, this);
            enemies.Add(enemy.GetComponent<Enemy>());
        }

        /// <summary>
        /// Remove an enemy
        /// </summary>
        /// <param name="enemy">Enemy object </param>
        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
            gameManager.UpdateScore();
            if (enemies.Count <= 0)
            {
                //won the game
                gameManager.SetGameResult(true);
                return;
            }
        }

    }
}