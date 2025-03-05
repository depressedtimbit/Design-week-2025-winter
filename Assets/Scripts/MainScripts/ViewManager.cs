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

        //activate our current view, as they turn themselves off in awake
        currentView.activateView();
    }
    public void ChangeView(View view)
    {
        currentView.deactivateView();

        currentView = view;
        
        currentView.activateView();
    }

}
