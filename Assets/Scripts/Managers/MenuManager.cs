using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text recordScoreLabel;

    private void Start()
    {
        recordScoreLabel.text = PlayerPrefs.GetInt("Record").ToString();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    
}
