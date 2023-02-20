using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FPS : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;

    void Update()
    {
        tmpText.text = Mathf.RoundToInt(1 / Time.deltaTime).ToString();
    }
}
