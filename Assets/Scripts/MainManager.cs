using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Color ChosenColor { get; private set; }
    public bool HasSelectedColor { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetChosenColor(Color color)
    {
        ChosenColor = color;
        HasSelectedColor = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
