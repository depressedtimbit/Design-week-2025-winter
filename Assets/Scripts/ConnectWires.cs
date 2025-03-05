using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConnectWires : MonoBehaviour
{
    public GameObject wirePrefab;
    private GameObject connector;
    private GameObject conectee;
    private List<GameObject> wires = new List<GameObject>();
    private bool canWeld = true;
    private bool canCut = false;
    private bool canProbe = false;
    private int maxPos = 1;
    private int maxNeg = 1;
    private int positiveButtons = 0;
    private int negativeButtons = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(clickPoint);
        if (Input.GetMouseButtonDown(0))
        {
            if (canProbe)
            {
                if (hit && (hit.gameObject.CompareTag("Button") || hit.gameObject.CompareTag("NoPress")))
                {
                    ButtonType(hit.gameObject, true);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (canWeld)
                {
                    if (hit )
                    {
                        if (connector == null && hit.gameObject.CompareTag("Button"))
                        {
                            connector = hit.gameObject;
                        }
                        else if (connector != null && hit.gameObject.CompareTag("NoPress"))
                        {
                            conectee = hit.gameObject;
                            CreateWire(connector, conectee);
                            connector = null;
                        }
                    }
                }

            else if (canCut)
                {
                    RaycastHit2D lineHit = Physics2D.Raycast(clickPoint, Vector2.zero);
                    if (lineHit.collider != null && lineHit.collider.CompareTag("Wire"))
                    {
                        if (canCut)
                            CutLine(lineHit.collider.gameObject);
                    }
                }

            else if (canProbe)
            {
                if (hit && hit.gameObject.CompareTag("Button") || hit.gameObject.CompareTag("NoPress"))
                {
                    ButtonType(hit.gameObject, false);
                }

            }
        }
    }

    void CreateWire(GameObject start, GameObject end)
    {
        GameObject newWire = Instantiate(wirePrefab);
        LineRenderer lr = newWire.GetComponent<LineRenderer>();
        EdgeCollider2D ec = newWire.GetComponent<EdgeCollider2D>();
        Vector2[] linePoints = { start.transform.position, end.transform.position };
        lr.positionCount = 2;
        lr.SetPosition(0, linePoints[0]);
        lr.SetPosition(1, linePoints[1]);
        ec.points = linePoints;

        DotLogic dot1 = start.GetComponent<DotLogic>();
        DotLogic dot2 = end.GetComponent<DotLogic>();
        if(dot1 !=null & dot2 != null)
        {
            dot1.ConnectDots(dot2);
            dot2.ConnectDots(dot1);
        }

        WireConnections wireConnection = newWire.GetComponent<WireConnections>();
        wireConnection.SetValue(dot1, dot2);
        wires.Add(newWire);
    }

    void ButtonType(GameObject button, bool positive)
    {
        DotLogic dot = button.GetComponent<DotLogic>();
        if (dot != null)
        {
            if (positive && positiveButtons < maxPos)
            {
                dot.SetPositive();
                positiveButtons++;
            }else if(!positive && negativeButtons < maxNeg)
            {
                dot.SetNegative();
                negativeButtons++;
            }
        }
    }
    void CutLine(GameObject wire)
    {
        WireConnections wireConnection = wire.GetComponent<WireConnections>();
        if(wireConnection != null)
        {
            wireConnection.dot1.DisconnectDots(wireConnection.dot2);
            wireConnection.dot2.DisconnectDots(wireConnection.dot1);
        }
        wires.Remove(wire);
        Destroy(wire);
    }

    public void EnableWelding()
    {
        canWeld = true;
        canCut = false;
        canProbe = false;
    }

    public void EnableCutting()
    {
        canCut = true;
        canWeld = false;
        canProbe = false;
    }

    public void EnableProbe()
    {
        canProbe = true;
        canWeld = false;
        canCut = false;
    }

    public void DisableTools()
    {
        canCut = false;
        canWeld = false;
        canProbe = false;
    }

}
