using UnityEngine;

[RequireComponent(typeof(Platform))]
public class PlatformController : MonoBehaviour
{
    private Platform _platform;

    void Start()
    {
        _platform = GetComponent<Platform>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _platform.SetPosition(worldPosition.x);
        }
    }
}
