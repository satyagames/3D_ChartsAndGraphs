using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using Newtonsoft.Json;
using Altux.Model;
using UnityEngine.Networking;
using Altux.Util;
using Altux.Model.Bar;


namespace Altux.Bar.Handler
{
    public class BarDataHandler : MonoBehaviour
    {
        BarChart barChart;
        public Material[] graphMaterial;
        void Start()
        {
           barChart = GetComponent<BarChart>();
           StartCoroutine(GetBarGraphData());
        }

        IEnumerator GetBarGraphData()
        {
            UnityWebRequest request = UnityWebRequest.Get(Constants.URL.BAR_DATA_URL);
            yield return request.SendWebRequest();

            string response = request.downloadHandler.text;
            BarModel root = JsonConvert.DeserializeObject<BarModel>(response);
           
            CreateBarGraph(root);
        }

        void CreateBarGraph(BarModel root)
        {
            barChart.DataSource.AutomaticMaxValue = true;
            barChart.DataSource.AutomaticMinValue = true;
            for (int i = 0; i < root.data.Count; i++)
            {
                if (!barChart.DataSource.HasCategory(root.data[i].x))
                    barChart.DataSource.AddCategory(root.data[i].x, graphMaterial[0]);
                if (!barChart.DataSource.HasGroup(root.data[i].z))
                    barChart.DataSource.AddGroup(root.data[i].z);
                barChart.DataSource.SetValue(root.data[i].x, root.data[i].z, root.data[i].y);
            }
            SetMaterial();
        }

        void SetMaterial()
        {
            int count = barChart.DataSource.TotalCategories;
            int mod = 0;
            for (int i = 0; i < count; i++)
            {
                if (mod >= graphMaterial.Length)
                    mod = 0;
                barChart.DataSource.SetMaterial(barChart.DataSource.GetCategoryName(i), graphMaterial[mod]);
                mod++;
            }
        }

        public void Refersh()
        {
            StartCoroutine(GetBarGraphData());
        }
    }
}
