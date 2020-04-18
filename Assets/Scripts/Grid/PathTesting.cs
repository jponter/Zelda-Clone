using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class PathTesting : MonoBehaviour
{
    private Pathfinding  pathfinding;

    [SerializeField] private PathFindingVisual pathfindingVisual;
    [SerializeField] private PathLog pathLog;

    public bool debug;
    public Transform parent;
    public int gridX;
    public int gridY;
    Grid<PathNode> grid;
    public LayerMask layerMask;
    public Transform target; //player

    public PathLog PathEnemy;


    private void Start()
    {
         pathfinding = new Pathfinding(gridX, gridY, parent, debug);
        pathfindingVisual.SetGrid(pathfinding.GetGrid());

        grid = pathfinding.GetGrid();

        Debug.Log("0,0 is at " + grid.GetWorldPosition(0, 0));


        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 pos = grid.GetWorldPosition(x, y);
               
                pos.x = pos.x + 0.5f;
                pos.y = pos.y + 0.5f;
               

                Collider2D collider = Physics2D.OverlapBox(pos, new Vector2(0.9f,0.9f), 0, layerMask);


               
                if (collider)
                {
                    
                    pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered grid killzone!");
            PathEnemy.Activate();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited grid killzone!");
            PathEnemy.DeActivate();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition()  ;
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            Debug.Log("Mouse down x: " + x + " y: " + y);



            //List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            pathfinding.GetGrid().GetXY(target.position, out int posX, out int posY);
            List<PathNode> path = pathfinding.FindPath(posX, posY, x, y);



            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    //Debug.DrawLine(new Vector3(path[i].x , path[i].y) + parent.position * 1f + Vector3.one, new Vector3(path[i + 1].x , path[i + 1].y ) + parent.position * 1f + Vector3.one, Color.green);
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) + Vector3.one * 0.5f - parent.position, new Vector3(path[i + 1].x, path[i + 1].y) + Vector3.one * 0.5f - parent.position, Color.red, 5f, false);

                }
            }
            pathLog.SetTargetPosition(mouseWorldPosition);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x,y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);


        }
    }
}
