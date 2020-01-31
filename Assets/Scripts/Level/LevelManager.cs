using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using PlayerSystem;
using EnemySystem;

namespace LevelSystem
{
  /// <summary>
  /// Responsible for level generation
  /// </summary>
    public class LevelManager
    {
        private LevelController levelController;
        private GameManager gameManager;
        private PlayerManager playerManager;

        public LevelManager(FixedBlock fixedBlockPrefab, WeakBlock breakableBlockPrefab,
                            EnemyManager enemyManager, PlayerManager playerManager)
        {
            this.playerManager = playerManager;
            this.playerManager.SetLevelManager(this);
            levelController = new LevelController(fixedBlockPrefab, breakableBlockPrefab,
                                                  enemyManager, playerManager, this);
        }

        public void EmptyGrid(Vector2 position)
        {
            levelController.gridArray[(int)position.x, (int)position.y] = null;
        }

        public void FillGrid(Vector2 position, GameObject gameObject)
        {
            if (levelController.gridArray[(int)position.x, (int)position.y])
                levelController.gridArray[(int)position.x, (int)position.y] = gameObject;
        }

        /// <summary>
        /// generates level
        /// </summary>
        public void GenerateLevel()
        {
            levelController.GenerateLevel();
        }

        public GameObject GetObjAtGrid(Vector2 position)
        {
            GameObject obj = null;

            if(levelController.gridArray[(int)position.x, (int)position.y])
                obj = levelController.gridArray[(int)position.x, (int)position.y];


            return obj;
        }
    }
}