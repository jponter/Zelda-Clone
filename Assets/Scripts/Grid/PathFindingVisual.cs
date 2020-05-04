using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingVisual : MonoBehaviour
{

    private Grid<PathNode> grid;
    private Mesh mesh;
    private bool updateMesh;

    public bool useMesh = false;

    private Vector2 gridValueUV;
     

    // Start is called before the first frame update
    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }



    public void SetGrid(Grid<PathNode> grid)
    {
        this.grid = grid;
        UpdateVisual();

        grid.OnGridObjectChanged += Grid_OnGridValueChanged;


    }

    private void Grid_OnGridValueChanged(object sender, Grid<PathNode>.OnGridObjectChangedEventArgs e)
    {
        updateMesh = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (useMesh)
        {
            if (updateMesh)
            {
                updateMesh = false;
                UpdateVisual();
            }
        }
    }

    private void UpdateVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        gridValueUV = new Vector2(0f, 0f);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                PathNode pathNode = grid.GetGridObject(x, y);

                if (pathNode.isWalkable)
                {
                    quadSize = Vector3.zero;
                }

                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * 0.5f, 0f, quadSize, gridValueUV, gridValueUV);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }

    public void OnDrawGizmos()
    {
        
        float cellSize = 0;


        if (grid != null)
        {
            cellSize = grid.GetCellSize();
        }

        if (grid != null)
        {

            Vector3 quadSize = new Vector3(1, 1) * cellSize;

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    PathNode pathNode = grid.GetGridObject(x, y);
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireCube(grid.GetWorldPosition(x, y) + (quadSize * 0.5f), new Vector3(cellSize, cellSize));

                    if (!pathNode.isWalkable)
                    {
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawSphere(grid.GetWorldPosition(x, y) + (quadSize * 0.5f), 0.2f);


                    }
                }

            }
        }
    }
}
