using UnityEngine;

public class GrowButton : MonoBehaviour
{
    public Farm[] plots;
    public GameObject gameController;
    public GameObject waterSelector;
    public GameObject harvestButton;
    public GameObject clear;

    private void Start()
    {
        int planted = 0;

        for (int i = 0; i < Global.planted.Length; i++)
        {
            if (Global.planted[i])
            {
                planted++;
            }
        }
        if (planted == 0)
        {
            this.gameObject.SetActive(false);
            clear.SetActive(false);
        }
    }

    public void GrowCrops()
    {
        Global.growClicked = true;
        if (!Global.growed)
        {
            waterSelector.SetActive(false);
            for (int i = 0; i < plots.Length; i++)
            {
                if (Global.planted[i])
                {
                    gameController.GetComponent<PlantGrowth>().GrowPlant(plots[i], i + 1);
                }
            }
            this.gameObject.SetActive(false);
        }
    }
}
