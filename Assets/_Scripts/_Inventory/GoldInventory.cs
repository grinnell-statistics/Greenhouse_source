using UnityEngine;
using TMPro;

public class GoldInventory : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().SetText(Global.gold.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().SetText(Global.gold.ToString());
    }
}
