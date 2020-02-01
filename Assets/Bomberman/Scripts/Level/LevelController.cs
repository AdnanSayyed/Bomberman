using System.Collections.Generic;
using UnityEngine;
using PlayerSystem;
using EnemySystem;
using Common;
using System.Linq;

namespace LevelSystem
{
    /// <summary>
    /// contains level generation code
    /// </summary>
    public class LevelController
    {
        public GameObject[,] gridArray;

        #region private fields

        private List<Vector2> emptyGrid;
        private int extraEdgeGrids = 2; 
        private int gridWidth = 15, gridHeight = 10, enemyCount;

        private FixedBlock fixedBlockPref;
        private WeakBlock weakBlockPref;
        private GameObject levelHolder;
        private EnemyManager enemyManager;

        PlayerManager playerManager;
        LevelManager levelManager;

        private Vector3 levelHolderPos = Vector3.zero;

        #endregion

        #region constructors

        public LevelController(FixedBlock fixedBlockPrefab, WeakBlock weakBlockPrefab,
                                EnemyManager enemyManager, PlayerManager playerManager,
                                LevelManager levelManager)
        {
            this.playerManager = playerManager;
            this.enemyManager = enemyManager;
            this.levelManager = levelManager;
            this.fixedBlockPref = fixedBlockPrefab;
            this.weakBlockPref = weakBlockPrefab;
            gridWidth = (int)GameManager.Instance.gridSize.x;
            gridHeight = (int)GameManager.Instance.gridSize.y;
            enemyCount = GameManager.Instance.enemyCount;
            GameManager.Instance.restartGame += RestartGame;
        }

        #endregion

        ~LevelController()
        {
            GameManager.Instance.restartGame -= RestartGame;
        }

        /// <summary>
        /// Remove all current grids
        /// ReGenerates level on restart
        /// </summary>
        void RestartGame()
        {
            for (int i = 0; i < gridWidth + extraEdgeGrids; i++)
            {
                for (int j = 0; j < gridHeight + extraEdgeGrids; j++)
                {
                    if (gridArray[i, j] != null)
                    {
                        GameObject obj = gridArray[i, j];
                        Object.Destroy(obj);
                        gridArray[i, j] = null;
                    }
                }
            }

            gridArray = null;

            GenerateLevel();
        }

        /// <summary>
        /// generates the level components
        /// </summary>
        public void GenerateLevel()
        {
            emptyGrid = new List<Vector2>();
            gridArray = new GameObject[gridWidth + extraEdgeGrids, gridHeight + extraEdgeGrids];
            if (levelHolder == null)
                levelHolder = new GameObject();
            levelHolder.transform.position = levelHolderPos;
            levelHolder.name = "LevelHolder";

            GenerateGrid();
            GenerateEdgeBoarder();
            SpawnFixedBlocks();
            SpawnPlayer();
            SpawnWeakBlocks();
            SpawnEnemies();

            emptyGrid.Clear();
        }

        void GenerateGrid()
        {
            for (int i = 0; i < gridWidth + extraEdgeGrids; i++)
            {
                for (int j = 0; j < gridHeight + extraEdgeGrids; j++)
                {
                    Vector2 vector = new Vector2(i, j);
                    emptyGrid.Add(vector);
                    gridArray[i, j] = null;
                }
            }
        }

        /// <summary>
        /// Generaes blocks on edge(outer)
        /// </summary>
        void GenerateEdgeBoarder()
        {

            for (int i = 0; i < gridHeight + extraEdgeGrids; i++)
            {
                Edge(new Vector2(0, i));
                Edge(new Vector2(gridWidth + extraEdgeGrids - 1, i));
            }

            for (int i = 1; i < gridWidth + extraEdgeGrids - 1; i++)
            {
                Edge(new Vector2(i, 0));
                Edge(new Vector2(i, gridHeight + extraEdgeGrids - 1));
            }
        }

        /// <summary>
        /// Spawns fixedblcoks on edge
        /// </summary>
        /// <param name="pos">spawn pos</param>
        void Edge(Vector2 pos)
        {
            GameObject fixedBlock = Object.Instantiate(fixedBlockPref.gameObject, pos, Quaternion.identity);
            fixedBlock.transform.SetParent(levelHolder.transform);
            fixedBlock.name = "Edge[" + pos.x + "," + pos.y + "]";
            emptyGrid.Remove(pos);
            gridArray[(int)pos.x, (int)pos.y] = fixedBlock;
        }

        /// <summary>
        /// Spawns fixed blocks inside edge blocks (inner)
        /// </summary>
        void SpawnFixedBlocks()
        {
            for (int i = extraEdgeGrids; i < gridWidth; i += 2)
            {
                for (int j = extraEdgeGrids; j < gridHeight; j += 2)
                {
                    Vector2 vector = new Vector2(i, j);
                    GameObject fixedBlock = Object.Instantiate(fixedBlockPref.gameObject, vector, Quaternion.identity);
                    fixedBlock.transform.SetParent(levelHolder.transform);
                    fixedBlock.name = "Fixed[" + vector.x + "," + vector.y + "]";
                    emptyGrid.Remove(vector);
                    gridArray[(int)vector.x, (int)vector.y] = fixedBlock;
                }
            }
        }

        void SpawnPlayer()
        {
            Vector2 spawnPos = new Vector2(1, gridHeight);
            playerManager.SpawnPlayer(spawnPos);
            emptyGrid.Remove(spawnPos);

            for (int i = 1; i < 4; i++)
            {
                for (int j = gridHeight; j > gridHeight - 3; j--)
                {
                    Vector2 tempVector = new Vector2(i, j);
                    emptyGrid.Remove(tempVector);
                }
            }
        }

        /// <summary>
        /// Spawns weak blocks 
        /// </summary>
        void SpawnWeakBlocks()
        {
            int val = Random.Range(Mathf.CeilToInt(emptyGrid.Count / 6), Mathf.CeilToInt(emptyGrid.Count / 3));
            for (int i = 0; i < val; i++)
            {
                int k = Random.Range(0, emptyGrid.Count);
                Vector2 vector = emptyGrid[k];
                GameObject weakBlocks = Object.Instantiate(weakBlockPref.gameObject, vector, Quaternion.identity);
                weakBlocks.transform.SetParent(levelHolder.transform);
                weakBlocks.name = "Weak[" + vector.x + "," + vector.y + "]";
                weakBlocks.GetComponent<WeakBlock>().SetLevelManager(levelManager);
                emptyGrid.RemoveAt(k);
                gridArray[(int)vector.x, (int)vector.y] = weakBlocks;
            }
        }

        void SpawnEnemies()
        {
            for (int i = 0; i < enemyCount; i++)
            {
                int k = Random.Range(0, emptyGrid.Count);
                Vector2 vector = emptyGrid[k];
                enemyManager.SpawnEnemy(vector);
                emptyGrid.RemoveAt(k);
            }
        }
    }
}