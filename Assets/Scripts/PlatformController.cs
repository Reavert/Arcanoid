using UnityEngine;

[RequireComponent(typeof(Platform))]
public class PlatformController : MonoBehaviour
{
    private Platform _platform;

    private void Start()
    {
        _platform = GetComponent<Platform>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _platform.SetPosition(worldPosition.x);
        }
    }
}
