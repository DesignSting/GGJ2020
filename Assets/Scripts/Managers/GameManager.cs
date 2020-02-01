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
            Destroy(this);
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
    public TMP_InputField inputField;
    public PlayerMovement player;

    [Space(20)]
    [SerializeField] private float timer;
    [SerializeField] private int diceRoll;
    private bool newDay;
    public Image countdownBG;
    public Image countdownFG;



    public void CheckDiceInput(TMP_InputField inputField)
    {
        string s = inputField.text;
        int dice = int.Parse(s);
        if (dice > 0 && dice <= 20)
        {
            diceRoll = dice;
            StartDay(dice);
        }
        else
        {
            inputField.Select();
            inputField.text = "INVALID NUMBER";
        }
    }

    private void StartDay(int diceRoll)
    {
        player.gameObject.SetActive(true);
        SceneManager.LoadScene("MainScene_Testing");
        FindObjectOfType<UIManager>().UpdateResources(woodResource, berriesResource, mudResource);
    }

    public void CollectResource(float timeToCollect, Resource res)
    {
        Vector3 toScreen = Camera.main.WorldToScreenPoint(res.transform.position);
        countdownBG.transform.position = toScreen;
        StartCoroutine(Collect(timeToCollect));
    }

    public int ReturnDiceRoll()
    {
        return diceRoll;
    }

    IEnumerator Collect(float f)
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

    }

}

public enum Weather
{
    Sun,
    Wet,
    Snow
}
