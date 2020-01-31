using UnityEngine;
using LevelSystem;

namespace PlayerSystem
{
    /// <summary>
    /// contains player spawn,bomb spawn code
    /// </summary>
    public class PlayerController
    {
        private Player player;
        private PlayerManager playerManager;
        private GameObject bombPrefab;
        private LevelManager levelManager;

        public Player GetPlayer { get { return player; } }

        private GameObject lastBomb = null;

        public PlayerController(Player playerPref, GameObject bombPrefab
                                , Vector2 pos, PlayerManager playerManager
            , LevelManager levelManager)
        {
            this.levelManager = levelManager;
            this.playerManager = playerManager;
            this.bombPrefab = bombPrefab;
            GameObject player = Object.Instantiate(playerPref.gameObject, pos, Quaternion.identity);
            this.player = player.GetComponent<Player>();
            this.player.SetController(this);
        }

        /// <summary>
        /// spawns a bomb
        /// </summary>
        public void SpawnBomb()
        {
            Vector2 spawnPos = player.transform.position;
            spawnPos.x = Mathf.Round(spawnPos.x);
            spawnPos.y = Mathf.Round(spawnPos.y);
            if (lastBomb == null)
            {
                lastBomb = Object.Instantiate(bombPrefab, spawnPos, Quaternion.identity);
                lastBomb.GetComponent<BombController>().SetLevelManager(levelManager);
            }
        }

        /// <summary>
        /// Destroys player
        /// </summary>
        public void PlayerKilled()
        {
            Object.Destroy(player.gameObject);
            playerManager.PlayerKilled();
        }

        /// <summary>
        /// Destroy Player (for restarting game)
        /// </summary>
        public void PlayerDestroy()
        {
            Object.Destroy(player.gameObject);
        }
    }
}