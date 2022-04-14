using System.Collections.Generic;
using System.Linq;
using Assets.Models;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Block _blockPrefab;

    [HideInInspector] public List<Block> Blocks;
    private ScreenHelper _screenHelper;

    public delegate void LevelCompletedHandler();
    public event LevelCompletedHandler Completed;

    public delegate void LevelFailedHandler();
    public event LevelFailedHandler Failed;

    private Ball _ball;
    private Platform _platform;

    void Awake()
    {
        _screenHelper = FindObjectOfType<ScreenHelper>();
        _ball = FindObjectOfType<Ball>();
        _platform = FindObjectOfType<Platform>();

        Events.BlockDestroyed += OnBlockDestroyed;
        _ball.Fell += OnFell;

        Blocks = new List<Block>();
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
        if (!Blocks.Any(b => b.gameObject.activeInHierarchy))
        {
            Completed?.Invoke();
        }
    }

    public void Build(int[,] blocks)
    {
        foreach (Block block in Blocks)
        {
            Destroy(block.gameObject);
        }

        Blocks.Clear();

        int rowsCount = blocks.GetLength(0);
        int columnsCount = blocks.GetLength(1);

        float xStep = _screenHelper.ScreenDimension.x * 2.0f / columnsCount;
        float blockSize = xStep / 2.0f;

        for (int row = 0; row < rowsCount; row++)
        {
            for (int column = 0; column < columnsCount; column++)
            {
                if (blocks[row, column] == 0)
                {
                    continue;
                }

                Block newBlock = Instantiate(_blockPrefab,
                    new Vector3(column * xStep - _screenHelper.ScreenDimension.x + blockSize,
                        -row * xStep + _screenHelper.ScreenDimension.y - blockSize), Quaternion.identity);

                newBlock.transform.localScale = new Vector3(blockSize, blockSize, 1.0f);
                newBlock.Condition = blocks[row, column] - 1;
                Blocks.Add(newBlock);
            }
        }
    }

    void OnDestroy()
    {
        Events.BlockDestroyed -= OnBlockDestroyed;
        _ball.Fell -= OnFell;
    }
}