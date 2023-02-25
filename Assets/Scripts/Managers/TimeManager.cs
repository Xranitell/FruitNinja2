using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private static float _timer;
    private Coroutine _slowTimeCoroutine;
    private AnimationCurve _timeChangeAnimation;
    [SerializeField] private float waitAfterPause = 1f;

    public static TimeManager Instance;

    [SerializeField] public Button pauseButton;
    [SerializeField] private PopUpAnimation pausePopUp;

    private bool _gamePaused;
    
    private void Awake()
    {
        DOTween.SetTweensCapacity(500,312);
        Instance = this;
        pausePopUp.OnPopUpShowed += PauseGame;
        pausePopUp.OnPopUpHided += UnPauseGame;
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
            FreezeEffect.Instance.StartFreezeScreen();
        }

        _timer = 0;

        while (_timer < duration)
        {
            if (!_gamePaused)
            {
                _timer += delay;
                var timeScale = _timeChangeAnimation.Evaluate(_timer/duration);
                Time.timeScale = timeScale;
            }
            yield return new WaitForSecondsRealtime(delay);
        }
        
        FreezeEffect.Instance.EndFreezeScreen();
        _timer = 0;
        StopCoroutine(_slowTimeCoroutine);
    }

    public void SetTimeScale(float timeScale) => Time.timeScale = timeScale;
    private float _savedTimeScale;
    private Coroutine _pauseCoroutine;
    
    public void UnPauseGame()
    {
        if (_pauseCoroutine != null)
        {
            StopCoroutine(_pauseCoroutine);
        }
        _pauseCoroutine = StartCoroutine(Wait());
        _gamePaused = false;
    }

    private void PauseGame()
    {
        if(Time.timeScale != 0 ) _savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
        DataHolder.Cutter.enabled = false;
        pauseButton.interactable = false;
        _gamePaused = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(waitAfterPause);
        Time.timeScale = _savedTimeScale;
        DataHolder.Cutter.enabled = true;
        pauseButton.interactable = true;
    }
}
