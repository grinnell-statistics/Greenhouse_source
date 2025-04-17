using UnityEngine;
using UnityEngine.UI;

public class ToggleSelect : MonoBehaviour
{
    // Level 4 Selection Scene
    public Toggle corn;
    public Toggle bean;
    public Toggle tomato;

    // Start is called before the first frame update
    private void Start()
    {
        corn.isOn = false;
        bean.isOn = false;
        tomato.isOn = false;
    }

    private void Update()
    {
        if (corn.isOn)
        {
            if (bean.isOn)
            {
                if (tomato.isOn)
                {
                    Global.cropTypes = 3;
                }
                else
                {
                    Global.cropTypes = 2;
                }
            }
            else
            {
                if (tomato.isOn)
                {
                    Global.cropTypes = 2;
                }
                else
                {
                    Global.cropTypes = 1;
                }
            }
        }
        else
        {
            if (bean.isOn)
            {
                if (tomato.isOn)
                {
                    Global.cropTypes = 2;
                }
                else
                {
                    Global.cropTypes = 1;
                }
            }
            else
            {
                if (tomato.isOn)
                {
                    Global.cropTypes = 1;
                }
                else
                {
                    Global.cropTypes = 0;
                }
            }
        }
    }

    public void CheckCorn()
    {
        if (corn.isOn)
        {
            Global.seeds = 1;
        }
        else
        {
            Global.seeds = 0;
        }

    }

    public void CheckBean()
    {
        if (bean.isOn)
        {
            Global.seeds2 = 1;
        }
        else
        {
            Global.seeds2 = 0;
        }

    }

    public void CheckTomato()
    {
        if (tomato.isOn)
        {
            Global.seeds3 = 1;
        }
        else
        {
            Global.seeds3 = 0;
        }

    }


}
