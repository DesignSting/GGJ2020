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
    public TMP_InputField inputField_01;
    public TMP_InputField inputField_02;
    public Button startButton;
    public PlayerMovement player;

    [Space(20)]
    [SerializeField] private float timer;
    [SerializeField] private int diceRoll_01;
    [SerializeField] private int diceRoll_02;
    private bool firstRollGood;
    private bool secondRollGood;
    private bool newDay;
    public Image countdownBG;
    public Image countdownFG;



    public void CheckDiceInput(TMP_InputField inputField)
    {
        string s = inputField.text;
        int dice = int.Parse(s);
        if (dice > 0 && dice <= 20)
        {
            if(inputField == inputField_01)
            {
                diceRoll_01 = dice;
                firstRollGood = true;
                inputField_02.interactable = true;
                inputField_02.Select();
            }
            if(inputField == inputField_02)
            {
                diceRoll_02 = dice;
                secondRollGood = true;
                startButton.Select();
            }
            inputField.text = "SUBMITTED";
            inputField.interactable = false;

        }
        else
        {
            inputField.text = "INVALID";
        }
        CheckStartButton();
    }

    private void CheckStartButton()
    {
        if(firstRollGood && secondRollGood)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    public void StartDay()
    {
        Debug.Log(diceRoll_01 + " : " + diceRoll_02);
        SceneManager.LoadScene("MainScene_Test");
        FindObjectOfType<UIManager>().UpdateResources(woodResource, berriesResource, mudResource);
    }

    public void CollectResource(float timeToCollect, Resource res)
    {
        Vector3 toScreen = Camera.main.WorldToScreenPoint(res.transform.position);
        countdownBG.transform.position = toScreen;
        StartCoroutine(Collect(timeToCollect));
    }

    public int ReturnFirstDiceRoll()
    {
        return diceRoll_01;
    }

    public int ReturnSecondDiceRoll()
    {
        return diceRoll_02;
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

    private void Start()
    {
        CheckStartButton();
        inputField_01.Select();
        inputField_02.interactable = false;
    }
}

public enum Weather
{
    Sun,
    Wet,
    Snow
}
