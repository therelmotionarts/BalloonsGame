﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField]
    private GameObject linePrefab;
    [SerializeField]
    private GameObject currentLine;

    public LineRenderer lineRenderer;
    //public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;
    public PolygonCollider2D polygonCollider;
    public CircleCollider2D circleCollider;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }

        }
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        //edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        circleCollider = currentLine.GetComponent<CircleCollider2D>();
        polygonCollider = currentLine.GetComponent<PolygonCollider2D>();
        fingerPositions.Clear();
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        //edgeCollider.points = fingerPositions.ToArray();
        polygonCollider.points = fingerPositions.ToArray();

    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        ++lineRenderer.positionCount;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        //edgeCollider.points = fingerPositions.ToArray();
        polygonCollider.points = fingerPositions.ToArray();
    }
}