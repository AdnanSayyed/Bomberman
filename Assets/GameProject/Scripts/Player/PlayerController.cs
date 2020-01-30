﻿using UnityEngine;
using LevelSystem;

namespace PlayerSystem
{
    public class PlayerController
    {
        private Player playerView;
        private PlayerManager playerManager;
        private GameObject bombPrefab;
        private LevelManager levelManager;

        public Player GetPlayerView { get { return playerView; } }

        private GameObject lastBomb = null;

        public PlayerController(Player playerPref, GameObject bombPrefab
                                , Vector2 pos, PlayerManager playerManager
            , LevelManager levelManager)
        {
            this.levelManager = levelManager;
            this.playerManager = playerManager;
            this.bombPrefab = bombPrefab;
            GameObject player = Object.Instantiate(playerPref.gameObject, pos, Quaternion.identity);
            playerView = player.GetComponent<Player>();
            playerView.SetController(this);
        }

        public void SpawnBomb()
        {
            Vector2 spawnPos = playerView.transform.position;
            spawnPos.x = Mathf.Round(spawnPos.x);
            spawnPos.y = Mathf.Round(spawnPos.y);
            if (lastBomb == null)
            {
                lastBomb = Object.Instantiate(bombPrefab, spawnPos, Quaternion.identity);
                lastBomb.GetComponent<BombController>().SetLevelManager(levelManager);
            }
        }

        public void PlayerKilled()
        {
            Object.Destroy(playerView.gameObject);
            playerManager.PlayerKilled();
        }

        public void PlayerDestroy()
        {
            Object.Destroy(playerView.gameObject);
        }
    }
}