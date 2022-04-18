using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Models;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] 
    private Block _blockPrefab;

    [SerializeField] 
    [Range(0.0f, 1.0f)]
    private float _maxBlockOccupation;

    private List<Block> _blocks;

    public event Action Completed;
    public event Action Failed;

    private Ball _ball;
    private Platform _platform;

    protected override void Initialize()
    {
        _ball = FindObjectOfType<Ball>();
        _platform = FindObjectOfType<Platform>();

        _blocks = new List<Block>();
    }

    private void OnEnable()
    {
        Events.BlockDestroyed += OnBlockDestroyed;
        _ball.Fell += OnFell;
    }

    private void OnDisable()
    {
        Events.BlockDestroyed -= OnBlockDestroyed;
        _ball.Fell -= OnFell;
    }

    private void OnFell()
    {
        Failed?.Invoke();
    }

    public void StartLevel()
    {
        _platform.gameObject.SetActive(true);
        _ball.gameObject.SetActive(true);

        _platform.SetPosition(0.0f);
        _ball.transform.position = _platform.transform.position + Vector3.up;
        _ball.Launch();
    }

    public void StopLevel()
    {
        _platform.gameObject.SetActive(false);
        _ball.gameObject.SetActive(false);
    }

    private void OnBlockDestroyed(Block block)
    {
        if (!_blocks.Any(b => b.gameObject.activeInHierarchy))
        {
            Completed?.Invoke();
        }
    }

    public void Build(int[,] blocks)
    {
        foreach (Block block in _blocks)
        {
            Destroy(block.gameObject);
        }

        _blocks.Clear();

        int rowsCount = blocks.GetLength(0);
        int columnsCount = blocks.GetLength(1);

        float xStep = ScreenHelper.Instance.ScreenDimension.x * 2.0f / columnsCount;
        float blockSize = xStep / 2.0f;

        float maxSize = ScreenHelper.Instance.ScreenDimension.x * _maxBlockOccupation;
        
        blockSize = Mathf.Clamp(blockSize, 0.0f, maxSize);
        float remainingSpace = ScreenHelper.Instance.ScreenDimension.x - blockSize * columnsCount;

        for (int row = 0; row < rowsCount; row++)
        {
            for (int column = 0; column < columnsCount; column++)
            {
                if (blocks[row, column] == 0)
                {
                    continue;
                }

                Block newBlock = Instantiate(_blockPrefab,
                    new Vector3(column * (blockSize * 2f) - ScreenHelper.Instance.ScreenDimension.x + blockSize + remainingSpace,
                        -row * (blockSize * 2f) + ScreenHelper.Instance.ScreenDimension.y - blockSize), Quaternion.identity);

                newBlock.transform.localScale = new Vector3(blockSize, blockSize, 1.0f);
                newBlock.Condition = blocks[row, column] - 1;

                _blocks.Add(newBlock);
            }
        }
    }
}
