using UnityEngine;

public class ScreenHelper : MonoBehaviour
{
    public Vector3 ScreenDimension { get; private set; }

    void Awake()
    {
        ScreenDimension = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }
}
