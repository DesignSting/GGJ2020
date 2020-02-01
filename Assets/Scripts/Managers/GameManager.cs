using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public int woodResource;
    public int mudResource;
    public int berriesResource;

    public PlayerMovement player;

    [Space(20)]
    [SerializeField] private float timer;
    [SerializeField] private int diceRoll_01;
    [SerializeField] private int diceRoll_02;
    private bool firstRollGood;
    private bool secondRollGood;
    private bool newDay;

    [Space(15)]
    public Image countdownBG;
    public Image countdownFG;

    
    public void StartDay()
    {
        Debug.Log(diceRoll_01 + " : " + diceRoll_02);
        SceneManager.LoadScene("MainScene");
        FindObjectOfType<UIManager>().UpdateResources(woodResource, berriesResource, mudResource);
    }

    public void CollectResource(float timeToCollect, Resource res)
    {
        Vector3 toScreen = Camera.main.WorldToScreenPoint(res.transform.position);
        countdownBG.transform.position = toScreen;
        StartCoroutine(Collect(timeToCollect, res));
    }

    public void SetFirstDiceRoll(int i)
    {
        diceRoll_01 = i;
    }

    public int ReturnFirstDiceRoll()
    {
        return diceRoll_01;
    }

    public void SetSecondDiceRoll(int i)
    {
        diceRoll_02 = i;
    }

    public int ReturnSecondDiceRoll()
    {
        return diceRoll_02;
    }

    IEnumerator Collect(float f, Resource r)
    {
        Debug.Log("Collect");
        countdownBG.gameObject.SetActive(true);
        Debug.Log(countdownBG.isActiveAndEnabled);
        float timer = f;
        FindObjectOfType<PlayerMovement>().SetPlayerLocked(true);
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            countdownFG.fillAmount = timer / f;

            yield return null;
        }
        FindObjectOfType<PlayerMovement>().SetPlayerLocked(false);
        countdownBG.gameObject.SetActive(false);
        FindObjectOfType<UIManager>().UpdateResources(r);
        r.Harvested();
    }
}

public enum Weather
{
    Sun,
    Wet,
    Snow
}
