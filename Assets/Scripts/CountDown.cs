using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

    public Text command;
    public Commands countdown;

    private int intNum;
    // Start is called before the first frame update
    private void Start()
    {
        countdown = FindObjectOfType<Commands>();
    }

    // Update is called once per frame
    private void Update()
    {
        intNum = Mathf.RoundToInt(countdown.countdown);
        command.text = "" + intNum;
    }
}
