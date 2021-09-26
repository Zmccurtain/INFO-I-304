using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public ColorType type;
    public Color color;
    public int row;
    public int col;
    public List<Vector2> outline;
    public CellGrid grid;
    public Character inside = null;
    public int depth = 0;

    public bool lower = false; 
    public enum ColorType
    {
        RED,
        GREEN,
        BLUE,
        BLACK,
        WHITE
    }

    
    public Cell(int row, int col, CellGrid grid)
    {
        this.grid = grid;
        this.col = col;
        this.row = row;
        this.outline = new List<Vector2>();

        for (int i = 500-(row+100); i < 500 - row; i++)
        {
            for (int j = col; j < col + 100; j++)
            {
                if (InOutline(i, j))
                {
                    this.outline.Add(new Vector2(i, j));
                }
            }
        }
    }
    
    bool InOutline(int row, int col)
    {
        
        if(row < 500 - (this.row + 10) && row > 500 - (this.row + 90))
        {
            if(col > this.col + 10 && col < this.col + 90)
            {
                return false;
            }
        }
        
        /*
        if(row > row -30 || row < row - 70)
        {
            if(col > col + 70 || col < col + 30)
            {
                return false;
            }
        }
        */
        return true;
    }

    public Color TypeToColor(ColorType type)
    {
        switch (type)
        {
            case ColorType.RED:
                return grid.material.GetColor("_RED");
            case ColorType.GREEN:
                return grid.material.GetColor("_GREEN");
            case ColorType.BLUE:
                return grid.material.GetColor("_BLUE");
            case ColorType.WHITE:
                return Color.white;
            default:
                return Color.black;
        }
    }
    public void setColor(ColorType type)
    {
        this.type = type;
        this.color = TypeToColor(type);

        foreach (Vector2 loc in this.outline)
        {
            grid.highlight.SetPixel((int)loc.y, (int)loc.x, this.color);
        }
        grid.highlight.Apply();
        grid.material.SetTexture("_Highlight", grid.highlight);
    }
}
