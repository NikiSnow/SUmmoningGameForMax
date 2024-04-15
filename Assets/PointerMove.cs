using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMove : MonoBehaviour
{
    public Transform startMarker; // Начальная точка
    public Transform endMarker;   // Конечная точка
    public Transform endGreenMarker; // Начальная точка
    public Transform endYellowMarker;   // Конечная точка
    public float speed = 0.6f;    // Скорость движения
    private float startTime;      // Время начала движения
    private float journeyLength;  // Длина пути
    public QTEminigame QTEscr;
    private bool shoudstart=false;
    public GameManager GMscr;

    void OnEnable()
    {
        goBack();
        // Рассчитываем длину пути от начальной до конечной точки
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        shoudstart = true;
    }

    void Update()
    {
        if(shoudstart)
        {
            // Определяем процент пройденного пути
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            // Двигаем объект от начальной до конечной точки
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);

            // Если достигли конечной точки, телепортируемся обратно к начальной
            GetPoints();
            if (fracJourney >= 1.0f)
            {
                Debug.Log("WorkaetNe?");
                journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
                shoudstart = false;
                QTEscr.GMscr.FMG.SetActive(false);
                GMscr.randomminigamic();
            }
        }

    }

    public void goBack()
    {
        transform.position = startMarker.position;
        startTime = Time.time; // Начинаем движение заново
    }
    // Вызывается при старте движения
    public void StartMovement()
    {
        startTime = Time.time;
    }
    void GetPoints()
    {
        if (transform.position.x >= endGreenMarker.position.x)
        {
            QTEscr.PointerScore = 2000;
        }
        else if (transform.position.x >= endYellowMarker.position.x)
        {
            QTEscr.PointerScore = 1000;
        }
        else
        {
            QTEscr.PointerScore = 500;
        }
    }
}
