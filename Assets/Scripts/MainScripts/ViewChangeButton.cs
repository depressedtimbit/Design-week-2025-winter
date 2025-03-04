using UnityEngine;

public class ViewChangeButton : MonoBehaviour
{
    public View view;
    public void OnMouseDown()
    {
        ViewManager.Instance.ChangeView(view);
    }
}
