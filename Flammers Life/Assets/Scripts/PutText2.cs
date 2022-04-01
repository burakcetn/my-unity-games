using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutText2 : MonoBehaviour
{
    [SerializeField] private Text text1;

    private void Start()
    {
        PopulateText();
    }

    private void PopulateText()
    {
        int p = GameObject.FindGameObjectWithTag("Profit").GetComponent<ProfitSystem>().Profit;

        text1.text = "" + p;
    }
}
