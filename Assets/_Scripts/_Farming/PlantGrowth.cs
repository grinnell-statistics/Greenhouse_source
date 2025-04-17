using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public GameObject pests;

    public void GrowPlant(Farm plot, int num)
    {
        if (Global.croptype[Global.season - 1, num - 1] == 1)
        {
            StartCoroutine(plot.Grow(1));
        }
        else if (Global.croptype[Global.season - 1, num - 1] == 2)
        {
            StartCoroutine(plot.Grow(2));
        }
        else
        {
            StartCoroutine(plot.Grow(3));
        }
    }
}
