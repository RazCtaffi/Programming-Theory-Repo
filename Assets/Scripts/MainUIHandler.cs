using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    public static MainUIHandler Instance { get; private set; }

    [Header("Game UI")]
    [SerializeField] private GameObject _gameUIScreen;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _controlsExplanation;
    
    [Header("Game Over Screen")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private Button _backToMenuButton;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (_gameUIScreen != null)
        {
            _gameUIScreen.SetActive(true);
        }
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
        }
        if (_backToMenuButton != null)
        {
            _backToMenuButton.onClick.AddListener(OnBackToMenuPressed);
        }
    }

    public void HideGameUI()
    {
        if (_gameUIScreen != null)
        {
            _gameUIScreen.SetActive(false);
        }
    }

    public void ShowGameOverScreen(int score = 0)
    {
        if (_gameOverText != null)
        {
            _gameOverText.text = $"Game Over!\nScore: {score}";
        }
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);
        }
    }

    public void UpdateScore(int currentScore)
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"Score: {currentScore}";
        }
    }

    private void OnBackToMenuPressed()
    {
        if (MainManager.Instance !=null)
        {
            MainManager.Instance.ReturnToMenu();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
