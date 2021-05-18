using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScoreHolder : MonoBehaviour
{
    public static int totalKills = 0;
    public static int totalTime = 0;
    [SerializeField]
    private TMP_Text killText;
    [SerializeField]
    private TMP_Text timeText;
    // Update is called once per frame
    void Update()
    {
        killText.text = totalKills + " kills";
        timeText.text = totalTime + " seconds";
    }
}
