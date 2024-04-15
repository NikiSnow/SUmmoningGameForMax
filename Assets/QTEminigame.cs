using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class QTEminigame : MonoBehaviour
{
    public int PointerScore = 5000;
    public GameObject ButtonE;
    public GameObject ButtonQ;
    public GameObject ButtonW;
    public GameObject Pointer;
    public bool nextbut = true;
    public GameManager GMscr;
    private void Update()
    {
        if (nextbut == true)
        {
            buttonsSys();
        }
    }
    public void buttonsSys()
    {
        Debug.Log("button system started");
        nextbut = false;
        ButtonE.SetActive(false);
        ButtonQ.SetActive(false);
        ButtonW.SetActive(false);
        int R= UnityEngine.Random.Range(0, 4);
        if (R == 0)
        {
            Butsys2(ButtonE,"E");
        }
        else if (R == 1)
        {
            Butsys2(ButtonQ,"Q");
        }
        else
        {
            Butsys2(ButtonW,"W");
        }
    }
    public void Butsys2(GameObject but, string expectedKey)
    {
        but.SetActive(true);
        StartCoroutine(WaitForKeyPress(expectedKey));
    }
    IEnumerator WaitForKeyPress(string expectedKey)
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                string keyPressed = Input.inputString.ToUpper();
                if (keyPressed == expectedKey)
                {
                    Debug.Log("Правильная клавиша!");
                    GMscr.AllScore = GMscr.AllScore+PointerScore;
                    GMscr.refreshScore();
                    nextbut = false;
                    GMscr.FMG.SetActive(false);
                    GMscr.randomminigamic();
                }
                else
                {
                    Debug.Log("Неправильная клавиша!");
                    nextbut = false;
                    GMscr.FMG.SetActive(false);
                    GMscr.randomminigamic();
                }
                break;
            }
            yield return null;
        }
    }
}
