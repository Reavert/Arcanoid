using System;
using System.Collections.Generic;
using Assets.Models;
using UnityEngine;

public class Game : Singleton<Game>
{
    private ILevelLoader _loader;
    private IEnumerable<int[,]> _levels;
    private IEnumerator<int[,]> _levelEnumerator;

    public event Action GameOver;
    public event Action GameWon;
    public event Action LevelLoaded;

    protected override void Initialize()
    {
        TextAsset jsonLevelsAsset = Resources.Load<TextAsset>("levels");

        _loader = new JsonLevelLoader(jsonLevelsAsset.text);
    }

    private void OnEnable()
    {
        LevelManager.Instance.Completed += OnLevelCompleted;
        LevelManager.Instance.Failed += OnLevelFailed;
    }

    private void OnDisable()
    {
        LevelManager.Instance.Completed -= OnLevelCompleted;
        LevelManager.Instance.Failed -= OnLevelFailed;
    }

    private void Start()
    {
        _levels = _loader.Load();

        _levelEnumerator = _levels.GetEnumerator();
        _levelEnumerator.MoveNext();

        LoadLevel();
    }

    private void OnLevelFailed()
    {
        GameOver?.Invoke();

        LevelManager.Instance.StopLevel();
    }

    private void OnLevelCompleted()
    {
        if (_levelEnumerator.MoveNext())
        {
            LoadLevel();
        }
        else
        {
            GameWon?.Invoke();

            _levelEnumerator = _levels.GetEnumerator();
            _levelEnumerator.MoveNext();

            LevelManager.Instance.StopLevel();
        }
    }

    private void LoadLevel()
    {
        LevelManager.Instance.Build(_levelEnumerator.Current);
        LevelManager.Instance.StartLevel();

        LevelLoaded?.Invoke();
    }

    public void RestartLevel()
    {
        LoadLevel();
    }
}
