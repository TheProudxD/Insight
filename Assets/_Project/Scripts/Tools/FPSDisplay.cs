using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private float _deltaTime;
    public bool ShowInfo = true;
    public Vector2 Resolution = new(1280, 720);
    public int SetFPS = 60;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Screen.SetResolution((int)Resolution.x, (int)Resolution.y, true);
        Application.targetFrameRate = SetFPS;
    }

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        if (ShowInfo)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = _deltaTime * 1000.0f;
            float fps = 1.0f / _deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);


            GUI.Label(rect, text, style);

            Rect rect2 = new Rect(250, 0, w, h * 2 / 100);
            GUI.Label(rect2, Screen.currentResolution.width + "x" + Screen.currentResolution.height, style);
        }
    }
}