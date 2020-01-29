using System.Collections.Generic;
using UnityEngine;
using LevelSystem;
using Common;

namespace EnemySystem
{
    public class EnemyManager 
    {
        private List<Enemy> enemyControllers;

        private LevelManager levelService;
        private Enemy enemyPrefab;

        private GameManager serviceManager;

        public EnemyManager(Enemy enemyPrefab, GameManager serviceManager)
        {
            this.serviceManager = serviceManager;
            enemyControllers = new List<Enemy>();
            this.enemyPrefab = enemyPrefab;
            this.serviceManager.restartGame += ResetEnemyList;
        }

        ~EnemyManager()
        {
            this.serviceManager.restartGame -= ResetEnemyList;
        }

        void ResetEnemyList()
        {
            for (int i = 0; i < enemyControllers.Count; i++)
            {
                Object.Destroy(enemyControllers[i].gameObject);
            }

            enemyControllers.Clear();
        }

        public void SetLevelService(LevelManager levelService)
        {
            this.levelService = levelService;
        }

        public void SpawnEnemy(Vector3 pos)
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab.gameObject, pos, Quaternion.identity);
            enemy.GetComponent<Enemy>().SetServices(levelService, this);
            enemyControllers.Add(enemy.GetComponent<Enemy>());
        }

        public void RemoveEnemy(Enemy enemyController)
        {
            enemyControllers.Remove(enemyController);
            serviceManager.UpdateScore();
            if (enemyControllers.Count <= 0)
            {
                //TODO: fire game won event
                serviceManager.SetGameStatus(true);
                return;
            }
        }


    }
}