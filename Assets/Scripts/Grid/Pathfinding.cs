using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;


    //Pathfinding is a SINGLETON
    public static Pathfinding Instance { get; private set; }

    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    private Transform origin;

    

    public Pathfinding(int width, int height, Transform origin,bool debug = false )
    {
        Instance = this;
        this.origin = origin;
        grid = new Grid<PathNode>(width, height, 1, origin.position, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y), debug);
    }

    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {

        grid.GetXY(startWorldPosition, out int startX, out int startY); 
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);

        if(path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();

            foreach  (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() + Vector3.one * 0.5f + origin.position );
                //new Vector3(path[i].x, path[i].y) + Vector3.one * 0.5f + parent.position
            }

            return vectorPath;
        }

    }


    public List<PathNode> FindPath(int startX, int StartY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, StartY);
        PathNode endNode = grid.GetGridObject(endX, endY);


        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();


        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                //final node
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighboursList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;

                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);

                if(tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();


                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }

            }

        }

        //out of nodes
        return null;
    }

    private List<PathNode> GetNeighboursList(PathNode currentNode)
    {
        List<PathNode> neighboursList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            //left
            neighboursList.Add(GetNode(currentNode.x - 1, currentNode.y));
            //left down
            //if (currentNode.y - 1 >= 0) neighboursList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            //left up
            //if (currentNode.y + 1 < grid.GetHeight()) neighboursList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        
        if (currentNode.x + 1 < grid.GetWidth())
        {
            //right
            neighboursList.Add(GetNode(currentNode.x + 1, currentNode.y));
            //right down
            //if (currentNode.y - 1 >= 0) neighboursList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            //right up
            //if (currentNode.y + 1 < grid.GetHeight()) neighboursList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }

        //down
        if (currentNode.y - 1 >= 0) neighboursList.Add(GetNode(currentNode.x, currentNode.y - 1));
        
        //up
        if (currentNode.y + 1 < grid.GetHeight()) neighboursList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighboursList;

    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;

        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestCostFNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestCostFNode.fCost)
            {
                lowestCostFNode = pathNodeList[i];
            }
        }
        return lowestCostFNode;
    }

}
