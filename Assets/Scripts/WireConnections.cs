using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireConnections : MonoBehaviour
{
    public DotLogic dot1;
    public DotLogic dot2;
    void Start()
    {
        if(dot1 != null && dot2 != null)
        {
            dot1.ConnectDots(dot2);
            dot2.ConnectDots(dot1);
        }
    }

    public void SetValue(DotLogic start, DotLogic end)
    {
        dot1 = start;
        dot2 = end;
    }
}
