using System;
using UnityEngine;
using TMPro;


public class FPS : MonoBehaviour
{
    public TextMeshProUGUI textFPS;                        // Запрашиваем текст.
    private int fpsCounter = 0;                 // Переменная считающая количество кадров.
    private float myTimeCounter = 0.0f;         // Переменная считающая время.
    private float lastFrameRate = 0.0f;         // Переменная показывающая в Text количество кадров в секунду.
    public float refreshTime = 0.5f;            // Переменная проверяющая сколько раз запустилась функция UpDate за прописанное время.
    const string format = "{0} FPS";            // Формат для отображения на экране.

    void Update()
    {
        if (myTimeCounter < refreshTime)        // Считает сколько времени прошло.
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