using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Color color;

    public int row;
    public int col;

    public List<Vector2> outline = new List<Vector2>();

    public Character inside = null;

    public int depth = 0;
    public List<Cell> path;
    public List<Cell> tempPath;
    

    public bool lower = false;

    public Vector3 center = Vector3.zero;
    public bool isRamp = false;

    
    public Cell(int row, int col)
    {
        path = new List<Cell>();
        tempPath = new List<Cell>();
        this.color = Color.magenta;

        this.col = col;
        this.row = row;
    }
}
