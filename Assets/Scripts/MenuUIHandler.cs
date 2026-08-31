using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button _redButton;
    [SerializeField] private Button _blueButton;
    [SerializeField] private Button _greenButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private TextMeshProUGUI _chooseColorText;
    private Coroutine _pulseCoroutine;

    private Color _redColor;
    private Color _greenColor;
    private Color _blueColor;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#CF0000", out _redColor);
        ColorUtility.TryParseHtmlString("#06A600", out _greenColor);
        ColorUtility.TryParseHtmlString("#004DD4", out _blueColor);
    }

    private void Start()
    {
        // Restore visual highlight if returning to the menu with a previously chosen color
        if (MainManager.Instance != null && MainManager.Instance.HasSelectedColor)
        {
            RestoreColorHighlight(MainManager.Instance.ChosenColor);
        }
    }

    public void OnRedButtonClicked()
    {
        MainManager.Instance.SetChosenColor(_redColor);
        HighlightButton(_redButton);
    }
    public void OnGreenButtonClicked()
    {
        MainManager.Instance.SetChosenColor(_greenColor);
        HighlightButton(_greenButton);
    }
    public void OnBlueButtonClicked()
    {
        MainManager.Instance.SetChosenColor(_blueColor);
        HighlightButton(_blueButton);
    }

    public void OnStartButtonClicked()
    {
        if (MainManager.Instance != null && MainManager.Instance.HasSelectedColor)
        {
            MainManager.Instance.StartGame();
        }
        else
        {
            Debug.LogWarning("Player must select a color before starting!");

            // Trigger the scale animation
            if (_chooseColorText != null)
            {
                if (_pulseCoroutine != null) StopCoroutine(_pulseCoroutine);
                _pulseCoroutine = StartCoroutine(PulseWarningText());
            }
        }
    }

    public void OnExitButtonClicked()
    {
        MainManager.Instance.Exit();
    }

    private void HighlightButton(Button selectedButton)
    {
        ResetButtonVisual(_redButton);
        ResetButtonVisual(_blueButton);
        ResetButtonVisual(_greenButton);

        if (selectedButton != null)
        {
            selectedButton.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
        }
    }

    private void ResetButtonVisual(Button button)
    {
        if (button != null)
        {
            button.transform.localScale = Vector3.one;
        }
    }

    private void RestoreColorHighlight(Color color)
    {
        if (color == _redColor) HighlightButton(_redButton);
        else if (color == _greenColor) HighlightButton(_greenButton);
        else if (color == _blueColor) HighlightButton(_blueButton);
    }

    private IEnumerator PulseWarningText()
    {
        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = Vector3.one * 1.3f;
        float duration = 0.15f; // Duration of scale up/down

        // Scale Up
        float elapsed = 0f;
        while (elapsed < duration)
        {
            _chooseColorText.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Scale Down
        elapsed = 0f;
        while (elapsed < duration)
        {
            _chooseColorText.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Guarantee scale resets to original size
        _chooseColorText.transform.localScale = originalScale;
    }

}
