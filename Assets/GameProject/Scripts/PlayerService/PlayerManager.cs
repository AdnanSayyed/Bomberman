using LevelSystem;
using UnityEngine;
using Common;

namespace PlayerSystem
{
    public class PlayerManager
    {
        private PlayerController playerController;
        private Player playerPrefab;
        private BombController bombPrefab;
        private LevelManager levelManager;
        private GameManager gameManager;

        public PlayerManager(Player playerPrefab, BombController bombPrefab, GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.playerPrefab = playerPrefab;
            this.bombPrefab = bombPrefab;
            gameManager.restartGame += RestartGame;
        }

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
            //TODO: fire game lost event
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

        public GameObject GetPlayer()
        {
            return playerController.GetPlayerView.gameObject;
        }

        public void SetLevelManager(LevelManager levelManager)
        {
            this.levelManager = levelManager;
        }
    }
}