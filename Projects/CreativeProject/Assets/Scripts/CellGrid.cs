using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CellGrid : MonoBehaviour
{
    public Material material;
    public Texture2D highlight;
    public Cell[,] grid;

    public int width;
    public int height;
    public int pixelsPerCell;

    public MeshRenderer meshRenderer;
    public Bounds worldBounds;

    // Start is called before the first frame update
    void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        List<Transform> children = new List<Transform>(GetComponentsInChildren<Transform>(false));
        List<MeshRenderer> meshRenderers = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());
        var SizeX = meshRenderers.Max(s => s.bounds.max.x) - meshRenderers.Min(s => s.bounds.min.x);
        var SizeY = meshRenderers.Max(s => s.bounds.max.y) - meshRenderers.Min(s => s.bounds.min.y);
        var SizeZ = meshRenderers.Max(s => s.bounds.max.z) - meshRenderers.Min(s => s.bounds.min.z);
        Vector3 size = new Vector3(SizeX, SizeY, SizeZ);

        Vector3 center = Vector3.zero;
        foreach(Transform i in children)
        {
            
            if(i.name != name)
            {
                center += i.position;
            }
        }
        center = meshRenderers.Aggregate(Vector3.zero, (y, x) => x.bounds.center + y);
        center /= (transform.childCount);

        
        worldBounds = new Bounds(center, size);
        material = meshRenderer.sharedMaterial;
        material.SetVector("_Size", new Vector2(width, height));
        highlight = new Texture2D(width*pixelsPerCell, height*pixelsPerCell);
        grid = new Cell[height, width];


        for(int i = 0; i <grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                Cell cur = new Cell((i * pixelsPerCell), (j * pixelsPerCell));
                setCenter(cur, i, j);
                makeOutline(cur, i*pixelsPerCell, j*pixelsPerCell);
                setColor(cur, ColorType.BLACK);
                grid[i, j] = cur;
            }
        }
        Character character = GameObject.Find("Character").GetComponent<Character>();
        var start = grid[6, 1];
        start.inside = character;
        character.targetCell = start;
        character.currentCell = start;

        Character enemy = GameObject.Find("Enemy").GetComponent<Character>();
        var start2 = grid[6, 11];
        start2.inside = enemy;
        enemy.targetCell = start2;
        enemy.currentCell = start2;
    }
    void setCenter(Cell cur, int row, int col)
    { 
        float centerX = worldBounds.max.x - ((col + .5f) / width * worldBounds.size.x);
        float centerZ = ((row + .5f) / height * worldBounds.size.z) + worldBounds.min.z;
        float centerY;
        RaycastHit hit;
        Physics.Raycast(new Vector3(centerX, worldBounds.max.y + 5, centerZ), Vector3.down, out hit, worldBounds.size.y + 20, LayerMask.GetMask("Grid"));
        centerY = hit.point.y;
        
        cur.center = new Vector3(centerX, centerY, centerZ);
        if(cur.center.y > .3f && cur.center.y < 3.5f)
        {
            cur.isRamp = true;
        }
    }
    void makeOutline(Cell cur, int row, int col)
    {
        for (int i = (height*pixelsPerCell) - (row + pixelsPerCell); i < (height*pixelsPerCell) - row; i++)
        {
            
            for (int j = col; j < col + pixelsPerCell; j++)
            {
                if (InOutline(cur, i, j))
                {
                    cur.outline.Add(new Vector2(i, j));
                }
            }
        }
    }
    bool InOutline(Cell cell, int row, int col)
    {
        if (row >= (height * pixelsPerCell) - cell.row - pixelsPerCell + (pixelsPerCell * .15f) && row <= (height * pixelsPerCell) - cell.row - pixelsPerCell + (pixelsPerCell *.78f))
        {
            if (col >= cell.col + (pixelsPerCell * .15f) && col <= cell.col + (pixelsPerCell * .78f))
            {
                return false;

            }
        }

        return true;
    }

    public enum ColorType
    {
        RED,
        GREEN,
        BLUE,
        BLACK,
        WHITE
    }

    public Color TypeToColor(ColorType type)
    {
        switch (type)
        {
            case ColorType.RED:
                return material.GetColor("_RED");
            case ColorType.GREEN:
                return material.GetColor("_GREEN");
            case ColorType.BLUE:
                return material.GetColor("_BLUE");
            case ColorType.WHITE:
                return Color.white;
            default:
                return Color.black;
        }
    }
    public void setColor(Cell cur, ColorType type)
    {
        cur.color = TypeToColor(type);

        foreach (Vector2 loc in cur.outline)
        {
            highlight.SetPixel((int)loc.y, (int)loc.x, cur.color);
        }
        highlight.Apply();
        material.SetTexture("_Highlight", highlight);
    }

    public override string ToString()
    {
        string s = "[";
        foreach(Cell cell in grid)
        {
            if (cell == null)
            {
                s += "null, ";
                continue;
            }
            s += cell.ToString() + ", ";
        }
        return s + "]";
    }
    /*
    private void OnDrawGizmosSelected()
    {
        foreach(Cell cell in grid)
        {
            Gizmos.DrawSphere(cell.center, .5f);
        }
    }
    */
}

