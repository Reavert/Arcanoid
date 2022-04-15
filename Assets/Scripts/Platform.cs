using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Platform : MonoBehaviour
{
    [SerializeField] private float _bottomOffset;

    public float Size { get; private set; }

    private Vector3 _screenDimension;
    private float _halfSize;

    void Start()
    {
        _screenDimension = FindObjectOfType<ScreenHelper>().ScreenDimension;
        Size = GetComponent<SpriteRenderer>().bounds.size.x;
        _halfSize = Size / 2.0f;

        SetPosition(0.0f);
    }

    public void SetPosition(float value)
    {
        value = Mathf.Clamp(value, -_screenDimension.x + _halfSize, _screenDimension.x - _halfSize);

        transform.position = new Vector3(value, -_screenDimension.y + _bottomOffset);
    }
}
