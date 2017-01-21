using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{

    public RawImage vrGoogleImage;
    public RawImage giftCardImage;
    public Text currentPlayerText;
    public Text roundNumberText;
    public Text currentPlayerMoneyBalanceText;
    public Text infoText;

    private int m_roundNumber = 0;

	// Use this for initialization
	void Start ()
    {
        // disable all UI items at the beginning
        vrGoogleImage.gameObject.SetActive(false);
        giftCardImage.gameObject.SetActive(false);
        currentPlayerText.gameObject.SetActive(false);
        currentPlayerMoneyBalanceText.gameObject.SetActive(false);
        roundNumberText.gameObject.SetActive(false);
        infoText.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void ShowVrGoogleImage()
    {
        vrGoogleImage.gameObject.SetActive(true);
    }

    public void HideVrGoogleImage()
    {
        vrGoogleImage.gameObject.SetActive(false);
    }

    public void ShowGiftCardImage()
    {
        giftCardImage.gameObject.SetActive(true);
    }

    public void HideGiftCardImage()
    {
        giftCardImage.gameObject.SetActive(false);
    }

    public void ShowRoundNumberText()
    {
        roundNumberText.gameObject.SetActive(true);
    }

    public void HideRoundNumberText()
    {
        roundNumberText.gameObject.SetActive(false);
    }

    public void ShowCurrentPlayerText()
    {
        currentPlayerText.gameObject.SetActive(true);
    }

    public void HideCurrentPlayerText()
    {
        currentPlayerText.gameObject.SetActive(false);
    }

    public void ShowCurrentPlayerMoneyBalance()
    {
        currentPlayerMoneyBalanceText.gameObject.SetActive(true);
    }

    public void HideCurrentPlayerMoneyBalance()
    {
        currentPlayerMoneyBalanceText.gameObject.SetActive(false);
    }

    public void ShowInfoText()
    {
        infoText.gameObject.SetActive(true);
    }

    public void HideInfoText()
    {
        infoText.gameObject.SetActive(false);
    }

    public void IncrementRoundNumber()
    {
        m_roundNumber++;
        roundNumberText.text = "Round " + m_roundNumber;
    }

    public void SetCurrentPlayerText(string playerName)
    {
        currentPlayerText.text = playerName;
    }

    public void SetInfoText(string text)
    {
        infoText.text = text;
    }

    public void SetCurrentPlayerMoneyBalance(int amountOfMoney)
    {
        currentPlayerMoneyBalanceText.text = amountOfMoney + "$";
    }

}
