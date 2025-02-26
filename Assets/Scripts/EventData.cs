using UnityEngine;

[CreateAssetMenu(fileName = "NewEvent", menuName = "Event")]
public class EventData : ScriptableObject
{
    public string eventText;
    public string choiceLeft;
    public string choiceRight;
    public string bearer;
    
    public int shareholdersChangeLeft, employeesChangeLeft, moneyChangeLeft, customersChangeLeft;
    public int shareholdersChangeRight, employeesChangeRight, moneyChangeRight, customersChangeRight;
}
