using Assets.Models;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    private int _condition;

    [SerializeField] 
    private Sprite[] _conditionSprites;

    private SpriteRenderer _spriteRenderer;

    public int Condition
    {
        get => _condition;
        set
        {
            if (value < 0)
            {
                gameObject.SetActive(false);
                Events.RaiseBlockDestroyed(this);
                return;
            }

            _condition = Mathf.Clamp(value, 0, _conditionSprites.Length - 1);
            _spriteRenderer.sprite = _conditionSprites[_condition];
        }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball == null)
        {
            return;
        }

        Condition--;
    }

}
