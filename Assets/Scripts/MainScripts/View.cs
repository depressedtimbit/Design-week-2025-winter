using UnityEngine;

public class View : MonoBehaviour
{

    public Camera cam;

    private void Awake()
    {
        deactivateView(); //automatically deactivate everything, view manager will reactivate our inital one
    }

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    public void deactivateView()
    {
        this.gameObject.SetActive(false);
    }

    public void activateView()
    {
        this.gameObject.SetActive(true);
    }
    
}
