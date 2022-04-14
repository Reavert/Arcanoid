using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScreenFitter : MonoBehaviour
{
    private ScreenHelper _screenHelper;
    void Start()
    {
        _screenHelper = FindObjectOfType<ScreenHelper>();
        Vector3 spriteSize = GetComponent<SpriteRenderer>().bounds.size;

        float scaleFactorX = _screenHelper.ScreenDimension.x * 2.0f / spriteSize.x;
        float scaleFactorY = _screenHelper.ScreenDimension.y * 2.0f / spriteSize.y;

        transform.localScale = new Vector3(scaleFactorX, scaleFactorY, 1.0f);
    }
}
