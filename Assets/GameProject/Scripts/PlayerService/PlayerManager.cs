﻿using LevelSystem;
using UnityEngine;
using Common;

namespace PlayerSystem
{
    public class PlayerManager
    {
        private PlayerController playerController;
        private Player playerPrefab;
        private BombController bombPrefab;
        private LevelManager levelService;
        private GameManager serviceManager;

        public PlayerManager(Player playerPrefab, BombController bombPrefab, GameManager serviceManager)
        {
            this.serviceManager = serviceManager;
            this.playerPrefab = playerPrefab;
            this.bombPrefab = bombPrefab;
            serviceManager.restartGame += RestartGame;
        }

        ~PlayerManager()
        {
            serviceManager.restartGame -= RestartGame;
        }

        public void SpawnPlayer(Vector2 spawnPos)
        {
            playerController = new PlayerController(playerPrefab, bombPrefab.gameObject, spawnPos, this,
            levelService);
        }

        public void PlayerKilled()
        {
            //TODO: fire game lost event
            serviceManager.SetGameStatus(false);
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

        public void SetLevelService(LevelManager levelService)
        {
            this.levelService = levelService;
        }
    }
}