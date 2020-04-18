using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private Grid<HeatMapGridObject> grid;
    private Rect gridBounds;
    public LayerMask boxcastMask;
    RaycastHit2D[] raycastHits;
    public bool showDebug;

    [SerializeField] private HeatMapVisual heatMapVisual;
    [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
    [SerializeField] private HeatMapGenericVisual heatMapGenericVisual;


    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<HeatMapGridObject>(20, 20, 1f, parent.position, (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y), showDebug);

        heatMapGenericVisual.SetGrid(grid);

        /*
        //manually set this for now
        gridBounds = new Rect(parent.position, new Vector2(10, 10));

        //i want a list of all colliders in the gridBounds
        raycastHits = Physics2D.BoxCastAll(parent.position, new Vector2(10, 10),0,Vector2.right,boxcastMask);
        Debug.Log("Raycast Complete hits = " +raycastHits.Length.ToString());

        if(raycastHits.Length > 0)
        {
            for (int i = 0; i < raycastHits.Length; i++)
            {
                Transform temp = raycastHits[i].transform;

                Debug.DrawLine(temp.position, new Vector3(temp.position.x + 32, temp.position.y +32, temp.position.z ),Color.red);
                Debug.Log("Line " + temp.position + " ");
            }
        }
        */
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            //int value = grid.GetValue(position);

            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);

            if (heatMapGridObject != null)
            {
                heatMapGridObject.AddValue(5);
            }
            //grid.AddValue(position,10,2, 5);
            //grid.SetValue(position, true);
        }



    }
}

    public class HeatMapGridObject
    {
        private const int MIN = 0;
        private const int MAX = 100;

        private Grid<HeatMapGridObject> grid;
        private int x;
        private int y;
        private int value;

        public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void AddValue(int addValue)
        {
            value += addValue;
            value = Mathf.Clamp(value, MIN, MAX);
            grid.TriggerGridObjectChanged(x, y);
        }

        public float GetValueNormalized()
        {
            return (float)value / MAX;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
