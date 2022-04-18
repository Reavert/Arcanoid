using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Platform : MonoBehaviour
{
    [SerializeField] 
    private float _bottomOffset;

    public float Size { get; private set; }
    private float _halfSize;

    private void Start()
    {
        Size = GetComponent<SpriteRenderer>().bounds.size.x;
        _halfSize = Size / 2.0f;

        SetPosition(0.0f);
    }

    public void SetPosition(float value)
    {
        value = Mathf.Clamp(
            value, 
            -ScreenHelper.Instance.ScreenDimension.x + _halfSize, 
            ScreenHelper.Instance.ScreenDimension.x - _halfSize);

        transform.position = new Vector3(value, -ScreenHelper.Instance.ScreenDimension.y + _bottomOffset);
    }
}
