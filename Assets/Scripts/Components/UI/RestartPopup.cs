using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text currentScoreTMP;
    [SerializeField] private TMP_Text recordScoreTMP;
    
    private void Start()
    {
        DataHolder.HealthManager.OnHealthEnded += WaitPartsFalling;
    }

    void WaitPartsFalling()
    {
        DataHolder.BlocksSpawner.StopAllCoroutines();
        DataHolder.Cutter.gameObject.SetActive(false);

        StartCoroutine(BoardClear());
    }

    private IEnumerator BoardClear()
    {
        while (DataHolder.AllActiveBlockParts.Any(x=>x.readyToSpawn != true))
        {
            yield return new WaitForSeconds(0.3f);
        }
        DisplayDeathPopup();
    }

    private void DisplayDeathPopup()
    {
        
        transform.GetChild(0).gameObject.SetActive(true);
        currentScoreTMP.text = DataHolder.ScoreLabels.CurrentScore.ToString();

        if (DataHolder.ScoreLabels.RecordScore > PlayerPrefs.GetInt("Record"))
        {
            PlayerPrefs.SetInt("Record", DataHolder.ScoreLabels.RecordScore);
        }

        recordScoreTMP.text = "Лучший: " + DataHolder.ScoreLabels.RecordScore;
        GetComponent<Animator>().SetTrigger("ShowPopUp");
    }
}
