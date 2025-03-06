using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance;
    public View currentView;
    public UnityEvent<Camera> OnCameraChanged;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("Multiple PlayerData's exist, deleting the latest one");
            Destroy(this);
        }

        if (OnCameraChanged == null) OnCameraChanged = new UnityEvent<Camera>();

    }
    void Start()
    {
        //activate our current view, as they turn themselves off in awake
        ChangeView(currentView);
    }
    public void ChangeView(View view)
    {
        // dither between views
        CurtainManager.instance.DitherIn(currentView, view, 0.8f);

        currentView.deactivateView();

        currentView = view;
        
        currentView.activateView();
        OnCameraChanged.Invoke(currentView.cam);
    }

}
