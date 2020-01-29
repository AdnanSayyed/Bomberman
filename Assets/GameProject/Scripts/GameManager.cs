using UnityEngine;
using LevelSystem;
using PlayerSystem;
using EnemySystem;
using System;
using UISystem;
using UnityEngine.SceneManagement;

namespace Common
{
    public class GameManager : Singleton<GameManager>
    {
        

        public event Action<bool> gameStatus;
        public event Action updateScore;
        public event Action restartGame;

        [Range(3, 10)]
        public int enemyCount;
        public Vector2 gridSize;
        public UIController uiController;
        public Enemy enemyPrefab;
        public BombController bombPrefab;
        public Player playerPrefab;
        public FixedBlock fixedBlock;
        public WeakBlock breableBlock;

        LevelManager levelService;
        PlayerManager playerService;
        EnemyManager enemyService;

      

        // Start is called before the first frame update
        void Start()
        {
            uiController.SetServiceManager(this);
            playerService = new PlayerManager(playerPrefab, bombPrefab, this);
            enemyService = new EnemyManager(enemyPrefab, this);
            levelService = new LevelManager(fixedBlock, breableBlock, enemyService, playerService);
            enemyService.SetLevelService(levelService);
            levelService.GenerateLevel();
        }

        public void SetGameStatus(bool gameWon) => gameStatus?.Invoke(gameWon);

        public void UpdateScore() => updateScore?.Invoke();

        public void RestartGame()
        {
            restartGame?.Invoke();
        }

    }
}