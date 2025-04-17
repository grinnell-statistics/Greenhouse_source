using UnityEngine;
using TMPro;

public class WaterAmounts : MonoBehaviour
{
    //Level 4 slelection scene
    public GameObject amount1;
    public GameObject amount2;
    public GameObject amount3;
    public GameObject amount4;
    public GameObject amount5;
    public GameObject amount6;

    public void Amount1Input()
    {
        if (amount1.GetComponent<TMP_InputField>().text != "")
        {
            Global.waterAmounts[0] = int.Parse(amount1.GetComponent<TMP_InputField>().text);
        }
        else
        {
            Global.waterAmounts[0] = -1;
        }
    }
    public void Amount2Input()
    {
        if (amount2.GetComponent<TMP_InputField>().text != "")
        {
            Global.waterAmounts[1] = int.Parse(amount2.GetComponent<TMP_InputField>().text);
        }
        else
        {
            Global.waterAmounts[1] = -1;
        }
    }
    public void Amount3Input()
    {
        if (amount3.GetComponent<TMP_InputField>().text != "")
        {
            Global.waterAmounts[2] = int.Parse(amount3.GetComponent<TMP_InputField>().text);
        }
        else
        {
            Global.waterAmounts[2] = -1;
        }
    }
    public void Amount4Input()
    {
        if (amount4.GetComponent<TMP_InputField>().text != "")
        {
            Global.waterAmounts[3] = int.Parse(amount4.GetComponent<TMP_InputField>().text);
        }
        else
        {
            Global.waterAmounts[3] = -1;
        }
    }
    public void Amount5Input()
    {
        if (amount5.GetComponent<TMP_InputField>().text != "")
        {
            Global.waterAmounts[4] = int.Parse(amount5.GetComponent<TMP_InputField>().text);
        }
        else
        {
            Global.waterAmounts[4] = -1;
        }
    }
    public void Amount6Input()
    {
        if (amount6.GetComponent<TMP_InputField>().text != "")
        {
            Global.waterAmounts[5] = int.Parse(amount6.GetComponent<TMP_InputField>().text);
        }
        else
        {
            Global.waterAmounts[5] = -1;
        }
    }


}
