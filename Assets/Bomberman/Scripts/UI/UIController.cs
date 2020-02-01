using UnityEngine;
using TMPro;
using Common;
using UnityEngine.UI;

namespace UISystem
{
    /// <summary>
    /// controls UI of game
    /// </summary>
    public class UIController : MonoBehaviour
    {
        #region Visible in Inspector fields
        
        [Header("Panels")]
        [SerializeField] private GameObject  gameOverPanel;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI  resultPanelScoreText;
        [SerializeField] private TextMeshProUGUI resultMessageText,titleText;

        [Header("Buttons")]
        [Tooltip("Restart button")]
        [SerializeField] private Button restartBtn;

        [Tooltip("Start button")]
        [SerializeField] private Button startBtn;

        [Tooltip("Main Quit button")]
        [SerializeField] private Button quitButton;

        [Tooltip("Result screen Quit button")]
        [SerializeField] private Button resultscreenQuitButton;

        #endregion

        
        GameManager gameManager;
        int score;

        #region Events

        private void RegisterEvents()
        {
            this.gameManager.updateScore += UpdateScore;
            this.gameManager.gameStatus += UpdateGameStatus;

            restartBtn.onClick.AddListener(RestartGame);
            startBtn.onClick.AddListener(StartGame);
            quitButton.onClick.AddListener(QuitGame);
            resultscreenQuitButton.onClick.AddListener(QuitGame);
        }
        private void UnRegisterEvents()
        {
            this.gameManager.updateScore -= UpdateScore;
            this.gameManager.gameStatus -= UpdateGameStatus;

            restartBtn.onClick.RemoveListener(RestartGame);
            startBtn.onClick.RemoveListener(StartGame);
            quitButton.onClick.RemoveListener(QuitGame);
            resultscreenQuitButton.onClick.RemoveListener(QuitGame);
        }

        #endregion


        public void SetGameManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnRegisterEvents();
        }

     
        void Start()
        {
            ResetUI();
            DontDestroyOnLoad(gameObject);
        }

        void UpdateScore()
        {
            score += 10;
            scoreText.text = "SCORE: " + score;
        }

        /// <summary>
        /// update UI on game over
        /// </summary>
        /// <param name="gameWon">won or loss boolean</param>
        void UpdateGameStatus(bool gameWon)
        {
            scoreText.gameObject.SetActive(false);
            resultPanelScoreText.text = "SCORE: " + score;
            resultMessageText.text = gameWon == true ? "You Won!!" : "You Lost!!";
            gameOverPanel.SetActive(true);
        }
        void ResetUI()
        {
            score = 000;
            scoreText.text = "SCORE: " + score;
            gameOverPanel.SetActive(false);
        }

        #region Buttons

        void RestartGame()
        {
            ResetUI();
            scoreText.gameObject.SetActive(true);
            GameManager.Instance.OnInputTrigger(EventState.restartButton, EventType.mouseDown);
        }

        void StartGame()
        {
            ResetUI();
            scoreText.gameObject.SetActive(true);
            startBtn.gameObject.SetActive(false);
            titleText.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            GameManager.Instance.OnInputTrigger(EventState.startButton, EventType.mouseDown);
        }

        void QuitGame()
        {
            GameManager.Instance.OnInputTrigger(EventState.quitButton, EventType.mouseDown);
        }


        #endregion


    }
}
