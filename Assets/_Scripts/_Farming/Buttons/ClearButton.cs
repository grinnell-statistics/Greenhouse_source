using UnityEngine;

public class ClearButton : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject bar;
    public GameObject clear2;
    public GameObject waterSelector;


    private void Start()
    {
        if (Global.level != 4 && !Global.growed && !Global.harvested)
        {
            this.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (Global.growed)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void SetRemove()
    {
        Global.remove = true;
        button1.SetActive(false);
        button2.SetActive(false);
        waterSelector.SetActive(false);
        button3.SetActive(false);
        bar.SetActive(false);
        clear2.SetActive(true);
        this.gameObject.SetActive(false);
    }
}