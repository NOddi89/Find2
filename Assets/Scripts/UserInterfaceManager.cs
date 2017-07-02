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
    public Button rollDiceButton;
    public Button buyButton;

    private int m_roundNumber = 0;

    public delegate void BuyTileClick();
    public static BuyTileClick OnBuyTileClick;

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
        rollDiceButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Fire an event when the buy tile buttin is clicked
    /// </summary>
    public void OnBuyTileClicked()
    {
        if(OnBuyTileClick != null)
        {
            OnBuyTileClick();
        }
    }

    /// <summary>
    /// Show VR googles image
    /// </summary>
    public void ShowVrGoogleImage()
    {
        vrGoogleImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide VR googles image
    /// </summary>
    public void HideVrGoogleImage()
    {
        vrGoogleImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show gift card image
    /// </summary>
    public void ShowGiftCardImage()
    {
        giftCardImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide gift card image
    /// </summary>
    public void HideGiftCardImage()
    {
        giftCardImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show round number text
    /// </summary>
    public void ShowRoundNumberText()
    {
        roundNumberText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide round number text
    /// </summary>
    public void HideRoundNumberText()
    {
        roundNumberText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show current player name text
    /// </summary>
    public void ShowCurrentPlayerText()
    {
        currentPlayerText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide current palyer name text
    /// </summary>
    public void HideCurrentPlayerText()
    {
        currentPlayerText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show current player money balance text
    /// </summary>
    public void ShowCurrentPlayerMoneyBalance()
    {
        currentPlayerMoneyBalanceText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide current player money balance text
    /// </summary>
    public void HideCurrentPlayerMoneyBalance()
    {
        currentPlayerMoneyBalanceText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show info text
    /// </summary>
    public void ShowInfoText()
    {
        infoText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide info text
    /// </summary>
    public void HideInfoText()
    {
        infoText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show roll dice button
    /// </summary>
    public void ShowRollDiceButton()
    {
        rollDiceButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide roll dice button text
    /// </summary>
    public void HideRollDiceButton()
    {
        rollDiceButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show buy button
    /// </summary>
    public void ShowBuyButton()
    {
        buyButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide buy button
    /// </summary>
    public void HideBuyButton()
    {
        buyButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Increment the round number with one and update the ui text
    /// </summary>
    public void IncrementRoundNumber()
    {
        m_roundNumber++;
        roundNumberText.text = "Round " + m_roundNumber;
    }

    /// <summary>
    /// Set the current player name text
    /// </summary>
    /// <param name="playerName"></param>
    public void SetCurrentPlayerText(string playerName)
    {
        currentPlayerText.text = playerName;
    }

    /// <summary>
    /// Set the info text
    /// </summary>
    /// <param name="text"></param>
    public void SetInfoText(string text)
    {
        infoText.text = text;
    }

    /// <summary>
    /// Set the current player money balance
    /// </summary>
    /// <param name="amountOfMoney"></param>
    public void SetCurrentPlayerMoneyBalance(int amountOfMoney)
    {
        currentPlayerMoneyBalanceText.text = amountOfMoney + "$";
    }

}
