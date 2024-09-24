using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public Image cardImage;
    public bool isSelected = false;
    public GridManager gridManager;
    public TMP_Text powerText;
    public TMP_Text staminaText;
    public TMP_Text strengthText;
    public TMP_Text CardClassText;
    public CardData cardData;

    public int Stamina { get; set; }
    public int RealDamage { get; set; }
    public int RealPower { get; set; }


    public void SetPower(CardData virtualCard)
    {
        cardData = virtualCard;
        cardImage.sprite = cardData.Image;
    }

    private void Update()
    {
        powerText.SetText(cardData.Power.ToString());
        staminaText.SetText(cardData.Stamina.ToString());
        strengthText.SetText(cardData.Luck.ToString());
        CardClassText.SetText(cardData.CardClass.ToString());
    }

    public void CardButtonClicked()
    {
        var (isAnyCardSelected, cardName) = gridManager.IsAnyCardSelected();
        if (isAnyCardSelected && cardName != gameObject.name)
        {
            Debug.Log("Another card is already selected");
            return;
        }
        
        isSelected = !isSelected;
        var state = isSelected ? "selected" : "unselected";
        
        Debug.Log($"Button {state} on card: {gameObject.transform.parent.name}");
        
        if (isSelected)
        {
            if (gridManager.playerType == PlayerType.Enemy)
            {
                cardImage.color = new Color(1,0,0, 1);
            }
            else
            {
                cardImage.color = new Color(0,1,0, 1);
            }
        }
        else
        {
            cardImage.color = new Color(1, 1, 1, 1);
        }
        
        gridManager.TheCardIsSelected(this);
    }
}