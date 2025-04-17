using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{

    public int rowNum;
    public TMP_Text funds;
    public TMP_Text season;
    public TMP_Text playerID;

    private static string[] data;
    private static bool hasReturned;
    // Start is called before the first frame update
    void Start()
    {
        hasReturned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasReturned)
        {
            funds.text = data[3 * (rowNum - 1)+2];
            season.text = data[3 * (rowNum - 1)+1];
            playerID.text = data[3 * (rowNum - 1)];
        }
    }

    public static IEnumerator GetLeaderboard(string worth)
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerID", Global.username);
        form.AddField("Season", Global.season);
        form.AddField("Funds", worth);
        string url = "https://stat2games.sites.grinnell.edu/php/createfarmerleaderboardrecord.php";
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        if(www.downloadHandler.text!="0")
        {
            Debug.Log("Create leaderboard slot failed");
            Debug.Log(www.downloadHandler.text);
            yield break;
        }
        url = "https://stat2games.sites.grinnell.edu/php/getfarmerleaderboard.php";
        www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        data = www.downloadHandler.text.Split(new char[] { ',' }, System.StringSplitOptions.None);
        if(data.Length<15)
        {
            Debug.Log("Getting leaderboard failed");
            Debug.Log(www.downloadHandler.text);
            yield break;
        }
        hasReturned = true;
    }
}
