using TMPro;
using UnityEngine;

public class ComboLabel : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text[] comboTMPTexts;

    private void Start()
    {
        DataHolder.ScoreManager.OnComboEnded += ShowComboLabel;
    }

    private void ShowComboLabel(int combo)
    {
        transform.position = DataHolder.ScoreManager.lastFruitPos;
        comboTMPTexts[0].text = $"Фруктов {combo}";
        comboTMPTexts[2].text = $"x {combo}";
        animator.SetTrigger("show");
    }
}
