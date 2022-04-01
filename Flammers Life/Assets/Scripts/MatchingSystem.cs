using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchingSystem : MonoBehaviour
{
    [SerializeField] private List<Scenario> scenarios;

    [SerializeField] private GameObject modalScoreboard;

    [SerializeField] private float minBetProfit = 1f;
    [SerializeField] private float maxBetProfit = 10f;
    [SerializeField] private int profitMultiplier = 1;
    
    [SerializeField] private Text profitText;
    [SerializeField] private Text cheatText;
    [SerializeField] private Text[] texts;

    private string ourTeamScore = "", rivalTeamScore = "";

    private int betProfit;

    private int cheatCost = 0;

    //  Bütün betler.
    private List<BetScenario> betScenarios;

    //  Ekranda yazdırılan rastgele betler. Text sayısı kadar yazdırılabilir.
    private BetScenario[] chosenBetScenarios;

    public int BetProfit { get => betProfit; set => betProfit = value; }
    public string OurTeamScore { get => ourTeamScore; set => ourTeamScore = value; }
    public string RivalTeamScore { get => rivalTeamScore; set => rivalTeamScore = value; }
    public int CheatCost { get => cheatCost; set => cheatCost = value; }

    public struct BetScenario
    {
        private int id;
        private float profit;
        private string scenarioText;
        Func<int, int, bool> precept;

        public BetScenario(int id, float profit, string scenarioText, Func<int, int, bool> precept)
        {
            this.id = id;
            this.profit = profit;
            this.scenarioText = scenarioText;
            this.precept = precept;
        }

        public int Id { get => id; set => id = value; }
        public float Profit { get => profit; set => profit = value; }
        public string ScenarioText { get => scenarioText; set => scenarioText = value; }
        public Func<int, int, bool> Precept { get => precept; set => precept = value; }
        
    }

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        Check();
    }

    private void Initialize()
    {
        var w = GameObject.FindGameObjectWithTag("Profit").GetComponent<ProfitSystem>().Week;

        if (w == 8)
        {
            BetProfit = 75;
        }
        else if(w == 0)
        {
            SceneManager.LoadScene(5);
        }
        else
        {
            BetProfit = GameObject.FindGameObjectWithTag("Profit").GetComponent<ProfitSystem>().Profit;
        }
       
        betScenarios = CreateBetScenarios();
        chosenBetScenarios = ChooseBets(ShuffleBets(betScenarios), 3);
        PrintBetsOnScreen(chosenBetScenarios);

        PrintCheatCostOnScreen(CheatCost);

        PrintProfitOnScreen(BetProfit);
    }

    public void CalculateButtonClick()
    {
        CalculateProfit(chosenBetScenarios, OurTeamScore, RivalTeamScore);

        BetProfit -= CheatCost;

        GameObject.FindGameObjectWithTag("Profit").GetComponent<ProfitSystem>().Profit = BetProfit;

        PrintProfitOnScreen(BetProfit);

        if (BetProfit < 0)
        {
            FindObjectOfType<SimpleLoader>().LoadQuitScreen();
        }
        else
        {
            SceneManager.LoadScene(3);
        }

    }

    private List<Func<int, int, bool>> CreatePreceptListInListOrder()
    {
        List<Func<int, int, bool>> funcs = new List<Func<int, int, bool>>();

        //  x: bizim takım  y: rakip takım

        //  Bu maçta gol olmamalı. 
        Func<int, int, bool> func0 = (int x, int y) => (x == 0 && y == 0); funcs.Add(func0);
        //  Ne olursa olsun Bizim takım gol atmamalı. 
        Func<int, int, bool> func1 = (int x, int y) => (x == 0); funcs.Add(func1);
        //  Ne olursa olsun Rakip takım gol atmamalı.
        Func<int, int, bool> func2 = (int x, int y) => (y == 0); funcs.Add(func2);
        //  Bu maç kesinlikle Karşılıklı gollü bitmeli. 
        Func<int, int, bool> func3 = (int x, int y) => (x != 0 && y != 0); funcs.Add(func3);
        //  Bu maçta 1 farkla galip gelmeliyiz.
        Func<int, int, bool> func4 = (int x, int y) => ((x - y) == 1); funcs.Add(func4);
        //  Bu maçta 2 farkla galip gelmeliyiz. 
        Func<int, int, bool> func5 = (int x, int y) => ((x - y) == 2); funcs.Add(func5);
        //  Bu maçta 3 farkla galip gelmeliyiz.
        Func<int, int, bool> func6 = (int x, int y) => ((x - y) == 3); funcs.Add(func6);
        //  Bu maç berabere bitmeli.
        Func<int, int, bool> func7 = (int x, int y) => (x == y); funcs.Add(func7);
        //  Ne olursa olsun kaybetmeliyiz. 
        Func<int, int, bool> func8 = (int x, int y) => (x < y); funcs.Add(func8);
        //  Bu maçta 2 farkla kaybedersek...
        Func<int, int, bool> func9 = (int x, int y) => ((y - x) == 2); funcs.Add(func9);
        //  Bu maçta 3 farkla kaybedersek...
        Func<int, int, bool> func10 = (int x, int y) => ((y - x) == 3); funcs.Add(func10);
        //  Toplam Gol 2-3 olursa...
        Func<int, int, bool> func11 = (int x, int y) => ((x + y) >= 2 && (x + y) <= 3); funcs.Add(func11);
        //  Toplam Gol 4-6 olursa...
        Func<int, int, bool> func12 = (int x, int y) => ((x + y) >= 4 && (x + y) <= 6); funcs.Add(func12);
        //  Maç sonucu ne olursa olsun 3’ten fazla gol olmamalı.
        Func<int, int, bool> func13 = (int x, int y) => ((x + y) <= 3); funcs.Add(func13);

        return funcs;
    }

    private int GenerateProfit(int profitMultiplier)
    {
        return (int)UnityEngine.Random.Range(minBetProfit, maxBetProfit * profitMultiplier);
    }

    private List<BetScenario> CreateBetScenarios()
    {
        List<BetScenario> betScenarioPopulator = new List<BetScenario>();

        int counter = 0;

        foreach (var s in scenarios)
        {
            betScenarioPopulator.Add(new BetScenario(counter, GenerateProfit(profitMultiplier), s.ScenarioText, CreatePreceptListInListOrder()[counter]));
            counter++;
        }

        return betScenarioPopulator;
    }

    private List<BetScenario> ShuffleBets(List<BetScenario> bets)
    {
        var shuffledBets = bets.OrderBy(s => Guid.NewGuid()).ToList();

        return shuffledBets;
    }

    private BetScenario[] ChooseBets(List<BetScenario> bets, int numberOfBets)
    {
        List<BetScenario> chosenBets = new List<BetScenario>();

        for (int i = 0; i < numberOfBets; i++)
        {
            chosenBets.Add(bets[i]);
        }

        CalculateCheatCost(chosenBets.ToArray());

        return chosenBets.ToArray();
    }
    
    private void CalculateCheatCost(BetScenario[] costC)
    {
        foreach (var c in costC)
        {
            CheatCost += (int) c.Profit;
        }

        CheatCost = ((CheatCost * 2) / 3);
    }

    private void CalculateProfit(BetScenario[] betsToBeCalculated, string ourTeamScore, string rivalTeamScore)
    {
        int oTS = int.Parse(ourTeamScore);

        int rTS = int.Parse(rivalTeamScore);

        foreach (var bet in betsToBeCalculated)
        {
            if (bet.Precept(oTS, rTS))
            {
                BetProfit += (int)bet.Profit;
            }
        }
    }

    private void PrintBetsOnScreen(BetScenario[] betsToBePrinted)
    {
        for (int i = 0; i < betsToBePrinted.Length; i++)
        {
            texts[i].text = string.Format(betsToBePrinted[i].ScenarioText, betsToBePrinted[i].Profit);
        }
    }

    private void PrintProfitOnScreen(int profit)
    {
        string profitT = profit + "M TL";

        profitText.text = profitT;
    }

    private void PrintCheatCostOnScreen(int cheatCost)
    {
        string cheatT = "Bu şikenin bedeli: " + cheatCost + "M TL";

        cheatText.text = cheatT;
    }

    public void OpenAndCloseScoreboardButtonClick()
    {
        modalScoreboard.SetActive(!modalScoreboard.activeSelf);
    }

    public void Check()
    {
        if (BetProfit < 0)
        {
            FindObjectOfType<SimpleLoader>().LoadQuitScreen();
        }
    }
}
