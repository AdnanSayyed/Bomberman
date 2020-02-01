using LevelSystem;
using UnityEngine;
using Common;

namespace PlayerSystem
{
    /// <summary>
    /// Playermanager to spawn player,remove player
    /// </summary>
    public class PlayerManager
    {
        #region private fields

        private PlayerController playerController;
        private Player playerPrefab;
        private BombController bombPrefab;
        private LevelManager levelManager;
        private GameManager gameManager;

        #endregion

        #region constructors

        public PlayerManager(Player playerPrefab, BombController bombPrefab, GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.playerPrefab = playerPrefab;
            this.bombPrefab = bombPrefab;
            gameManager.restartGame += RestartGame;
        }

        #endregion

        ~PlayerManager()
        {
            gameManager.restartGame -= RestartGame;
        }

        public void SpawnPlayer(Vector2 spawnPos)
        {
            playerController = new PlayerController(playerPrefab, bombPrefab.gameObject, spawnPos, this,
            levelManager);
        }

        public void PlayerKilled()
        {
            gameManager.SetGameResult(false);
            playerController = null; 
        }

        void RestartGame()
        {
            if (playerController != null)
            {
                playerController.PlayerDestroy();
                playerController = null;
            }
        }

        
        public void SetLevelManager(LevelManager levelManager)
        {
            this.levelManager = levelManager;
        }
    }
}