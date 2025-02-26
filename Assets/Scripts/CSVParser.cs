using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public static class CSVParser
{
    public static List<EventData> LoadEventsFromCSV(string fileName)
    {
        List<EventData> eventsList = new List<EventData>();

        // Load the CSV file from Resources
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);
        if (csvFile == null)
        {
            Debug.LogError($"❌ CSV file {fileName} not found in Resources folder!");
            return eventsList;
        }

        // Read all lines
        string[] lines = csvFile.text.Split('\n');
        if (lines.Length <= 1) return eventsList; // Skip if empty

        for (int i = 1; i < lines.Length; i++) // Skip header row
        {
            string[] values = SplitCsvLine(lines[i]); // Use regex split

            if (values.Length < 13) continue; // Ensure row has enough values

            EventData newEvent = ScriptableObject.CreateInstance<EventData>();

            newEvent.eventText = values[1].Trim();
            newEvent.choiceLeft = values[2].Trim();
            newEvent.choiceRight = values[3].Trim();
            newEvent.bearer = values[4].Trim();

            try
            {
                newEvent.shareholdersChangeLeft = int.Parse(values[5]);
                newEvent.employeesChangeLeft = int.Parse(values[6]);
                newEvent.moneyChangeLeft = int.Parse(values[7]);
                newEvent.customersChangeLeft = int.Parse(values[8]);

                newEvent.shareholdersChangeRight = int.Parse(values[9]);
                newEvent.employeesChangeRight = int.Parse(values[10]);
                newEvent.moneyChangeRight = int.Parse(values[11]);
                newEvent.customersChangeRight = int.Parse(values[12]);
            }
            catch (System.FormatException ex)
            {
                Debug.LogError($"❌ Error parsing values for event: {newEvent.eventText}");
                Debug.LogError($"Row: {i} Values: {string.Join(", ", values)}");
                Debug.LogError(ex.Message);
                continue;
            }

            eventsList.Add(newEvent);
        }

        Debug.Log($"✅ Loaded {eventsList.Count} events from CSV.");
        return eventsList;
    }

    // Proper CSV splitting function that handles quoted values
    private static string[] SplitCsvLine(string line)
    {
        return Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"); // Handles commas inside quotes
    }
}
