using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Ball : MonoBehaviour
{
    public float Speed;
    public float Bounce;

    private Rigidbody2D _rigidbody;
    private ScreenHelper _screenHelper;
    private Vector2 _halfBound;


    public delegate void FellHandler();
    public FellHandler Fell;

    public void Launch()
    {
        _rigidbody.velocity = Vector2.up * Speed;
    }

    void Awake()
    {
        _screenHelper = FindObjectOfType<ScreenHelper>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _halfBound = GetComponent<SpriteRenderer>().bounds.size / 2.0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Platform platform = collision.gameObject.GetComponent<Platform>();

        if (platform == null)
        {
            return;
        }

        float angleMultiplier = (collision.transform.position.x - transform.position.x) / (platform.Size);
        _rigidbody.velocity = new Vector2(-angleMultiplier * Bounce, Speed);
    }

    void FixedUpdate()
    {
        if (transform.position.x > _screenHelper.ScreenDimension.x ||
            transform.position.x < -_screenHelper.ScreenDimension.x)
        {
            transform.position =
                new Vector3(
                    Mathf.Clamp(transform.position.x, 
                        -_screenHelper.ScreenDimension.x, 
                        _screenHelper.ScreenDimension.x), 
                    transform.position.y);

            _rigidbody.velocity *= new Vector2(-1.0f, 1.0f);
        }

        if (transform.position.y > _screenHelper.ScreenDimension.y)
        {
            transform.position =
                new Vector3(transform.position.x, transform.position.y - _halfBound.y);

            _rigidbody.velocity *= new Vector2(1.0f, -1.0f);
        }

        if (transform.position.y < -_screenHelper.ScreenDimension.y)
        {
            Fell?.Invoke();
        }
    }
}
