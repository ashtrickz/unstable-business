using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private Image backBar;
    [SerializeField] private Image fronBar;

    [SerializeField] private float pauseBeforeDestroy = 0.5f;

    private FigureControl _figureControl;
    
    private float endValue;
    private float startValue;
    private float duration = 3f;
    private float elapsedTime;

    void Start()
    {
        _figureControl = gameObject.GetComponentInParent<FigureControl>();
        startValue = 0;
        endValue = 1;
        fronBar.fillAmount = startValue;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / duration;
        fronBar.fillAmount = Mathf.Lerp(startValue, endValue, Mathf.SmoothStep(0, 1, percentageComplete));
        if (fronBar.fillAmount == 1)
            TimerStop(pauseBeforeDestroy);
    }

    void FixedUpdate() =>
        gameObject.transform.rotation = Quaternion.identity;

    public void TimerStop(float time)
    {
        Destroy(gameObject, time);
        _figureControl.FreezeFigure();
    }

}
