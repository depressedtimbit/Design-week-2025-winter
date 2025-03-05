using UnityEngine;

public class ViewChangeButton : MonoBehaviour
{
    public View view;

    public void Start()
    {
        if (view == null){
            Debug.LogWarning(gameObject.name + "has no set view!");
        }
    }
    public void OnMouseDown()
    {
        ViewManager.Instance.ChangeView(view); 
    }
}
