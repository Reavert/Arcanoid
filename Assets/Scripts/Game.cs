using System;
using System.Collections.Generic;
using Assets.Models;
using UnityEngine;

public class Game : MonoBehaviour
{
    private ILevelLoader _loader;
    private IEnumerable<int[,]> _levels;
    private IEnumerator<int[,]> _levelEnumerator;

    private LevelManager _levelManager;

    public event Action GameOver;
    public event Action GameWon;
    public event Action LevelLoaded;

    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _levelManager.Completed += OnLevelCompleted;
        _levelManager.Failed += OnLevelFailed;

        TextAsset jsonLevelsAsset = Resources.Load<TextAsset>("levels");

        _loader = new JsonLevelLoader(jsonLevelsAsset.text);
        _levels = _loader.Load();

        _levelEnumerator = _levels.GetEnumerator();
        _levelEnumerator.MoveNext();

        LoadLevel();
    }

    private void OnLevelFailed()
    {
        GameOver.Invoke();

        _levelManager.StopLevel();
    }

    private void OnLevelCompleted()
    {
        if (_levelEnumerator.MoveNext())
        {
            LoadLevel();
        }
        else
        {
            GameWon.Invoke();

            _levelEnumerator = _levels.GetEnumerator();
            _levelEnumerator.MoveNext();

            _levelManager.StopLevel();
        }
    }

    private void LoadLevel()
    {
        _levelManager.Build(_levelEnumerator.Current);
        _levelManager.StartLevel();

        LevelLoaded.Invoke();
    }

    public void RestartLevel()
    {
        LoadLevel();
    }

    void OnDestroy()
    {
        _levelManager.Completed -= OnLevelCompleted;
        _levelManager.Failed -= OnLevelFailed;
    }
}
