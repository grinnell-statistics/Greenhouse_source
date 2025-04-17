using UnityEngine;

public class Models
{
    private int totalWater;
    public int nitrateLevel;
    private bool pest;

    //---- Constructor ------- //
    public Models(int plot)
    {
        this.totalWater = 0;
        this.nitrateLevel = 0;
        this.pest = false;

    }

    // ---- Water ----- //
    public int GetTotalWater()
    {
        return this.totalWater;
    }

    //public bool GetPest()
    //{
    //    return this.pest;
    //}

    public void AddWater(int waterAdded)
    {
        this.totalWater += waterAdded;
    }

    public void RemoveWater()
    {
        this.totalWater = 0;
    }



    //-------------------Nitrate--------------

    //Automatically change nitrate based on what was planted before
    public void UpdateNitrate(int plot)
    {
        //if previous crop was corn
        if (Global.croptype[Global.season - 1, plot - 1] == 1)
        {
            this.nitrateLevel = Mathf.Max(0, this.nitrateLevel - 25);
        }
        //if previous crop was beans
        else if (Global.croptype[Global.season - 1, plot - 1] == 2 && this.nitrateLevel < 450)
        {
            this.nitrateLevel += 10;
        }
        //if previous crop was tomatos
        else if (Global.croptype[Global.season - 1, plot - 1] == 3)
        {
            this.nitrateLevel = Mathf.Max(0, this.nitrateLevel - 15);
        }
        Global.nitrate[Global.season - 1, plot - 1] = this.nitrateLevel;
    }

    //Increase nitrate levels with fertilizer
    public void AddFertilizer(int plot)
    {
        this.nitrateLevel += 50;
        Global.nitrate[Global.season - 1, plot - 1] = this.nitrateLevel;
    }

    //Get nitrate level
    public int GetNitrateLevel()
    {
        return this.nitrateLevel;
    }

    //Set nitrate level
    public void SetNitrateLevel(int input)
    {
        this.nitrateLevel = input;
    }


    //-------------Pests-------------------------

    //public bool SetPest(int plot)
    //{

    //    if (Global.season < 20)
    //    {
    //        this.pest = false;
    //    }
    //    else
    //    {
    //        int pestChance = 0;
    //        int rand = Random.Range(0, 100);
    //        //if current is the same as past three seasons
    //        if (Global.croptype[Global.season - 1, plot - 1] == Global.croptype[Global.season - 2, plot - 1] && Global.croptype[Global.season - 1, plot - 1] == Global.croptype[Global.season - 4, plot - 1] && Global.croptype[Global.season - 3, plot - 1] == Global.croptype[Global.season - 1, plot - 1])
    //        {
    //            pestChance = 50;
    //        }
    //        //if current is the same as past two seasons
    //        else if (Global.croptype[Global.season - 1, plot - 1] == Global.croptype[Global.season - 3, plot - 1] && Global.croptype[Global.season - 2, plot - 1] == Global.croptype[Global.season - 1, plot - 1])
    //        {
    //            pestChance = 25;
    //        }
    //        //if current is the same as last season
    //        else if ( Global.croptype[Global.season - 2, plot - 1] == Global.croptype[Global.season - 1, plot - 1])
    //        {
    //            pestChance = 15;
    //        }
    //        //not the same 
    //        else
    //        {
    //            pestChance = 10;
    //        }

    //        //If pesticide was used on this plot
    //        if (this.pesticides)
    //        {
    //            pest = false;
    //            Debug.Log("pesticides " + plot);
    //        }
    //        else
    //        {
    //            if (rand < pestChance)
    //            {
    //                pest = true;
    //                Global.pests[Global.season - 1, plot - 1] = true;
    //            }
    //            else
    //            {
    //                pest = false;
    //            }
    //        }
    //    }
    //    return pest;      
    //}

    //public void AddPesticides(int plot)
    //{
    //    this.pesticides = true;
    //    Global.pestUsed[Global.season - 1, plot - 1] = true;
    //}


    //---------------Yield Models----------------------

    //Model for corn
    public float GetYield1()
    {
        float fertP = Mathf.Min(this.GetNitrateLevel() / 400f, 1f);
        float yield = 0f;
        float error = GetError();
        if (this.totalWater <= 12)
        {
            float calc = (0.05f * this.totalWater * fertP + 0.04f * this.totalWater * this.totalWater * fertP) + error;
            yield = Mathf.Max(calc, 0);
        }
        else if (this.totalWater <= 50)
        {
            float calc = (-18f * fertP + 2.5f * this.totalWater * fertP - 0.04f * this.totalWater * this.totalWater * fertP) + error;
            yield = Mathf.Max(calc, 0);
        }
        else if (this.totalWater <= 65)
        {
            float calc = (161.9f * fertP - 5.09f * this.totalWater * fertP + 0.04f * this.totalWater * this.totalWater * fertP) + error;
        }
        else
        {
            yield = 0;
        }

        if (this.pest)
        {
            yield *= .8f;
        }
        //pesticides = false;
        this.totalWater = 0;
        return yield;
    }

    // Model for beans
    public float GetYield2()
    {
        float fertP = Mathf.Min(this.GetNitrateLevel() / 200f, 1f);
        float yield = 0;
        float error = GetError();

        if (this.totalWater <= 6)
        {
            float calc = (-0.0621f + 0.2787f * this.totalWater + 0.065f * fertP - 0.0225f * this.totalWater * this.totalWater
                              + 0.1191f * this.totalWater * fertP + 0.00504f * this.totalWater * this.totalWater * this.totalWater
                              + 0.03032f * this.totalWater * this.totalWater * fertP) + error;
            yield = Mathf.Max(calc, 0);
        }
        else if (this.totalWater <= 30)
        {
            float calc = (-1.13f + 0.601f * this.totalWater - 1.13f * fertP - 0.01668f * this.totalWater * this.totalWater
                             + 0.601f * this.totalWater * fertP - 0.01669f * this.totalWater * this.totalWater * fertP) + error;
            yield = Mathf.Max(calc, 0);
        }
        else
        {
            float calc = (7.1897f + 7.1897f * fertP - 0.2303f * this.totalWater + 0.0019f * this.totalWater * this.totalWater
                             - 0.2303f * this.totalWater * fertP + 0.0019f * this.totalWater * this.totalWater * fertP) + error;
            yield = Mathf.Max(calc, 0);
        }

        if (this.pest)
        {
            yield *= .8f;
        }
        //pesticides = false;
        this.totalWater = 0;
        return yield;
    }

    // Third type of crop
    public float GetYield3()
    {
        float fertP = Mathf.Min(this.GetNitrateLevel() / 250f, 1f);
        float yield = 0;
        float error = GetError();

        if (this.totalWater < 20)
        {
            yield = 0;
        }
        else if (this.totalWater <= 34)
        {
            float calc = (float)(7.36037 - 0.72742 * this.totalWater + 6.35922 * fertP + 0.01936 * this.totalWater * this.totalWater
                 - 0.61262 * this.totalWater * fertP - 0.000053 * this.totalWater * this.totalWater * this.totalWater
                 + 0.015065 * this.totalWater * this.totalWater * fertP);
            calc += error;
            yield = Mathf.Max(calc, 0);

        }
        else if (this.totalWater <= 57)
        {
            float calc = (float)(-38.3834 + 1.9531 * this.totalWater - 38.3834 * fertP - 0.0217 * this.totalWater * this.totalWater
                + 1.9531 * this.totalWater * fertP - 0.0217 * this.totalWater * this.totalWater * fertP);
            calc += error;
            Debug.Log("water " + this.totalWater);
            Debug.Log("fertilizer " + fertP);
            yield = Mathf.Max(calc, 0);
        }
        else
        {
            float calc = (float)(1389.44428 - 66.642183 * this.totalWater + 148.789368 * fertP + 1.0702973 * this.totalWater * this.totalWater
                            - 4.5288676 * this.totalWater * fertP - 0.005755 * this.totalWater * this.totalWater * this.totalWater
                             + 0.034404 * this.totalWater * this.totalWater * fertP);
            calc += error;
            yield = Mathf.Max(calc, 0);
        }

        if (this.pest)
        {
            yield *= .8f;
        }
        //pesticides = false;
        this.totalWater = 0;
        return yield;
    }


    private float GetError()
    {
        float rand = Random.Range(0f, 0.5f);
        float error = 0;

        if (rand <= 0.0987)
        {
            error = Random.Range(0f, 0.25f);
        }
        else if (rand <= 0.1915)
        {
            error = Random.Range(0.25f, 0.5f);
        }
        else if (rand <= 0.2734)
        {
            error = Random.Range(0.5f, 0.75f);
        }
        else if (rand <= 0.3413)
        {
            error = Random.Range(0.75f, 1f);
        }
        else if (rand <= 0.3944)
        {
            error = Random.Range(1f, 1.25f);
        }
        else if (rand <= 0.4332)
        {
            error = Random.Range(1.25f, 1.5f);
        }
        else if (rand <= 0.4599)
        {
            error = Random.Range(1.5f, 1.75f);
        }
        else if (rand <= 0.4772)
        {
            error = Random.Range(1.75f, 2.0f);
        }
        else if (rand <= 0.4878)
        {
            error = Random.Range(2.0f, 2.25f);
        }
        else if (rand <= 0.4938)
        {
            error = Random.Range(2.25f, 2.5f);
        }
        else if (rand <= 0.497)
        {
            error = Random.Range(2.5f, 2.75f);
        }
        else if (rand <= 0.4987)
        {
            error = Random.Range(2.75f, 3.0f);
        }
        else if (rand <= 0.4994)
        {
            error = Random.Range(3.0f, 3.25f);
        }
        else
        {
            error = Random.Range(3.25f, 3.5f);
        }

        float sign = Random.Range(0f, 1f);
        if (sign < 0.5)
        {
            return error * -1;
        }
        else
        {
            return error;
        }
    }


}
