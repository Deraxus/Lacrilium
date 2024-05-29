using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TMP_Text timer;
    public TMP_Text coins;
    public TMP_Text hp;
    public TMP_Text maxhp;
    public TMP_Text itemMessage;

    public GameObject playerObject;
    public GameObject timerObject;

    public float messageDuration = 3f;
    public float messageFadeDelay = 1f;

    public string currentItemMessage;

    private int publicCoins;
    private float publicTimer;

    private float seconds;
    private float seconds2;

    private bool togl = false;
    private bool isShowingMessage = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        publicTimer = timerObject.GetComponent<Timer>().timer;
        float round_timer = Mathf.Round(publicTimer);
        timer.text = round_timer.ToString();
        coins.text = playerObject.GetComponent<Player>().coins.ToString();
        hp.text = $"{playerObject.GetComponent<Player>().HP} / {playerObject.GetComponent<Player>().MaxHP}";
        if (isShowingMessage == true) {
            ChangeTextSlowlyVoid(itemMessage, currentItemMessage, messageFadeDelay, messageDuration);
        }
    }

    public IEnumerator ChangeTextSlowly(TMP_Text text, string message, float delay) {
        text.color = Color32.Lerp(new Color32(0, 0, 0, 0), new Color32(255, 255, 255, 255), 3000000000);
        yield return new WaitForSeconds(delay);
    }

    public void StartNewMessage(string message) {
        itemMessage.color = new Color(itemMessage.color.r, itemMessage.color.g, itemMessage.color.b, 0);
        seconds = 0;
        seconds2 = 0;
        togl = false;
        isShowingMessage = true;
        currentItemMessage = message;
    }

    public void ChangeTextSlowlyVoid(TMP_Text text, string message, float delay, float duration) {
        text.text = message;
        if (isShowingMessage == false) {
            return;
        }
        if (((seconds / delay) <= 1) && (togl == false))
        {
            seconds += Time.deltaTime;
            itemMessage.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(1, 1, 1, 1), seconds / delay);
        }
        else {
            togl = true;
            seconds2 += Time.deltaTime;
            if (seconds2 >= duration) {
                seconds -= Time.deltaTime;
                itemMessage.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(1, 1, 1, 1), seconds / delay);
            }
        }
        if (seconds2 >= (delay * 2 + duration)) {
            togl = false;
            seconds = 0;
            seconds2 = 0;
            text.text = string.Empty;
            isShowingMessage = false;
        }

    }
}
