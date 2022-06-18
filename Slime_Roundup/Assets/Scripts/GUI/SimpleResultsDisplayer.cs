using System;
using UnityEngine;
using UnityEngine.UI;

public class SimpleResultsDisplayer : MonoBehaviour
{
    [SerializeField] private Text _capturedAmmountText;
    [SerializeField] private Text _scapedAmmountText;

    private void OnEnable()
    {
        DisplayGameResults();
    }

    private void DisplayGameResults()
    {
        if (SlimesManager.Singleton) 
        {
            _capturedAmmountText.text = SlimesManager.Singleton.CapturedSlimes_Ammount + "";
            _scapedAmmountText.text = SlimesManager.Singleton.ScapedSlimes_Ammount + "";
        }
    }
}
