using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFitter : MonoBehaviour
{
    private void Start()
    {
        Vector3 spriteSize = GetComponent<SpriteRenderer>().bounds.size;

        float scaleFactorX = ScreenHelper.Instance.ScreenDimension.x * 2.0f / spriteSize.x;
        float scaleFactorY = ScreenHelper.Instance.ScreenDimension.y * 2.0f / spriteSize.y;

        transform.localScale = new Vector3(scaleFactorX, scaleFactorY, 1.0f);
    }
}
