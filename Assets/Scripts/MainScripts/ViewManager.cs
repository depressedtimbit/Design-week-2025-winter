using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance;
    public View currentView;
    void Start()
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
    }
    public void ChangeView(View view)
    {
        // dither between views
        CurtainManager.instance.DitherIn(currentView, view, 1.5f);

        currentView.deactivateView();

        currentView = view;
        
        currentView.activateView();
    }

}
