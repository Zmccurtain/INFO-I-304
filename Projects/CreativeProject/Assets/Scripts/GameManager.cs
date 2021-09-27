using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public CellGrid lower;
    public CellGrid upper;

    public List<Character> characters;
    public Character selected;

    private IEnumerator coroutine;
    // Start is called before the first frame update
    private void Start()
    {
        foreach (Cell cell in lower.grid)
        {
            cell.lower = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (selected == null)
                {
                    selected = hit.collider.gameObject.GetComponent<Character>();
                    if (selected != null)
                    {
                        foreach (Cell cell in BFS(selected.currentCell, selected.movement))
                        {
                            cell.setColor(Cell.ColorType.RED);
                        }
                    }
                }

                else
                {
                    CellGrid gridHit;
                    if (hit.point.y > 100) gridHit = upper;
                    else gridHit = lower;
                    selected.loc = gridHit.grid[(int)((gridHit.transform.position.z - hit.point.z - 11.98f) * -1 / ((11.98f * 2) / 5))
                                                , (int)((gridHit.transform.position.x - hit.point.x + 22.38f) / ((22.38f * 2) / 10))].center;
                    foreach (Cell cell in BFS(selected.currentCell, selected.movement))
                    {
                        cell.setColor(Cell.ColorType.BLACK);
                    }
                    selected = null;
                }
            }
        }
    }


    public List<Cell> BFS(Cell root, int depthLimit)
    {
        List<Cell> within = new List<Cell>();
        root.depth = 0;
        Queue<Cell> q = new Queue<Cell>();
        q.Enqueue(root);
        while (q.Count > 0)
        {
            Cell cur = q.Dequeue();
            if(cur.depth > depthLimit)
            {
                continue;
            }
            within.Add(cur);

            List<Cell> children = new List<Cell>();

            if (cur.lower) children = getAdjacentL(cur.row, cur.col);
            else children = getAdjacentU(cur.row, cur.col);
            

            foreach(Cell cell in children)
            {
                cell.depth = cur.depth + 1;
                q.Enqueue(cell);
            }
        }

        return within;
    }

    public List<Cell> getAdjacentL(int row, int col)
    {
        List<Cell> children = new List<Cell>();
        Vector2[] moves = { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };
        foreach (Vector2 move in moves)
        {

            int newRow = (int)((row / 100) - move.x);
            int newCol = (int)((col / 100) - move.y);

            if (newRow < 0) children.Add(upper.grid[4, newCol]);

            if (newRow >= 0 && newRow < 5 && newCol >= 0 && newCol < 10) children.Add(lower.grid[newRow, newCol]);
        }

        return children;
    }

    public List<Cell> getAdjacentU(int row, int col)
    {
        List<Cell> children = new List<Cell>();
        Vector2[] moves = { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };
        foreach (Vector2 move in moves)
        {

            int newRow = (int)((row / 100) - move.x);
            int newCol = (int)((col / 100) - move.y);

            if (newRow < 0) children.Add(lower.grid[0, newCol]);

            if (newRow >= 0 && newRow < 5 && newCol >= 0 && newCol < 10) children.Add(upper.grid[newRow, newCol]);
        }

        return children;
    }
}
