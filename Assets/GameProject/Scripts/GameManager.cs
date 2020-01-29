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

        LevelManager levelManager;
        PlayerManager playerManager;
        EnemyManager enemyManager;

        [SerializeField] private Transform enemyParent;

        // Start is called before the first frame update
        void Start()
        {
            uiController.SetGameManager(this);
            playerManager = new PlayerManager(playerPrefab, bombPrefab, this);
            enemyManager = new EnemyManager(enemyPrefab, this,enemyParent);
            levelManager = new LevelManager(fixedBlock, breableBlock, enemyManager, playerManager);
            enemyManager.SetLevelManager(levelManager);
            levelManager.GenerateLevel();
        }

        public void SetGameResult(bool gameWon) => gameStatus?.Invoke(gameWon);

        public void UpdateScore() => updateScore?.Invoke();

        public void RestartGame() => restartGame?.Invoke();
    }
}