using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField] 
    private GameObject _winLayout;

    [SerializeField] 
    private GameObject _loseLayout;

    protected override void Initialize()
    { }

    private void OnEnable()
    {
        Game.Instance.GameWon += OnGameWon;
        Game.Instance.GameOver += OnGameOver;
        Game.Instance.LevelLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        Game.Instance.GameWon -= OnGameWon;
        Game.Instance.GameOver -= OnGameOver;
        Game.Instance.LevelLoaded -= OnLevelLoaded;
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
}
