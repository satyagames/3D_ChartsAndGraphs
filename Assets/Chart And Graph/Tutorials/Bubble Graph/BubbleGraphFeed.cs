#define Graph_And_Chart_PRO
using UnityEngine;

namespace ChartAndGraph
{
    class BubbleGraphFeed : MonoBehaviour
    {
        string[] items = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
        public Material graphMaterial;
        void Start()
        {

            GraphChartBase graph = GetComponent<GraphChartBase>();
            if (graph != null)
            {

                //      graph.DataSource.StartBatch();
                MaterialTiling tiling = new MaterialTiling();
                tiling.EnableTiling = true;
                //graph.DataSource.AddCategory("Player 1", graphMaterial, 1, tiling, graphMaterial, false, graphMaterial, 10); 
               //graph.DataSource.ClearCategory("Player 2");
                for (int i = 0; i < 10; i++)
                {
                    GraphData.AddPointToCategoryWithLabel(graph, "Player 1", 2005, Random.value * 20f, 2500);
                    GraphData.AddPointToCategoryWithLabel(graph, "Player 2", 2006, Random.value * 50f, 1000);
                    //graph.DataSource.AddPointToCategory("Player 1", Random.value * 10f, Random.value * 10f, Random.value *3f);
                    //   graph.DataSource.AddPointToCategory("Player 2", Random.value * 10f, Random.value * 10f, Random.value *3f);
                }
        //        graph.DataSource.EndBatch();
            }
        }

    }
}
