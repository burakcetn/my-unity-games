using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutText : MonoBehaviour
{
    [SerializeField] private Text text1;
    [SerializeField] private Text text2;

    private void Start()
    {
        PopulateText();
    }

    private void PopulateText()
    {
        int w = GameObject.FindGameObjectWithTag("Profit").GetComponent<ProfitSystem>().Week;
        int p = GameObject.FindGameObjectWithTag("Profit").GetComponent<ProfitSystem>().Profit;

        text1.text = "Mevcut Kar: " + p;
        text2.text = "Kalan Hafta: " + w;
    }
}
