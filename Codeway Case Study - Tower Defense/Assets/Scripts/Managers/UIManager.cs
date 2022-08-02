using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text stageText;
    [SerializeField] private TMP_Text killCountText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private GameObject messagePanel;

    public void UpdateStageText(int stageCount)
    {
        stageText.text = "Stage " + stageCount.ToString();
    }

    public void UpdateKillCountText(int totalMonstersKilled)
    {
        killCountText.text = totalMonstersKilled.ToString();
    }

    public void UpdateCoinsText(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        StartCoroutine(FlashMessage());
    }

    private IEnumerator FlashMessage()
    {
        messagePanel.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        messagePanel.SetActive(false);
    }
}
