using UnityEngine;
using UnityEngine.UI;

public class WaterInput : MonoBehaviour
{
    private InputField input;
    private string amount;

    void Start()
    {
        input = gameObject.GetComponent<InputField>();
        input.characterValidation = InputField.CharacterValidation.Integer;
        input.onEndEdit.AddListener(SubmitAmount);
    }

    private void SubmitAmount(string arg)
    {
        this.amount = arg;
    }

    public int GetAmount()
    {
        return int.Parse(amount);
    }

    public void Reset()
    {
        amount = "0";
        input.SetTextWithoutNotify("");
    }


}
