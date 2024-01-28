using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float totalTime = 60f; 
    float timeRemaining;
    TMP_Text textMesh;

    public static Action YouLostBitch;

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        timeRemaining = totalTime;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            // Handle timer completion here
            Debug.Log("Timer finished!");
            YouLostBitch?.Invoke();
        }

        // Update the text mesh
        UpdateTextMesh();
    }

    void UpdateTextMesh()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        textMesh.text = "TIMER " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}