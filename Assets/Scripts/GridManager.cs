using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public PlayerType playerType;
    public GameManager gameManager;
    public GameObject cardsPrefab;
    public Transform canvasTransform;
    public int rows = 2;
    public int columns = 3;
    public string gridName;

    public List<CardManager> cards = new List<CardManager>();
    private LevelData activeLevelData;
    
    void Start()
    {
        CreateGrid(rows, columns);
        
        for (var i = 0; i < cards.Count; i++) 
            cards[i].SetPower(activeLevelData.Powers[i]);
        
    }

    void CreateGrid(int rows, int columns)
    {
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                GameObject card = Instantiate(cardsPrefab, canvasTransform);
                card.name = $"{gridName}_{i}_{j}";

                var cardManager = card.GetComponentInChildren<CardManager>();
                cardManager.gridManager = this;
                
                cards.Add(cardManager);
            }
        }
    }
    
    public (bool, string) IsAnyCardSelected()
    {
        foreach (var card in cards)
        {
            if (card.isSelected)
            {
                return (true, card.gameObject.name);
            }
        }

        return (false, "");
        
    }
    
    public void TheCardIsSelected(CardManager cardManager)
    {
        gameManager.CardSelected(playerType, cardManager);
    }

    public int CountActiveCards()
    {
        var count = 0;
        foreach (var card in cards)
        {
            if (card.gameObject.activeSelf)
            {
                count++;
            }
        }

        return count;
    }

    public void ResetSelection()
    {
        foreach (var card in cards)
        {
            card.isSelected = false;
            card.cardImage.color = new Color(1, 1, 1, 1);
        }
    }

    public void SetGridCells(LevelData level)
    {
       activeLevelData = level;
    }
}
