using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfitSystem : MonoBehaviour
{
    [SerializeField] private int profit;
    [SerializeField] private int week;

    private int profitSystemCount;

    private GameObject myGame;

    public int Profit { get => profit; set => profit = value; }
    public int Week { get => week; set => week = value; }

    private void Awake()
    {  
        profitSystemCount = FindObjectsOfType<ProfitSystem>().Length;
        
        if (profitSystemCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Week = 8;

        myGame = GameObject.FindGameObjectWithTag("System");
    }

    private void FixedUpdate()
    {
        
    }
}
