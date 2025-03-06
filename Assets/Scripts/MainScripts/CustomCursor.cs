
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomCursor : MonoBehaviour
{

    RaycastHit hit;
    public Texture2D DefaultCursor;
    public Texture2D HoverCursor;
    public Vector2 cursor_offSet;
    private Camera CurrentCamera;


    // Start is called before the first frame update
    void Start()
    {
        ViewManager.Instance.OnCameraChanged.AddListener(OnCameraChange);
        Cursor.SetCursor(DefaultCursor, cursor_offSet, CursorMode.Auto);
    }

    void Update()
    {
        if (CurrentCamera != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, 999))
            {
                Cursor.SetCursor(HoverCursor, cursor_offSet, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(DefaultCursor, cursor_offSet, CursorMode.Auto);
            }
            Debug.DrawRay(ray.origin, ray.direction, Color.cyan);  
        }
    }

    public void OnCameraChange(Camera camera)
    {
        CurrentCamera = camera;
    }
}
