using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _winLayout;
    [SerializeField] private GameObject _loseLayout;

    private Game _game;

    void Awake()
    {
        _game = FindObjectOfType<Game>();

        _game.GameWon += OnGameWon;
        _game.GameOver += OnGameOver;
        _game.LevelLoaded += OnLevelLoaded;
    }

    private void OnGameWon()
    {
       _winLayout.SetActive(true);
    }
    private void OnGameOver()
    {
        _loseLayout.SetActive(true);
    }

    private void OnLevelLoaded()
    {
        _winLayout.SetActive(false);
        _loseLayout.SetActive(false);
    }

    void OnDestroy()
    {
        _game.GameWon -= OnGameWon;
        _game.GameOver -= OnGameOver;
        _game.LevelLoaded -= OnLevelLoaded;
    }
}
