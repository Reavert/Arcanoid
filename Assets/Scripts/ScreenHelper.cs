using UnityEngine;

public class ScreenHelper : Singleton<ScreenHelper>
{
    public Vector3 ScreenDimension { get; private set; }

    protected override void Initialize()
    {
        ScreenDimension = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }
}
