using UnityEngine;

[CreateAssetMenu(fileName = "Scenario")]
public class Scenario : ScriptableObject
{
    [SerializeField] [TextArea(10, 100)] private string scenarioText;
    public string ScenarioText { get => scenarioText; set => scenarioText = value; }

}
