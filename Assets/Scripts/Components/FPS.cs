using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = Mathf.RoundToInt(1 / Time.deltaTime).ToString();
    }
}
