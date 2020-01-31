using UnityEngine;
using LevelSystem;
using PlayerSystem;
using EnemySystem;
using System;
using UISystem;
using UnityEngine.SceneManagement;

namespace Common
{
    public class GameManager : StateMachine
    {

        public static GameManager Instance;

        public event Action<bool> gameStatus;
        public event Action updateScore;
        public event Action restartGame;


        [Range(1, 10),Header("Enem Count between 1 to 10")]
        public int enemyCount;

        [Header("Game grid dimensions")]
        public Vector2 gridSize;

        [Tooltip("UI controller")]
        public UIController uiController;

        [Tooltip("Enemy Prefab")]
        public Enemy enemyPrefab;

        [Tooltip("Bomb Prefab")]
        public BombController bombPrefab;

        [Tooltip("Player Prefab")]
        public Player playerPrefab;

        [Tooltip("FixedBlock Prefab")]
        public FixedBlock fixedBlock;

        [Tooltip("BreakableBlock Prefab")]
        public WeakBlock breakableBlock;

        [Tooltip("Enemy Parent gameobject to spawn enemies in")]
        [SerializeField] private Transform enemyParent;

       [HideInInspector] public LevelManager levelManager;
       [HideInInspector] public PlayerManager playerManager;
       [HideInInspector] public EnemyManager enemyManager;



        void Start()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;

            DontDestroyOnLoad(gameObject);

            uiController.SetGameManager(this);

            if (enemyParent == null)
            {
                GameObject parentEnemy=new GameObject();
                parentEnemy.name = "EnemyParentCreated";
                parentEnemy.gameObject.transform.position = Vector3.zero;
                enemyParent = parentEnemy.gameObject.transform;
            }

            //added menu state
            AddState(new MenuState());
        }

        public override void OnInputTrigger(EventState state, EventType type)
        {
            if (currentState != null)
            {
                currentState.OnInputTrigger(state, type);
            }
        }


        public override void EndCurrentState()
        {
            if (currentState != null)
            {
                currentState.OnStateEnd();
            }
        }


        /// <summary>
        /// generates player,enemies and tiles
        /// </summary>
        public void GenerateComponents()
        {
            playerManager = new PlayerManager(playerPrefab, bombPrefab, this);
            enemyManager = new EnemyManager(enemyPrefab, this, enemyParent);
            levelManager = new LevelManager(fixedBlock, breakableBlock, enemyManager, playerManager);
            enemyManager.SetLevelManager(levelManager);
        }

        /// <summary>
        /// sets game result
        /// </summary>
        /// <param name="gameWon"></param>
        public void SetGameResult(bool gameWon) => gameStatus?.Invoke(gameWon);

        /// <summary>
        /// updates score
        /// </summary>
        public void UpdateScore() => updateScore?.Invoke();

        /// <summary>
        /// restarts game
        /// </summary>
        public void RestartGame() => restartGame?.Invoke();
    }
}