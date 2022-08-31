using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using Altux.Model.Bubble;
using UnityEngine.Networking;
using Altux.Util;
using Newtonsoft.Json;

namespace Altux.Bubble.Handler
{
    public class BubbleDataHandler : MonoBehaviour
    {
        GraphChartBase bubbleChart;
        //public WorldSpaceGraphChart bubbleChart;
        public GameObject PointPrefab;
        public Material[] graphMaterial;
        public Material other;
        int DepthOffset = 2;
        int totalCategory;

        void Start()
        {
            bubbleChart = GetComponent<GraphChartBase>();
            StartCoroutine(GetBarGraphData());
        }

        IEnumerator GetBarGraphData()
        {
            UnityWebRequest request = UnityWebRequest.Get(Constants.URL.BUBBLE_DATA_URL);
            yield return request.SendWebRequest();

            string response = request.downloadHandler.text;
            Debug.Log(response);
            BubbleModel root = JsonConvert.DeserializeObject<BubbleModel>(response);

            CreateBubbleGraph(root);
        }

        void CreateBubbleGraph(BubbleModel root)
        {
            int mod = 0;
            for (int i = 0; i < root.data.Count; i++)
            {
                MaterialTiling tiling = new MaterialTiling();

                if (!bubbleChart.DataSource.HasCategory(root.data[i].category))
                {
                    if (mod >= graphMaterial.Length)
                        mod = 0;
                    int depth = DepthOffset * i;
                    bubbleChart.DataSource.AddCategory3DGraph(root.data[i].category, null, graphMaterial[mod], 0.4f, tiling, null, other, false, PointPrefab, graphMaterial[mod], 1.11f, depth, false, 10);
                    mod++;
                }
                Debug.Log(root.data[i].category + " " + root.data[i].x);
                GraphData.AddPointToCategoryWithLabel(bubbleChart, root.data[i].category, root.data[i].x, root.data[i].y, root.data[i].size);
            }
        }
    }
}
