using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ThirdMG : MonoBehaviour
{
    public UnityEngine.UI.Image timerBar;
    public KeyCode CurrentKey;
    public float maxTime = 10f;
    public float timeLeft = 10f;
    System.Random rnd = new System.Random();
    private List<KeyCode> AllKeys = new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D,KeyCode.UpArrow, KeyCode.DownArrow,KeyCode.RightArrow,
    KeyCode.LeftArrow};
    public List<UnityEngine.UI.Image> KeysObjectsImages = new List<UnityEngine.UI.Image>();
    public List<GameObject> KeysObjects;
    public List<KeyCode> LevelKeys;
    List<KeyCode> indexses;
    public int LevelKeysCount;
    private int currentIndex = 0;
    List<string> prefixList = new List<string>();
    private bool ShouldKeyCheck = true;
    Dictionary<KeyCode, KeyData> KeyPadDict;
    KeyCode keyCode1 = KeyCode.W;
    public GameManager GMscr;
    private void OnEnable()
    {
        prefixList = new List<string>();
        CreateListOfKeys();
        currentIndex = 0;
        foreach (UnityEngine.UI.Image image in KeysObjectsImages)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        }
        timeLeft = maxTime;

    }
    void Update()
    {
        if (ShouldKeyCheck)
        {
            keyHandler();
        }
        else
        {
            if (Input.GetKeyUp(keyCode1))
            {
                Debug.Log("Meow");
                ShouldKeyCheck = true;
            }
        }
        TimerScript();
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
            GMscr.TMG.SetActive(false);
            GMscr.randomminigamic();
        }
    }
    public void CreateListOfKeys()
    {
        KeyPadDict = new Dictionary<KeyCode, KeyData>();
        LevelKeys = new List<KeyCode>();

        List<int> randomIndices = GenerateRandomIndices(0, 7);
        for (int i = 0; i < randomIndices.Count; i++)
        {
            KeyCode key = (KeyCode)(i + (int)KeyCode.Alpha0);
            UnityEngine.UI.Image keyImage = KeysObjectsImages[randomIndices[i]];
            KeyPadDict.Add(key, new KeyData(keyImage, randomIndices[i]));
        }
        ShowKeys();
    }

    void ShowKeys()
    {
        foreach (GameObject placeholder in KeysObjects)
        {
            // Получаем индекс клавиши для данного плейсхолдера
            int index = GetIndexForKeyCode(placeholder.name);
            if (index != -1 && KeyPadDict.ContainsKey((KeyCode)(index + (int)KeyCode.Alpha0)))
            {
                // Получаем данные о клавише из словаря
                KeyData keyData = KeyPadDict[(KeyCode)(index + (int)KeyCode.Alpha0)];

                // Устанавливаем позицию картинки на позицию плейсхолдера
                keyData.image.transform.position = placeholder.transform.position;
            }
        }
        foreach (var entry in KeyPadDict)
        {
            string imageName = entry.Value.image.name;
            string prefix = imageName.Substring(0, imageName.IndexOf("but")); // Извлекаем часть строки до слова "but"
            prefixList.Add(prefix);
        }
        Debug.Log(string.Join(", ", prefixList));
    }
    int GetIndexForKeyCode(string placeholderName)
    {
        // Получаем номер из имени плейсхолдера (например, "Place1" -> 1)
        if (int.TryParse(placeholderName.Replace("Place", ""), out int index))
        {
            return index - 1; // Вычитаем 1, так как индексы начинаются с 0
        }
        return -1; // Если не удалось получить индекс из имени плейсхолдера
    }
    void keyHandler()
    {
        if (currentIndex != 8)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("Check1");
                KeyCode keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), prefixList[currentIndex]);
                keyCode1 = (KeyCode)Enum.Parse(typeof(KeyCode), prefixList[currentIndex]);
                if (Input.GetKeyDown(keyCode))
                {
                    Debug.Log("Check2");
                    int index = currentIndex;
                    KeyCode keyCodee = KeyPadDict.ElementAt(index).Key; // Получаем ключ (KeyCode) по индексу
                    KeyData keyDataa = KeyPadDict[keyCodee]; // Получаем данные о клавише по ключу
                    UnityEngine.UI.Image image = keyDataa.image; // Получаем изображение из данных о клавише
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
                    currentIndex++;
                    ShouldKeyCheck = false;
                }
                else
                {
                    Debug.Log(keyCode);
                    // Неправильная клавиша, сбрасываем последовательность
                    GMscr.TMG.SetActive(false);
                    GMscr.randomminigamic();
                }
            }
        }
        else
        {
            Debug.Log("POBEDA");
            GMscr.AllScore = GMscr.AllScore + 5000;
            GMscr.refreshScore();
            GMscr.TMG.SetActive(false);
            GMscr.randomminigamic();
        }
    }
    List<int> GenerateRandomIndices(int min, int max)
    {
        List<int> indices = new List<int>();
        for (int i = min; i <= max; i++)
        {
            indices.Add(i);
        }
        Shuffle(indices);
        return indices;
    }
    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
public class KeyData
{
    public UnityEngine.UI.Image image;
    public int index;

    public KeyData(UnityEngine.UI.Image image, int index)
    {
        this.image = image;
        this.index = index;
    }
}