using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class Water : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int amount;
    private GameObject imageObj;
    public int slotIndex;
    public Sound invSound;
    public GameObject water;
    public Multiselect multi;
    private GameObject priceObj;
    private string price;


    // Start is called before the first frame update
    void Start()
    {
        imageObj = this.transform.GetChild(1).gameObject;
        priceObj = this.transform.GetChild(7).gameObject;
    }

    private void Update()
    {
        imageObj.SetActive(true);
        price = "$" + Global.waterPrice.ToString();
        priceObj.GetComponent<TextMeshProUGUI>().SetText(price);

    }

    //Deselect all slots
    public void Deselect()
    {
        this.transform.GetChild(4).gameObject.SetActive(false);
        water.SetActive(false);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Selected");
        foreach (GameObject obj in objs)
        {
            obj.SetActive(false);
        }

        for (int i = 0; i < 40; i++)
        {
            multi.plotsSelected[i] = false;
        }
    }

    //Selection of a slot
    public void OnPointerDown(PointerEventData eventData)
    {
        invSound.PlayInventorySound();
        if (Global.slotSelected != this.slotIndex)
        {
            if (Global.slotSelected != -1)
            {
                this.transform.parent.GetChild(Global.slotSelected).GetComponent<Slot>().Deselect();
            }

            this.transform.GetChild(4).gameObject.SetActive(true);
            Global.slotSelected = this.slotIndex;
        }
        else
        {
            this.transform.GetChild(4).gameObject.SetActive(false);
            Global.slotSelected = -1;
        }
    }

    //Hover
    public void OnPointerEnter(PointerEventData eventdata)
    {

        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
        this.transform.GetChild(3).gameObject.SetActive(false);
        this.transform.GetChild(5).gameObject.SetActive(true);
        this.transform.GetChild(6).gameObject.SetActive(true);
        this.transform.GetChild(7).gameObject.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventdata)
    {

        this.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(2).gameObject.SetActive(true);
        this.transform.GetChild(3).gameObject.SetActive(true);
        this.transform.GetChild(5).gameObject.SetActive(false);
        this.transform.GetChild(6).gameObject.SetActive(false);
        this.transform.GetChild(7).gameObject.SetActive(false);


    }
}

