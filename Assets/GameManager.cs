using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.UIElements;
using System.Reflection;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private const string volumeKey = "Volume";
    public UnityEngine.UI.Button buttonLow;
    public UnityEngine.UI.Button Buttonhigh;
    public TMP_InputField inputField;
    public float defaultVolume = 0.9f; // уровень громкости по умолчанию
    public UnityEngine.UI.Image VolPointer;
    float mrr = 0;
    public GameObject FMG;
    public GameObject SMG;
    public GameObject TMG;
    public QTEminigame FMGscr;
    public MiniGameRightAc SMGscr;
    public ThirdMG TMGscr;
    public int AllScore=0;
    public TMP_Text ScoreText;
    public AudioSource audioSource;
    public TMP_Text MusikTimer;

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            float remainingTime = audioSource.clip.length - audioSource.time;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            string timeString = string.Format("{0}:{1:00}", minutes, seconds);
            Debug.Log("Осталось времени: " + timeString);
            MusikTimer.text = timeString;
        }
        else
        {
            Debug.Log("Воспроизведение завершено");
            Debug.Log("NEXTScene startueeem");
        }
    }
    public void refreshScore()
    {
        ScoreText.text = Convert.ToString(AllScore);
    } 
    private void Start()
    {
        // Загрузка уровня громкости из сохранений или установка значения по умолчанию
        float volume = PlayerPrefs.GetFloat(volumeKey, defaultVolume);
        SetVolume(volume);
    }
    public void stobbilSEX()
    {
        mrr = AudioListener.volume*100;
        inputField.text = mrr.ToString();
        coolbuttons();
    }

    public void ChoseLang(string SceneName)
    {
        mrr = 1;
        SceneManager.LoadScene(SceneName);
    }
    public void VolumeCheck()
    {
        int inputtext;
        if (inputField.text.Length == 0)
        {
            inputtext = 0;
        }
        else
        {
            inputtext = Convert.ToInt32(inputField.text);
            if (inputtext > 100)
            {
                inputtext = 100;
            }
        }
        inputField.text = inputtext.ToString();
        Debug.Log(inputtext);
        coolbuttons();
    }
    public void VolumeChange()
    {
        float inputtext = Convert.ToInt32(inputField.text);
        inputtext = inputtext / 100;
        Debug.Log(inputtext);
        SetVolume(Mathf.Clamp(AudioListener.volume = inputtext, 0f, 1f));
    }
    public void SetVolume(float volume)
    {
        // Установка уровня громкости для всех звуков
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(volumeKey, volume);
    }
    public void ButtonsVolume(int changeamount)
    {
        int meor = Convert.ToInt32(inputField.text);
        inputField.text=Convert.ToString(meor+changeamount);
        coolbuttons();
    }
    void TimerOfMusik()
    {

    }
    public void coolbuttons()
    {
        buttonLow.interactable = true;
        Buttonhigh.interactable = true;
        int meor = Convert.ToInt32(inputField.text);
        spinPoint(meor);
        if (meor == 10)
        {
            buttonLow.interactable = false;
        }
        else if (meor == 100)
        {
            Buttonhigh.interactable = false;
        }
        VolumeChange();
    }
    void spinPoint(int meor)
    {
        switch (meor)
        {
            case 10:
                VolPointer.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 20:
                VolPointer.transform.rotation = Quaternion.Euler(0, 0, -40);
                break;
            case 30:
                VolPointer.transform.rotation = Quaternion.Euler(0, 0, -83);
                break;
            case 40:
                VolPointer.transform.rotation = Quaternion.Euler(0, 0, -115);
                break;
            case 50:
                VolPointer.transform.rotation = Quaternion.Euler(0, 0, -144);
                break;
            case 60:
                VolPointer.transform.rotation = Quaternion.Euler(0, 0, -180);
                break;
            case 70:
                VolPointer.transform.rotation = Quaternion.Euler(0, 0, -215);
                break;
            case 80:
                VolPointer.transform.rotation = Quaternion.Euler(0, 0, -255);
                break;
            case 90:
                VolPointer.transform.rotation = Quaternion.Euler(0, 0, -290);
                break;
            case 100:
                VolPointer.transform.rotation = Quaternion.Euler(0, 0, -330);
                break;
        }
    }
    IEnumerator WaitForFiveSeconds()
    {
        Debug.Log("analsex");
        yield return new WaitForSeconds(5f);
        Debug.Log("analsex");
        int chooseminigame = UnityEngine.Random.Range(0, 3);
        switch (chooseminigame)
        {
            case 0:
                FMG.SetActive(true);
                FMGscr.nextbut = true;
                break;
            case 1:
                SMG.SetActive(true);
                break;
            case 2:
                TMG.SetActive(true);
                break;
        }
    }
    public void randomminigamic()
    {
        Debug.Log("Random momo");
        StartCoroutine(WaitForFiveSeconds());
    }
}