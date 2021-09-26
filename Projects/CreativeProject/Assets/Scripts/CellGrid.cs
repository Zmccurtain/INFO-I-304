using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    public Material material;
    public Texture2D highlight;
    public Cell[,] grid;

    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
        highlight = new Texture2D(1000, 500);
        grid = new Cell[5, 10];

        for(int i = 0; i <grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = new Cell((i * 100), (j * 100), this);
                grid[i, j].setColor(Cell.ColorType.BLACK);
            }
        }
        grid[2, 2].inside = GameObject.Find("Character").GetComponent<Character>();
        GameObject.Find("Character").GetComponent<Character>().currentCell = grid[2, 2];
    }
}

