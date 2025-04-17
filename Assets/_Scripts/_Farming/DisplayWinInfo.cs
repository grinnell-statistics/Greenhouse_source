using TMPro;
using UnityEngine;

public class DisplayWinInfo : MonoBehaviour
{
    public GameObject expenses;
    public GameObject income;
    public GameObject profit;
    public GameObject funds;
    public GameObject header;
    public GameObject message;
    public GameObject startover;
    public GameObject menu;
    public GameObject continuePlaying;

    public void Update()
    {
        int profitNum = Global.earning - Global.spending;
        // Lose
        if (profitNum < 0 && Global.gold < 0)
        {
            header.GetComponent<TextMeshProUGUI>().SetText("Oops! Sorry...");
            message.GetComponent<TextMeshProUGUI>().SetText("You didn't make enough to continue!");
            continuePlaying.SetActive(false);
            startover.SetActive(true);
            menu.SetActive(false);
        }

        // Lose this season
        else if (profitNum < 0 && Global.gold > 0)
        {
            header.GetComponent<TextMeshProUGUI>().SetText("You lost money in this season...");
            message.GetComponent<TextMeshProUGUI>().SetText("but you have enough have to continue!");
            continuePlaying.SetActive(true);
            menu.SetActive(true);
            startover.SetActive(false);
        }

        // Win
        else
        {
            header.GetComponent<TextMeshProUGUI>().SetText("Congratulations!");
            message.GetComponent<TextMeshProUGUI>().SetText("You've made profit in this season!");
            continuePlaying.SetActive(true);
            menu.SetActive(true);
            startover.SetActive(false);
        }
        expenses.GetComponent<TextMeshProUGUI>().SetText("Expenses: $" + Global.spending.ToString());
        income.GetComponent<TextMeshProUGUI>().SetText("Income: $" + Global.earning.ToString());
        profit.GetComponent<TextMeshProUGUI>().SetText("Profit: $" + profitNum.ToString());
        funds.GetComponent<TextMeshProUGUI>().SetText("Current Funds: $" + Global.gold.ToString());
    }

}