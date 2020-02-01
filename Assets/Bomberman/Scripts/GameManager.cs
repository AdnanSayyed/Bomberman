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

        #region Visible in Inspector fields

        [Header("Enemy")]

        [Range(1, 10)]
        public int enemyCount;

        [Tooltip("Enemy Prefab")]
        public Enemy enemyPrefab;

        [Tooltip("Enemy Parent gameobject to spawn enemies in")]
        [SerializeField] private Transform enemyParent;


        [Header("Level")]

        [Tooltip("Grid dimensions")]
        public Vector2 gridSize;

        [Tooltip("FixedBlock Prefab")]
        public FixedBlock fixedBlock;

        [Tooltip("WeakBlock Prefab")]
        public WeakBlock weakBlock;

        [Header("UI")]

        [Tooltip("UI controller")]
        public UIController uiController;

        [Header("Bomb")]

        [Tooltip("Bomb Prefab")]
        public BombController bombPrefab;

        [Header("Player")]

        [Tooltip("Player Prefab")]
        public Player playerPrefab;



       [HideInInspector] public LevelManager levelManager;
       [HideInInspector] public PlayerManager playerManager;
       [HideInInspector] public EnemyManager enemyManager;

        #endregion

        void Start()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
                   
            uiController.SetGameManager(this);

            if (enemyParent == null)
            {
                GameObject parentEnemy=new GameObject();
                parentEnemy.name = "Enemies";
                parentEnemy.gameObject.transform.position = Vector3.zero;
                enemyParent = parentEnemy.gameObject.transform;
            }

            DontDestroyOnLoad(gameObject);

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
        public void CreateComponents()
        {
            playerManager = new PlayerManager(playerPrefab, bombPrefab, this);
            enemyManager = new EnemyManager(enemyPrefab, this, enemyParent);
            levelManager = new LevelManager(fixedBlock, weakBlock, enemyManager, playerManager);
            enemyManager.SetLevelManager(levelManager);
        }

        /// <summary>
        /// sets game result
        /// calling all registered methods to event
        /// </summary>
        /// <param name="gameWon"></param>
        public void SetGameResult(bool gameWon) => gameStatus?.Invoke(gameWon);

        /// <summary>
        /// updates score
        /// calling all registered methods to the event
        /// </summary>
        public void UpdateScore() => updateScore?.Invoke();

        /// <summary>
        /// restarts game
        /// calling all registered methods to the event
        /// </summary>
        public void RestartGame() => restartGame?.Invoke();
    }
}