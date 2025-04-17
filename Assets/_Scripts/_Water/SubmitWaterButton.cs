using UnityEngine;
using UnityEngine.UI;

public class SubmitWaterButton : MonoBehaviour
{

    public GameObject amountInput;
    public GameObject waterSlot;
    public Sound sound;
    private int amount;
    public GameObject waterDisplay;
    public GameObject irrDisplay;
    public GameObject gameController;
    //Each index represents if a plot is selected or n
    public Farm[] plots;

    private void Update()
    {
        amountInput.GetComponent<InputField>().ActivateInputField();
    }

    public void GetWater()
    {
        amount = amountInput.GetComponent<WaterInput>().GetAmount();

        if (amount < 0)
        {
        }
        else
        {
            for (int i = 0; i < gameController.GetComponent<Multiselect>().plotsSelected.Length; i++)
            {
                //Water plot if it's selected
                if (gameController.GetComponent<Multiselect>().plotsSelected[i])
                {
                    plots[i].model.AddWater(amount);
                    Global.gold -= amount * Global.waterPrice;
                    Global.spending += amount * Global.waterPrice;
                    Global.wateradded[Global.season - 1, i] += amount;
                    waterDisplay.GetComponent<Display>().DisplayResource((amount * GetNumSelected()).ToString());
                }
            }
            //Reset amount to 0
            amountInput.GetComponent<WaterInput>().Reset();

            //Set all plots watered to prevWatered;
            for (int i = 1; i < 41; i++)
            {
                if (gameController.GetComponent<Multiselect>().plotsSelected[i - 1])
                {
                    gameController.GetComponent<Multiselect>().PrevSelect(i);
                    gameController.GetComponent<Multiselect>().plotsSelected[i - 1] = false;
                }
            }
            this.transform.parent.gameObject.SetActive(false);

        }
    }


    public int GetNumSelected()
    {
        int counter = 0;
        for (int i = 0; i < gameController.GetComponent<Multiselect>().plotsSelected.Length; i++)
        {
            if (gameController.GetComponent<Multiselect>().plotsSelected[i])
            {
                counter++;
            }
        }
        return counter;
    }
}
