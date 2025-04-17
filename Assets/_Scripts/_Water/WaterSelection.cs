using UnityEngine;

public class WaterSelection : MonoBehaviour
{
    // link to Water Selector
    public void AddWater()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
