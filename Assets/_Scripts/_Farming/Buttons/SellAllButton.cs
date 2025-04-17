using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SellAllButton : MonoBehaviour
{
    public GameObject loading;

    private void Start()
    {
        if (!Global.harvested)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SellCrops()
    {
        Global.earning = Global.crops * Global.cornCropPrice + Global.crops2 * Global.beanCropPrice + Global.crops3 * Global.tomatoCropPrice;
        Global.gold += Global.earning;
        Global.crops = 0;
        Global.crops2 = 0;
        Global.crops3 = 0;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(4);
        loading.SetActive(false);
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("LevelComplete");
    }

}
