using UnityEngine;

public class View : MonoBehaviour
{
    public Camera cam;

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
