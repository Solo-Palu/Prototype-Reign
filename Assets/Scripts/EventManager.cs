using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public TextMeshProUGUI eventText;
    public Button choiceLeftButton, choiceRightButton;
    public TextMeshProUGUI choiceLeftText, choiceRightText;
    public TextMeshProUGUI bearerText;

    private List<EventData> eventPool;
    private EventData currentEvent;

    private void Start()
    {
        eventPool = CSVParser.LoadEventsFromCSV("prototype-reign-events"); // Filename without .csv
        if (eventPool.Count == 0)
        {
            Debug.LogError("❌ No events loaded!");
            return;
        }

        LoadRandomEvent();
    }

    void LoadRandomEvent()
    {
        currentEvent = eventPool[Random.Range(0, eventPool.Count)];
        eventText.text = currentEvent.eventText;
        bearerText.text = currentEvent.bearer;
        choiceLeftText.text = currentEvent.choiceLeft;
        choiceRightText.text = currentEvent.choiceRight;
    }

    public void SelectChoice(bool leftChoice)
    {
        if (leftChoice)
        {
            GameManager.Instance.UpdateMetrics(
                currentEvent.shareholdersChangeLeft,
                currentEvent.employeesChangeLeft,
                currentEvent.moneyChangeLeft,
                currentEvent.customersChangeLeft
            );
        }
        else
        {
            GameManager.Instance.UpdateMetrics(
                currentEvent.shareholdersChangeRight,
                currentEvent.employeesChangeRight,
                currentEvent.moneyChangeRight,
                currentEvent.customersChangeRight
            );
        }

        LoadRandomEvent();
    }
}
