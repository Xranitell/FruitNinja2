using TMPro;
using UnityEngine;

public class ComboLabel : MonoBehaviour
{
    private Animator _animator;
    private TMP_Text[] _tmpTexts;

    private void Start()
    {
        DataHolder.ScoreManager.OnComboEnded += ShowComboLabel;
        _animator = GetComponent<Animator>();
        _tmpTexts = transform.GetComponentsInChildren<TMP_Text>();
    }

    private void ShowComboLabel(int combo)
    {
        transform.position = DataHolder.ScoreManager.lastFruitPos;
        _tmpTexts[0].text = $"Фруктов {combo}";
        _tmpTexts[2].text = $"x {combo}";
        _animator.SetTrigger("show");
    }
}
