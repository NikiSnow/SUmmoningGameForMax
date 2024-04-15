using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class MiniGameRightAc : MonoBehaviour
{
    public UnityEngine.UI.Image timerBar;
    public float maxTime = 5f;
    public float timeLeft = 5f;
    System.Random rnd = new System.Random();
    private List<KeyCode> AllKeys = new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D,KeyCode.Q, KeyCode.E, KeyCode.Z, KeyCode.X, KeyCode.C,
    KeyCode.O, KeyCode.P, KeyCode.I,KeyCode.U,KeyCode.H,KeyCode.J,KeyCode.K,KeyCode.L,KeyCode.B,KeyCode.N,KeyCode.M};
    public TMP_Text firstL; //letter
    public TMP_Text secondL;
    public TMP_Text thirdL;
    public List<KeyCode> LevelKeys;
    public int LevelKeysCount;
    private bool isCheckingQTE = true;
    public GameManager GMscr;
    void Update()
    {
        TimerScript();
        if (isCheckingQTE)
        {
            CheckQTE();
        }
        if (Input.GetKey(LevelKeys[0]))
        {
            firstL.color = Color.green; // Если зажата, меняем цвет на зеленый
        }
        else
        {
            firstL.color = Color.white; // Если не зажата, возвращаем цвет по умолчанию
        }
        if (Input.GetKey(LevelKeys[1]))
        {
            secondL.color = Color.green; // Если зажата, меняем цвет на зеленый
        }
        else
        {
            secondL.color = Color.white; // Если не зажата, возвращаем цвет по умолчанию
        }
        if (Input.GetKey(LevelKeys[2]))
        {
            thirdL.color = Color.green; // Если зажата, меняем цвет на зеленый
        }
        else
        {
            thirdL.color = Color.white; // Если не зажата, возвращаем цвет по умолчанию
        }
    }
    public void OnEnable()
    {
        CreateListOfKeys();
        timeLeft = maxTime;
        firstL.color = Color.white;
        secondL.color = Color.white;
        thirdL.color = Color.white;
        isCheckingQTE = true;
    }
    void TimerScript()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            GMscr.SMG.SetActive(false);
            GMscr.randomminigamic();
        }
    }
    public void CreateListOfKeys()
    {
        LevelKeys = new List<KeyCode>();

        while (LevelKeys.Count < LevelKeysCount)
        {
            KeyCode newKey=(AllKeys[rnd.Next(0, 20)]);
            if (!LevelKeys.Contains(newKey))
            {
                LevelKeys.Add(newKey);
            }
        }
        ShowKeys();
    }
    void ShowKeys()
    {
        firstL.text=Convert.ToString(LevelKeys[0]);
        secondL.text = Convert.ToString(LevelKeys[1]);
        thirdL.text= Convert.ToString(LevelKeys[2]);
    }
    void CheckQTE()
    {
        if (thirdL.color == Color.green & secondL.color == Color.green & firstL.color == Color.green)
        {
            Debug.Log("QTE успешно пройден!");
            GMscr.AllScore =GMscr.AllScore + (1000 * (((int)timeLeft) + 1));
            GMscr.refreshScore();
            isCheckingQTE = false;
            GMscr.SMG.SetActive(false);
            GMscr.randomminigamic();
        }
    }
}
