using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    [Min(0.0f)]
    private float _speed;

    [SerializeField]
    [Min(0.0f)]
    private float _bounce;

    private Rigidbody2D _rigidbody;
    private Vector2 _halfBound;

    public Action Fell;

    public void Launch()
    {
        _rigidbody.velocity = Vector2.up * _speed;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _halfBound = GetComponent<SpriteRenderer>().bounds.size / 2.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Platform platform = collision.gameObject.GetComponent<Platform>();

        if (platform == null)
        {
            return;
        }

        float angleMultiplier = (collision.transform.position.x - transform.position.x) / (platform.Size);
        _rigidbody.velocity = new Vector2(-angleMultiplier * _bounce, _speed);
    }

    private void FixedUpdate()
    {
        if (transform.position.x > ScreenHelper.Instance.ScreenDimension.x ||
            transform.position.x < -ScreenHelper.Instance.ScreenDimension.x)
        {
            transform.position =
                new Vector3(
                    Mathf.Clamp(transform.position.x, 
                        -ScreenHelper.Instance.ScreenDimension.x,
                        ScreenHelper.Instance.ScreenDimension.x), 
                    transform.position.y);

            _rigidbody.velocity *= new Vector2(-1.0f, 1.0f);
        }

        if (transform.position.y > ScreenHelper.Instance.ScreenDimension.y)
        {
            transform.position =
                new Vector3(transform.position.x, transform.position.y - _halfBound.y);

            _rigidbody.velocity *= new Vector2(1.0f, -1.0f);
        }

        if (transform.position.y < -ScreenHelper.Instance.ScreenDimension.y)
        {
            Fell?.Invoke();
        }
    }
}
