using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConnectWires : MonoBehaviour
{
    private LineRenderer lr;
    private GameObject connector;
    private GameObject conectee;
    private EdgeCollider2D ec;
    // Start is called before the first frame update
    void Start()
    {
        ec = gameObject.GetComponent<EdgeCollider2D>();
        lr = gameObject.GetComponent<LineRenderer>();
        lr.startWidth = 0.08f;
        lr.endWidth = 0.08f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            CutLine();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(clickPoint);

            if (hit)
            {
                if (connector == null)
                {
                    connector = hit.gameObject;
                }
                else if (ec.OverlapPoint(clickPoint))
                {
                    CutLine();
                }
                else
                {
                    conectee = hit.gameObject;
                    Vector2[] linePoints = { connector.transform.position, conectee.transform.position };
                    lr.positionCount = 2;
                    lr.SetPosition(0, linePoints[0]);
                    lr.SetPosition(1, linePoints[1]);
                    ec.points = linePoints;
                    connector = null;
                }
            }
        }
    }
    void CutLine()
    {
        lr.positionCount = 0;
        ec.points = new Vector2[0];
        Debug.Log("Hi");
    }

}
