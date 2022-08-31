using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using Newtonsoft.Json;
using Altux.Model;
using UnityEngine.Networking;
using Altux.Util;
using Altux.Model.Pie;

namespace Altux.Pie.Handler
{
    public class PieDataHandler : MonoBehaviour
    {
        PieChart pieChart;
        public Material[] graphMaterial;
        void Start()
        {
            pieChart = GetComponent<PieChart>();
            StartCoroutine(GetPieGraphData());
        }

        IEnumerator GetPieGraphData()
        {
            UnityWebRequest request = UnityWebRequest.Get(Constants.URL.PIE_DATA_URL);
            yield return request.SendWebRequest();

            string response = request.downloadHandler.text;
            List<PieModel> root = JsonConvert.DeserializeObject<List<PieModel>>(response);
            CreateBarGraph(root);
        }

        void CreateBarGraph(List<PieModel> root)
        {
            for (int i = 0; i < root.Count; i++)
            {
                if (!pieChart.DataSource.HasCategory(root[i].block))
                    pieChart.DataSource.AddCategory(root[i].block, graphMaterial[0]);
                pieChart.DataSource.SetValue(root[i].block, root[i].percent);
                SetMaterial();
            }
        }

        void SetMaterial()
        {
            int count = pieChart.DataSource.TotalCategories;
            int mod = 0;
            for (int i = 0; i < count; i++)
            {
                if (mod >= graphMaterial.Length)
                    mod = 0;
                pieChart.DataSource.SetMaterial(pieChart.DataSource.GetCategoryName(i), graphMaterial[mod]);
                mod++;
            }
        }
    }
}
