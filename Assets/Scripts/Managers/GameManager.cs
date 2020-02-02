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
        SetUpWeather();
        DontDestroyOnLoad(gameObject);
    }

    public PlayerMovement player;
    public int totalDays;
    public int currentDay;
    public List<Weather> dailyWeather = new List<Weather>();

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

    [Space(15)]
    private bool harvesting;
    public List<AudioClip> woodChomps = new List<AudioClip>();
    public List<AudioClip> mudSlops = new List<AudioClip>();
    public List<AudioClip> bushRushel = new List<AudioClip>();
    private AudioSource audioSource;
    private Queue<AudioClip> inQueue = new Queue<AudioClip>();


    private bool endedDay;

    public void EndDay()
    {
        if (harvesting)
        {
            endedDay = true;
        }
        else
        {
            SceneManager.LoadScene("RollTheDiceScene");
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.SetPlayerLocked(true);
            FindObjectOfType<UIManager>().EndRound();
            currentDay++;
        }
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

    public void SetUpHarvestSounds(float timeTaken, List<AudioClip> clips)
    {
        float totalTime = 0.0f;
        while (totalTime < timeTaken)
        {
            int rand = Random.Range(0, clips.Count);
            totalTime += clips[rand].length;
            inQueue.Enqueue(clips[rand]);
        }
        Debug.Log(totalTime);
        PlaySoundQueue();
    }

    public void SetUpWeather()
    {
        List<Weather> tempList = new List<Weather>();
        tempList.Add(Weather.Sun);
        tempList.Add(Weather.Sun);
        tempList.Add(Weather.Sun);
        tempList.Add(Weather.Sun);
        tempList.Add(Weather.Wet);
        tempList.Add(Weather.Wet);
        tempList.Add(Weather.Snow);
        while(dailyWeather.Count < 7)
        {
            int i = Random.Range(0, tempList.Count);
            dailyWeather.Add(tempList[i]);
            tempList.Remove(tempList[i]);
        }
    }

    public Weather ReturnWeather()
    {
        return dailyWeather[currentDay];
    }

    private void PlaySoundQueue()
    {
        audioSource.clip = inQueue.Dequeue();
        audioSource.Play();
    }

    IEnumerator Collect(float f, Resource r)
    {
        countdownBG.gameObject.SetActive(true);
        Debug.Log(countdownBG.isActiveAndEnabled);
        float timer = f;
        FindObjectOfType<PlayerMovement>().SetPlayerLocked(true);
        harvesting = true;

        List<AudioClip> harvestSoundList = new List<AudioClip>();
        switch (r.resource)
        {
            case TypeResource.Wood:
                harvestSoundList = woodChomps;
                break;
            case TypeResource.Berries:
                harvestSoundList = bushRushel;
                break;
            case TypeResource.Mud:
                harvestSoundList = mudSlops;
                break;
        }
        SetUpHarvestSounds(f, harvestSoundList);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            countdownFG.fillAmount = timer / f;
            if(!audioSource.isPlaying)
            {
                PlaySoundQueue();
            }

            yield return null;
        }
        FindObjectOfType<PlayerMovement>().SetPlayerLocked(false);
        countdownBG.gameObject.SetActive(false);
        FindObjectOfType<UIManager>().UpdateResources(r);
        r.Harvested();
        harvesting = false;
        if(inQueue.Count > 0)
        {
            inQueue.Clear();
        }
    }

    private void Update()
    {
        if(endedDay)
        {
            if(!harvesting)
            {
                endedDay = false;
                EndDay();
            }
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}

public enum Weather
{
    Sun,
    Wet,
    Snow
}
