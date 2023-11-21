using System;
using UnityEngine;
using TMPro;


public class FPS : MonoBehaviour
{
    public TextMeshProUGUI textFPS;                        // ����������� �����.
    private int fpsCounter = 0;                 // ���������� ��������� ���������� ������.
    private float myTimeCounter = 0.0f;         // ���������� ��������� �����.
    private float lastFrameRate = 0.0f;         // ���������� ������������ � Text ���������� ������ � �������.
    public float refreshTime = 0.5f;            // ���������� ����������� ������� ��� ����������� ������� UpDate �� ����������� �����.
    const string format = "{0} FPS";            // ������ ��� ����������� �� ������.

    void Update()
    {
        if (myTimeCounter < refreshTime)        // ������� ������� ������� ������.
        {
            myTimeCounter += Time.deltaTime;
            fpsCounter++;
        }
        else
        {
            lastFrameRate = (float)fpsCounter / myTimeCounter;
            myTimeCounter = 0.0f;
            fpsCounter = 0;
        }
        textFPS.text = string.Format(format, Convert.ToInt32(lastFrameRate));
    }
}