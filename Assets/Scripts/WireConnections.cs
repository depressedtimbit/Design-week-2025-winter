using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireConnections : MonoBehaviour
{
    public DotLogic dot1;
    public DotLogic dot2;
    void Start()
    {
        
    }

    public void SetValue(DotLogic start, DotLogic end)
    {
        dot1 = start;
        dot2 = end;
    }
}
