using UnityEngine;

public class Global : MonoBehaviour
{
    //If the value of anything changes check the
    //DisplayWinInfo script and change it there 
    //as well

    //Starting Data
    public static int level = 1;
    public static int season = 1;
    public static int seeds = 0;
    public static int crops = 0;
    public static int gold = 1000;
    public static int slotSelected = -1;
    public static bool loadCore = false;
    public static bool muted = false;
    public static int spending = 0;
    public static int earning = 0;

    //New Crops
    public static int seeds2 = 0;
    public static int crops2 = 0;
    public static int seeds3 = 0;
    public static int crops3 = 0;

    //Prices
    public static int cornSeedPrice = 30;
    public static int beanSeedPrice = 25;
    public static int tomatoSeedPrice = 40;
    public static int waterPrice = 1;
    public static int cornCropPrice = 4;
    public static int beanCropPrice = 6;
    public static int tomatoCropPrice = 12;
    public static int fertilizerPrice = 10;
    public static int pesticidePrice = 0;

    //Upgrades
    public static int nitrateStart = 350;
    public static int water = 100000000;
    public static int fertilizer = 0;
    public static int pesticides = 0;

    //Data
    public static float[,] yielddata = new float[30, 40];
    public static int[,] wateradded = new int[30, 40];
    public static int[,] croptype = new int[30, 40];
    public static float[,] nitrate = new float[30, 40];
    public static int[,] nitrateadded = new int[30, 40];
    public static string data = "";

    //Current prices
    public static int currCornSeed = 10;
    public static int currBeanSeed = 7;
    public static int currCarrotSeed = 10;
    public static int currCornCrop = 5;
    public static int currBeanCrop = 10;
    public static int currCarrotCrop = 20;

    //Table
    public static bool[,] plots = new bool[30, 40];

    //Plot growth
    public static bool[] planted = new bool[40];
    public static bool[] harvest = { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
    public static bool[] grown = new bool[40];
    public static Models[] models = {new Models(1), new Models(2), new Models(3), new Models(4), new Models(5),
                                     new Models(6), new Models(7), new Models(8), new Models(9), new Models(10), new Models(11),
                                     new Models(12),new Models(13), new Models(14), new Models(15), new Models(16), new Models(17),
                                     new Models(18),new Models(19), new Models(20), new Models(21), new Models(22), new Models(23),
                                     new Models(24),new Models(25), new Models(26), new Models(27), new Models(28), new Models(29),
                                     new Models(30), new Models(31), new Models(32), new Models(33), new Models(34), new Models(35),
                                     new Models(36),new Models(37), new Models(38), new Models(39), new Models(40)};
    public static bool[] nitrateUpdated = new bool[40];

    //Player Settings
    public static string username = "";
    public static string password = "";

    //Misc
    public static int fertUsed = 0;
    public static bool[,] pestUsed = new bool[30, 40];
    public static bool[,] pests = new bool[30, 40];

    //False if inactive or not claimed, true if claimed
    public static bool newGame = false;
    public static bool growed = false;
    public static bool harvested = false;
    public static bool finishGrow = false;
    public static bool finishCurrSeason = false;
    public static bool growClicked = false;

    public static int backbuttonPlaceHolder = 1;

    // Level 4 data
    public static int[] waterAmounts = { -1, -1, -1, -1, -1, -1 };
    public static int cropTypes = 0;
    public static int plotNeeded = 0;
    public static int repetition = 0;
    public static int waterLength = 0;

    // Undo Button
    public static bool remove = false;

}
