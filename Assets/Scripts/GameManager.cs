using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int shareholders = 50, employees = 50, money = 50, customers = 50;

    public RectTransform shareholdersFill, employeesFill, moneyFill, customersFill;

    private float maxBarHeight = 24f; // Set this to the actual height of your bar (in pixels)
    public float barOffset = -12f; // Offset value

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Set initial position offset
        SetInitialPositionOffset();
        UpdateUI(); // Ensure bars are initialized with correct values
    }

    private void SetInitialPositionOffset()
    {
        // Apply the offset to the y position of the fill bars to ensure they start from the bottom
        shareholdersFill.anchoredPosition = new Vector2(shareholdersFill.anchoredPosition.x, barOffset);
        employeesFill.anchoredPosition = new Vector2(employeesFill.anchoredPosition.x, barOffset);
        moneyFill.anchoredPosition = new Vector2(moneyFill.anchoredPosition.x, barOffset);
        customersFill.anchoredPosition = new Vector2(customersFill.anchoredPosition.x, barOffset);
    }

    public void UpdateMetrics(int shareholdersChange, int employeesChange, int moneyChange, int customersChange)
    {
        shareholders = Mathf.Clamp(shareholders + shareholdersChange, 0, 100);
        employees = Mathf.Clamp(employees + employeesChange, 0, 100);
        money = Mathf.Clamp(money + moneyChange, 0, 100);
        customers = Mathf.Clamp(customers + customersChange, 0, 100);

        UpdateUI();
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI...");

        if (shareholdersFill == null || employeesFill == null || moneyFill == null || customersFill == null)
        {
            Debug.LogError("❌ One or more fill bars are not assigned in the Inspector!");
            return;
        }

        // Animate fill bars smoothly
        StartCoroutine(SmoothBarChange(shareholdersFill, shareholders));
        StartCoroutine(SmoothBarChange(employeesFill, employees));
        StartCoroutine(SmoothBarChange(moneyFill, money));
        StartCoroutine(SmoothBarChange(customersFill, customers));

        Debug.Log($"✅ New Values - Shareholders: {shareholders}, Employees: {employees}, Money: {money}, Customers: {customers}");
    }

    IEnumerator SmoothBarChange(RectTransform bar, float targetValue)
    {
        float duration = 0.3f; // Smooth transition time
        float startHeight = bar.rect.height;
        float targetHeight = (targetValue / 100f) * maxBarHeight;
        float time = 0;

        // Smoothly animate the height of the bar
        while (time < duration)
        {
            bar.sizeDelta = new Vector2(bar.sizeDelta.x, Mathf.Lerp(startHeight, targetHeight, time / duration));
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure the height snaps to the final value at the end
        bar.sizeDelta = new Vector2(bar.sizeDelta.x, targetHeight);
    }
}
