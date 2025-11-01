using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCore : MonoBehaviour
{
    public static GameCore main;

    [Header("UI Elements")]
    public Image hpBar;
    public Image hpBarFilled;
    public TextMeshProUGUI hpText;
    public GameObject gameOverPanel;

    private bool _gameOver = false;
    public bool IsGameOver => _gameOver;

    private void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _gameOver = false;

        if (gameOverPanel)
            gameOverPanel.SetActive(false);
    }

    public void SetHP(int current, int max)
    {
        if (hpBarFilled == null) return;

        float ratio = Mathf.Clamp01((float)current / max);
        hpBarFilled.fillAmount = ratio;

        if (hpText)
            hpText.text = $"{current} / {max}";
    }

    public void GameOver()
    {
        if (_gameOver) return;

        _gameOver = true;

        if (gameOverPanel)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("Player is dead. Game over!");
    }
}
