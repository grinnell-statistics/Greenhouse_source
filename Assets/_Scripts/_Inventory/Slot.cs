using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int amount;
    private GameObject imageObj;
    private GameObject amountObj;
    public int slotNumber;
    public Sound invSound;
    private GameObject priceObj;
    private string price = "";

    // Start is called before the first frame update
    void Start()
    {
        imageObj = this.transform.GetChild(1).gameObject;
        amountObj = this.transform.GetChild(3).gameObject;
        priceObj = this.transform.GetChild(7).gameObject;
        imageObj.SetActive(false);

        if (this.slotNumber == 1)
        {
            amount = Global.seeds;
            imageObj.SetActive(true);
            price = "$" + Global.cornSeedPrice.ToString();
            priceObj.GetComponent<TextMeshProUGUI>().SetText(price);
        }

        Global.slotSelected = -1;
    }

    private void Update()
    {
        if (this.slotNumber == 1)
        {
            amount = Global.seeds;
            price = "$" + Global.cornSeedPrice.ToString();
        }
        else if (this.slotNumber == 2)
        {
            amount = Global.crops;
            price = "$" + Global.cornCropPrice.ToString();
        }

        if (this.slotNumber == 4)
        {
            amount = Global.seeds2;
            price = "$" + Global.beanSeedPrice.ToString();
        }
        else if (this.slotNumber == 5)
        {
            amount = Global.crops2;
            price = "$" + Global.beanCropPrice.ToString();
        }

        if (this.slotNumber == 6)
        {
            amount = Global.seeds3;
            price = "$" + Global.tomatoSeedPrice.ToString();
        }
        else if (this.slotNumber == 7)
        {
            amount = Global.crops3;
            price = "$" + Global.tomatoCropPrice.ToString();
        }

        //Introduce fertilizer at level 2
        if (Global.level > 1)
        {
            if (this.slotNumber == 8)
            {
                amount = Global.fertilizer;
                price = "$" + Global.fertilizerPrice.ToString();
            }
        }

        //Introduce pesticide at season 20
        //if (Global.level >= 4)
        //{
        //    if (this.slotNumber == 9)
        //    {
        //        amount = Global.pesticides;
        //        price = "$" + Global.pesticidePrice.ToString();
        //    }
        //}

        //Show image if there are more than 0 in inventory
        if (amount > 0)
        {
            imageObj.SetActive(true);
            if (this.slotNumber != 1 && this.slotNumber != 4 && this.slotNumber != 6 && this.slotNumber != 8)
            {
                amountObj.GetComponent<TextMeshProUGUI>().SetText(amount.ToString());
            }
            priceObj.GetComponent<TextMeshProUGUI>().SetText(price);
        }
        else
        {
            imageObj.SetActive(false);
            amountObj.GetComponent<TextMeshProUGUI>().SetText("");
            priceObj.GetComponent<TextMeshProUGUI>().SetText("");

            //Don't let the slot be selected
            this.transform.GetChild(4).gameObject.SetActive(false);
        }
    }

    public void Deselect()
    {
        this.transform.GetChild(4).gameObject.SetActive(false);
    }

    //Selection of a slot
    public void OnPointerDown(PointerEventData eventData)
    {
        //Play sound
        invSound.PlayInventorySound();

        //Deselect previously selected slot
        if (Global.slotSelected != this.slotNumber)
        {
            if (Global.slotSelected == 3)
            {
                this.transform.parent.GetChild(Global.slotSelected).GetComponent<Water>().Deselect();
            }
            else if (Global.slotSelected != -1)
            {
                this.transform.parent.GetChild(Global.slotSelected).GetComponent<Slot>().Deselect();
            }

            //Set this slot active
            if (this.amount > 0)
            {
                this.transform.GetChild(4).gameObject.SetActive(true);
                Global.slotSelected = this.slotNumber;
            }

        }

        //Deselect all slots
        else
        {
            this.Deselect();
            Global.slotSelected = -1;
        }

    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        if (this.slotNumber <= 8)
        {
            EnterHover();
        }
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


    private void EnterHover()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
        this.transform.GetChild(3).gameObject.SetActive(false);
        this.transform.GetChild(5).gameObject.SetActive(true);
        this.transform.GetChild(6).gameObject.SetActive(true);
        this.transform.GetChild(7).gameObject.SetActive(true);
    }

}
