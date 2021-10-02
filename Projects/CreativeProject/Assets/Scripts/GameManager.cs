using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public HPbar hPbar;
    public CellGrid grid;

    public List<Character> characters;
    public Character selected = null;
    public CellGrid.ColorType moveColor = CellGrid.ColorType.BLUE;

    public bool attacking = false;

    List<Cell> withinMove;
    List<Cell> withinAttack;
    // Start is called before the first frame update
    private void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<CellGrid>();
        characters = new List<Character>(GetComponentsInChildren<Character>());
        hPbar.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, LayerMask.GetMask("Grid")))
            {
                var col = (int)((grid.worldBounds.max.x - hit.point.x) / grid.worldBounds.size.x * grid.width);
                var row = (int)((hit.point.z - grid.worldBounds.min.z) / grid.worldBounds.size.z * grid.height);
                Cell cellHit = grid.grid[row, col];
                if (selected != null)
                {
                    if (attacking)
                    {
                        Attacking(cellHit);
                    }
                    else
                    {
                        Moving(cellHit);
                    }


                    selected = null;
                    hPbar.selected = null;
                    hPbar.gameObject.SetActive(false);
                    return;
                }
                if (cellHit.inside != null)
                {
                    selected = cellHit.inside;
                    withinMove = new List<Cell>(BFS(selected.currentCell, selected.movementRange));
                    setToColor(withinMove, moveColor);
                    hPbar.gameObject.SetActive(true);
                    hPbar.selected = selected;
                }
            }
        }
    }

    void Attacking(Cell cellHit)
    {
        setToColor(withinAttack, CellGrid.ColorType.BLACK);
        if (withinAttack.Contains(cellHit))
        {
            cellHit.inside.takeDamage(selected.damage);
            attacking = false;
        }
    }

    void Moving(Cell cellHit)
    {
        setToColor(withinMove, CellGrid.ColorType.BLACK);
        if (withinMove.Contains(cellHit))
        {
            
            selected.targetCell.inside = null;
            cellHit.inside = selected;
            cellHit.path = new List<Cell>(cellHit.tempPath);
            cellHit.tempPath = new List<Cell>();
            selected.targetCell = cellHit;
        }
    }

    void setToColor(List<Cell> cells, CellGrid.ColorType type)
    {
        foreach(Cell cell in cells)
        {
            grid.setColor(cell, type);
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
                return within;
            }
            within.Add(cur);

            if (cur.depth < depthLimit)
            {
                foreach (Cell cell in getAdjacent(cur, cur.row, cur.col))
                {
                    if (!within.Contains(cell) && !q.Contains(cell))
                    {
                        cell.tempPath = new List<Cell>(cur.tempPath)
                        {
                            cur
                        };
                        cell.depth = cur.depth + 1;
                        q.Enqueue(cell);
                    }
                }
            }
        }
        return within;
    }

    public List<Cell> getAdjacent(Cell root, int row, int col)
    {
        List<Cell> children = new List<Cell>();
        Vector2[] moves = { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };
        foreach (Vector2 move in moves)
        {

            int newRow = (int)((row / grid.pixelsPerCell) - move.x);
            int newCol = (int)((col / grid.pixelsPerCell) - move.y);
            
            if (newRow >= 0 && newRow < grid.height && newCol >= 0 && newCol < grid.width)
            {
                Cell child = grid.grid[newRow, newCol];

                if (move == new Vector2(1, 0))
                {
                    if (root.isRamp)
                    {
                        children.Add(child);
                        continue;
                    }
                }
                if (child.isRamp)
                {
                    children.Add(child);
                    continue;
                }
                if (root.center.y - child.center.y > -.5f) {
                    children.Add(child); 
                }
            }
        }
        return children;
    }

    public void setAttacking()
    {
        attacking = true;
        withinAttack = new List<Cell>(BFS(selected.currentCell, selected.attackRange));
        setToColor(withinMove, CellGrid.ColorType.BLACK);
        setToColor(withinAttack, CellGrid.ColorType.RED);
    }
}
