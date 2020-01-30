using UnityEngine;
using TMPro;
using Common;
using UnityEngine.UI;

namespace UISystem
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject  gameOverPanel;

        [SerializeField] private TextMeshProUGUI scoreText, resultPanelScoreText, resultMessageText;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button startBtn;

        GameManager gameManager;
        int score;

        private void RegisterEvents()
        {
            this.gameManager.updateScore += UpdateScore;
            this.gameManager.gameStatus += UpdateGameStatus;

            restartBtn.onClick.AddListener(RestartGame);
            startBtn.onClick.AddListener(StartGame);
        }
        private void UnRegisterEvents()
        {
            this.gameManager.updateScore -= UpdateScore;
            this.gameManager.gameStatus -= UpdateGameStatus;

            restartBtn.onClick.RemoveListener(RestartGame);
            startBtn.onClick.RemoveListener(StartGame);
        }

        public void SetGameManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
            RegisterEvents();
        }


        private void OnDisable()
        {
            UnRegisterEvents();
        }

        // Start is called before the first frame update
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

        void UpdateGameStatus(bool gameWon)
        {
            //gamePanel.SetActive(false);
            scoreText.gameObject.SetActive(false);
            resultPanelScoreText.text = "SCORE: " + score;
            resultMessageText.text = gameWon == true ? "You Won!!" : "You Lost!!";
            gameOverPanel.SetActive(true);
        }

        void RestartGame()
        {
            ResetUI();
            scoreText.gameObject.SetActive(true);
            gameManager.RestartGame();
        }

        void StartGame()
        {
            ResetUI();
            scoreText.gameObject.SetActive(true);
            gameManager.StartGame();
            startBtn.gameObject.SetActive(false);
        }



        void ResetUI()
        {
            score = 000;
            scoreText.text = "SCORE: " + score;
            gameOverPanel.SetActive(false);
        }

    }
}
