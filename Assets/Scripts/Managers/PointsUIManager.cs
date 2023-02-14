using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

        public void MoveText()
        {
            float delta = 1.0f - (Timer / Duration);
            var pos = StartPosition + new Vector3(delta, delta, 0);
            pos.z = 0;
            TMPText.transform.position = pos;
        }
    }

    public void AddText(int amount, Vector3 startPos)
    {
        var t = _tmpTextPull.Dequeue();
        t.text = amount.ToString();
        t.gameObject.SetActive(true);

        ActiveText at = new ActiveText() { Duration = duration};
        at.Timer = at.Duration;
        at.TMPText = t;
        at.StartPosition = startPos + Vector3.up;
        at.MoveText();
        _activeTexts.Add(at);
    }

    private void Update()
    {
        for (int i = 0; i < _activeTexts.Count; i++)
        {
            ActiveText at = _activeTexts[i];
            at.Timer -= Time.deltaTime;

            if (at.Timer <= 0)
            {
                at.TMPText.gameObject.SetActive(false);
                _tmpTextPull.Enqueue(at.TMPText);
                _activeTexts.RemoveAt(i);
                --i;
            }
            else
            {
                var color = at.TMPText.color;
                color.a = at.Timer / at.Duration;
                at.TMPText.color = color;
                
                at.MoveText();
            }
        }
    }
}

