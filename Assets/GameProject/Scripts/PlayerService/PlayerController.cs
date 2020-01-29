using UnityEngine;
using LevelSystem;

namespace PlayerSystem
{
    public class PlayerController
    {
        private Player playerView;
        private PlayerManager playerService;
        private GameObject bombPrefab;
        private LevelManager levelService;

        public Player GetPlayerView { get { return playerView; } }

        private GameObject lastBomb = null;

        public PlayerController(Player playerPref, GameObject bombPrefab
                                , Vector2 pos, PlayerManager playerService
            , LevelManager levelService)
        {
            this.levelService = levelService;
            this.playerService = playerService;
            this.bombPrefab = bombPrefab;
            GameObject player = Object.Instantiate(playerPref.gameObject, pos, Quaternion.identity);
            playerView = player.GetComponent<Player>();
            playerView.SetController(this);
        }

        public void SpawnBomb()
        {
            Vector2 spawnPOs = playerView.transform.position;
            spawnPOs.x = Mathf.Round(spawnPOs.x);
            spawnPOs.y = Mathf.Round(spawnPOs.y);
            if (lastBomb == null)
            {
                lastBomb = Object.Instantiate(bombPrefab, spawnPOs, Quaternion.identity);
                lastBomb.GetComponent<BombController>().SetLevelService(levelService);
            }
        }

        public void PlayerKilled()
        {
            Object.Destroy(playerView.gameObject);
            playerService.PlayerKilled();
        }

        public void PlayerDestroy()
        {
            Object.Destroy(playerView.gameObject);
        }
    }
}