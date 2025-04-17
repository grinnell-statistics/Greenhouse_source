using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;

public class Load : MonoBehaviour
{
    // When continue to next season within one level
    public void LoadData()
    {
        // Same var
        Global.season++;
        Global.spending = 0;
        Global.earning = 0;

        Global.growed = false;
        Global.harvested = false;
        Global.finishGrow = false;
        Global.loadCore = true;
        Global.remove = false;
        Global.finishCurrSeason = false;
        Global.growClicked = false;


        for (int i = 0; i < 6; i++)
        {
            Global.waterAmounts[i] = -1;
        }
        Global.cropTypes = 0;
        Global.plotNeeded = 0;
        Global.repetition = 0;
        Global.waterLength = 0;

        // Reset nitrate
        if (Global.level == 2)
        {
            // nitrate updates to start value every season
            for (int i = 0; i < 40; i++)
            {
                Global.nitrate[Global.season - 1, i] = Global.nitrateStart;
                Global.models[i].SetNitrateLevel(Global.nitrateStart);
            }
        }
        else if (Global.level == 3)
        {
            // nitrate continue to next season
            for (int i = 0; i < 40; i++)
            {
                Global.nitrate[Global.season - 1, i] = Global.models[i].GetNitrateLevel();
            }
        }
        else if (Global.level == 4)
        {
            // nitrate updates to start value every season
            for (int i = 0; i < 40; i++)
            {
                Global.nitrate[Global.season - 1, i] = Global.nitrateStart;
                Global.models[i].SetNitrateLevel(Global.nitrateStart);
            }
        }

        if (Global.level < 4)
        {
            SceneManager.LoadScene("Farm");
        }
        else
        {
            SceneManager.LoadScene("Selection");
        }

    }

    public void ResetVar()
    {
        Global.newGame = true;
        for (int i = 0; i < 40; i++)
        {
            Global.croptype[Global.season - 1, i] = 0;
            Global.planted[i] = false;
        }
        if (string.Compare(EventSystem.current.currentSelectedGameObject.name, "Level (1)") == 0)
        {
            upgradeLevel(1);
        }
        else if (string.Compare(EventSystem.current.currentSelectedGameObject.name, "Level (2)") == 0)
        {
            upgradeLevel(2);
        }
        else if (string.Compare(EventSystem.current.currentSelectedGameObject.name, "Level (3)") == 0)
        {
            upgradeLevel(3);
        }
        else if (string.Compare(EventSystem.current.currentSelectedGameObject.name, "Level (4)") == 0)
        {
            upgradeLevel(4);
        }

    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Farm");
    }

    // To differentiate when going back from graph scene
    public void Back()
    {
        if (Global.backbuttonPlaceHolder == 1)
        {
            SceneManager.LoadScene("Farm");
        }
        else
        {
            SceneManager.LoadScene("LevelComplete");
        }
    }

    public void FarmGraphs()
    {
        Global.backbuttonPlaceHolder = 1;
        SceneManager.LoadScene("Scatterplot");
    }

    public void CompleteGraphs()
    {
        Global.backbuttonPlaceHolder = 2;
        SceneManager.LoadScene("Scatterplot");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void SelectionScene()
    {
        SceneManager.LoadScene("Selection");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Data()
    {
        OpenLinkJSPlugin("https://www.stat2games.sites.grinnell.edu/data/greenhouse/greenhouse.php");
    }

    public void Resources()
    {
        OpenLinkJSPlugin("https://stat2labs.sites.grinnell.edu/farmer.html");
    }

    public void Tutorialvideo()
    {
        OpenLinkJSPlugin("https://youtu.be/uIc2_2b4lAE");
    }

    private void OpenLinkJSPlugin(string url)
    {
#if !UNITY_EDITOR
            openWindow(url);
#endif
    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);

    public void upgradeLevel(int level)
    {
        // Same var
        Global.season = 1;
        Global.earning = 0;
        Global.spending = 0;
        Global.slotSelected = -1;
        Global.fertUsed = 0;
        Global.pesticides = 0;

        Global.crops = 0;
        Global.crops2 = 0;
        Global.crops3 = 0;
        Global.cornSeedPrice = 30;
        Global.beanSeedPrice = 25;
        Global.tomatoSeedPrice = 40;
        Global.cornCropPrice = 4;
        Global.beanCropPrice = 6;
        Global.tomatoCropPrice = 12;
        Global.waterPrice = 1;

        Global.growed = false;
        Global.harvested = false;
        Global.loadCore = false;
        Global.muted = false;
        Global.remove = false;
        Global.finishCurrSeason = false;

        Global.wateradded = new int[30, 40];
        Global.nitrateadded = new int[30, 40];
        Global.croptype = new int[30, 40];
        Global.nitrate = new float[30, 40];
        Global.plots = new bool[30, 40];
        Global.planted = new bool[40];
        Global.harvest = new bool[40];
        Global.grown = new bool[40];
        Global.nitrateUpdated = new bool[40];
        Global.yielddata = new float[30, 40];

        for (int i = 0; i < 40; i++)
        {
            Global.harvest[i] = true;
        }

        Global.models = new Models[40];
        for (int i = 0; i < 40; i++)
        {
            Global.models[i] = new Models(i + 1);
        }

        // Different var
        switch (level)
        {
            case 1:
                Global.level = 1;
                Global.gold = 1000;

                Global.seeds = 40;
                Global.seeds2 = 40;
                Global.seeds3 = 40;

                Global.nitrateStart = 350;
                Global.fertilizer = 0;

                for (int i = 0; i < 40; i++)
                {
                    Global.nitrate[0, i] = Global.nitrateStart;
                }
                break;
            case 2:
                Global.level = 2;
                Global.gold = 1000;

                Global.seeds = 40;
                Global.seeds2 = 40;
                Global.seeds3 = 40;

                Global.nitrateStart = 200;
                Global.fertilizer = 10;

                for (int i = 0; i < 40; i++)
                {
                    Global.nitrate[0, i] = Global.nitrateStart;
                    Global.models[i].SetNitrateLevel(Global.nitrateStart);
                }
                break;
            case 3:
                Global.level = 3;
                Global.gold = 1000;

                Global.seeds = 40;
                Global.seeds2 = 40;
                Global.seeds3 = 40;

                Global.nitrateStart = 200;
                Global.fertilizer = 10;

                for (int i = 0; i < 40; i++)
                {
                    Global.nitrate[0, i] = Global.nitrateStart;
                    Global.models[i].SetNitrateLevel(Global.nitrateStart);
                }
                break;
            case 4:
                Global.level = 4;
                Global.gold = 1000;

                Global.seeds = 0;
                Global.seeds2 = 0;
                Global.seeds3 = 0;

                Global.fertilizer = 0;
                Global.nitrateStart = 350;

                for (int i = 0; i < 40; i++)
                {
                    Global.nitrate[0, i] = Global.nitrateStart;
                    Global.models[i].SetNitrateLevel(Global.nitrateStart);
                }

                for (int i = 0; i < 6; i++)
                {
                    Global.waterAmounts[i] = -1;
                }

                Global.cropTypes = 0;
                Global.plotNeeded = 0;
                Global.repetition = 0;
                Global.waterLength = 0;
                break;
            case 5:
                break;

        }
    }
}
