using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMove : MonoBehaviour
{
    public Transform startMarker; // ��������� �����
    public Transform endMarker;   // �������� �����
    public Transform endGreenMarker; // ��������� �����
    public Transform endYellowMarker;   // �������� �����
    public float speed = 0.6f;    // �������� ��������
    private float startTime;      // ����� ������ ��������
    private float journeyLength;  // ����� ����
    public QTEminigame QTEscr;
    private bool shoudstart=false;
    public GameManager GMscr;

    void OnEnable()
    {
        goBack();
        // ������������ ����� ���� �� ��������� �� �������� �����
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        shoudstart = true;
    }

    void Update()
    {
        if(shoudstart)
        {
            // ���������� ������� ����������� ����
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            // ������� ������ �� ��������� �� �������� �����
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);

            // ���� �������� �������� �����, ��������������� ������� � ���������
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
        startTime = Time.time; // �������� �������� ������
    }
    // ���������� ��� ������ ��������
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
