using UnityEngine;

public class View : MonoBehaviour
{
    public void deactivateView()
    {
        this.gameObject.SetActive(false);
    }

    public void activateView()
    {
        this.gameObject.SetActive(true);
    }
    
}
