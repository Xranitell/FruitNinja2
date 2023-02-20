using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static float _timer;
    private Coroutine _slowTimeCoroutine;
    private AnimationCurve _timeChangeAnimation;
    [SerializeField] private float waitAfterPause = 1f;

    public static TimeManager Instance;
    private void Awake()
    {
        Instance = this;
    }


    public void ChangeTimeScale(float delay, float duration, AnimationCurve timeChangeAnimation)
    {
        this._timeChangeAnimation = timeChangeAnimation;
        if(_slowTimeCoroutine != null)
        {
            StopCoroutine("SlowTime");
        }
        
        _slowTimeCoroutine = StartCoroutine(SlowTime(delay, duration));
    }
    
    IEnumerator SlowTime(float delay, float duration)
    {
        if (_timer == 0)
        {
            FreezeEffect.StartFreezeScreen();
        }

        _timer = 0;

        while (_timer < duration)
        {
            _timer += delay;
            var timeScale = _timeChangeAnimation.Evaluate(_timer/duration);
            Time.timeScale = timeScale;
            yield return new WaitForSecondsRealtime(delay);
        }
        
        FreezeEffect.EndFreezeScreen();
        _timer = 0;
        StopCoroutine(_slowTimeCoroutine);
    }

    private float savedTimeScale;
    private Coroutine pauseCoroutine;
    public void SetPauseGameState(bool pause)
    {
        
        if (pause)
        {
            if(Time.timeScale != 0 ) savedTimeScale = Time.timeScale;
            Time.timeScale = 0;
            DataHolder.Cutter.gameObject.SetActive(false);
        }
        else
        {
            if (pauseCoroutine != null)
            {
                 StopCoroutine(pauseCoroutine);
            }
            pauseCoroutine = StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(waitAfterPause);
        Time.timeScale = savedTimeScale;
        DataHolder.Cutter.gameObject.SetActive(true);
    }
}
