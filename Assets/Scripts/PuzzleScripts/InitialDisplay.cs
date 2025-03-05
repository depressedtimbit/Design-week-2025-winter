using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitialDisplay : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponentInChildren<TextMeshPro>().text = GetComponent<DotLogic>().number.ToString();
    }
}
