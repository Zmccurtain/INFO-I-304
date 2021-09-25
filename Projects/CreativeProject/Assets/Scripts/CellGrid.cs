using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    public Material material;
    public Texture2D highlight;
    public int[,] grid;

    public int colLimit = 4;
    public int rowLimit = 2;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
        highlight = new Texture2D(1000, 500);
        grid = new int[,]{  { 0,0,0,0,1,0,0,0,0,0 },
                            { 0,0,0,0,0,0,0,0,0,0 },
                            { 0,0,0,0,0,0,1,0,0,0 },
                            { 0,0,1,0,0,0,0,0,0,0 },
                            { 0,0,0,0,0,0,0,0,0,0 } };

        for(int row = 0; row < highlight.height; row++)
        {
            for(int col = 0; col< highlight.width; col++)
            {
                highlight.SetPixel(col, row, Color.black);
            }
        }
        Draw();
    }

    public void Draw()
    {
        Color color = Color.black;
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                
                
                if (grid[row,col] == 1)
                {
                    Debug.Log("hello");
                    color = Color.white;
                }
                else
                {
                    color = Color.black;
                }

                for (int i = 500 - ((row + 1) * 100); i <= 500 - (row * 100); i++)
                {
                    for (int j = col*100; j <= (col+1)*100; j++)
                    {
                        highlight.SetPixel(j, i, color);
                    }
                }
                
            }

        }
        highlight.Apply();
        material.SetTexture("_Highlight", highlight);
    }

    // Update is called once per frame
    public void Update()
    {
        
    }


}

