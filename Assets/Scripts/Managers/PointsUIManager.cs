
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PointsUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text prefab;
    [SerializeField] int pullSize = 64;
    [SerializeField] private float duration = 2;
    
    public static PointsUIManager ThisInstance { get; private set; }
    private Transform _transform;

    private Queue<TMP_Text> _tmpTextPull = new Queue<TMP_Text>();
    private List<ActiveText> _activeTexts = new List<ActiveText>();


    void Awake()
    {
        ThisInstance = this;
        _transform = transform;

       

        for (int i = 0; i < pullSize; i++)
        {
            var text = Instantiate(prefab, _transform);
            text.gameObject.SetActive(false);
            _tmpTextPull.Enqueue(text);
        }
    }

    private class ActiveText
    {
        public TMP_Text TMPText;
        public float Duration;
        public float Timer;
        public Vector3 StartPosition;

        Vector2 moveDir;
        public ActiveText()
        {
            moveDir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        }

        public void MoveText()
        {
            var delta = (Timer / Duration) * moveDir;
            var pos = StartPosition + new Vector3(delta.x, delta.y, 0);
            pos.z = 0;
            TMPText.transform.position = pos;
        }
    }

    public void AddText(int amount, Vector3 spawnPosition, Color textColor)
    {
        TMP_Text textObj;
        try
        {
            textObj = _tmpTextPull.Dequeue();
        }
        catch(InvalidOperationException)
        {
            textObj = Instantiate(prefab, _transform);
        }
        
        

        textObj.text = amount.ToString();
        textObj.gameObject.SetActive(true);

        textObj.color = textColor ;

        ActiveText activeTextObj = new ActiveText() { Duration = duration};
        activeTextObj.Timer = activeTextObj.Duration;
        activeTextObj.TMPText = textObj;
        activeTextObj.StartPosition = spawnPosition + Vector3.up;
        activeTextObj.MoveText();
        _activeTexts.Add(activeTextObj);
    }

    private void Update()
    {
        for (int i = 0; i < _activeTexts.Count; i++)
        {
            ActiveText activeTextObj = _activeTexts[i];
            activeTextObj.Timer -= Time.deltaTime;

            if (activeTextObj.Timer <= 0)
            {
                activeTextObj.TMPText.gameObject.SetActive(false);
                _tmpTextPull.Enqueue(activeTextObj.TMPText);
                _activeTexts.RemoveAt(i);
                --i;
            }
            else
            {
                var color = activeTextObj.TMPText.color;
                color.a = activeTextObj.Timer / activeTextObj.Duration;
                activeTextObj.TMPText.color = color;
                
                activeTextObj.MoveText();
            }
        }
    }
}

