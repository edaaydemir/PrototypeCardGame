using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum PlayerType
{
    Player,
    Enemy
}

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public TMP_Text textView;
    public PlayerType WhoIsPlaying;

    public CardManager selectedCard;
    public CardManager targetCard;

    public GridManager playerGridManager;
    public GridManager enemyGridManager;
    public List<LevelData> levels = new List<LevelData>();
    public LevelData playerCards;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        enemyGridManager.SetGridCells(levels[1]);
        playerGridManager.SetGridCells(playerCards);
    }

    public int ActiveLevel
    {
        get => PlayerPrefs.GetInt("ActiveLevel", 0);
        set => PlayerPrefs.SetInt("ActiveLevel", value);
    }

 

    void Start()
    {
        WhoIsPlaying = PlayerType.Player;       
        textView.SetText("Player Turn");       
    }

    public void CardSelected(PlayerType clickedCardType, CardManager clickedCard)
    {
        if (WhoIsPlaying == PlayerType.Player)
        {
            if(selectedCard == null && clickedCardType == PlayerType.Enemy)
            {
                Debug.Log("Player is already playing");
                selectedCard = null;
                targetCard = null;
                playerGridManager.ResetSelection();
                enemyGridManager.ResetSelection();
                return;
            }
            
            if (clickedCardType == PlayerType.Player)
            {
                selectedCard = clickedCard;
                textView.SetText("Choose Enemy Card to Attack");
            }
            else
            {
                targetCard = clickedCard;
                
                Debug.Log($"Player {selectedCard.gameObject.transform.parent.name} card is attacking {targetCard.gameObject.transform.parent.name} card");
                var RealPower = (int)(selectedCard.cardData.Power + (selectedCard.cardData.Luck * selectedCard.cardData.Power));           
               
                if (targetCard.cardData.Stamina > 0)
                {  
                    if(RealPower >= targetCard.cardData.Stamina)
                    {
                        var fark = RealPower - targetCard.cardData.Stamina;
                        targetCard.cardData.Stamina = 0;
                        targetCard.cardData.Power -= fark;
                        if (targetCard.cardData.Power <= 0)
                        {
                            Debug.Log($"{targetCard.gameObject.transform.parent.name} card is dead");
                            targetCard.gameObject.SetActive(false);

                            CheckFinal();
                        }
                    }
                    else
                    {
                        targetCard.cardData.Stamina -= RealPower;
                        if (targetCard.cardData.Power <= 0)
                        {
                            Debug.Log($"{targetCard.gameObject.transform.parent.name} card is dead");
                            targetCard.gameObject.SetActive(false);

                            CheckFinal();
                        }
                    }
                }
                else
                {
                    targetCard.cardData.Power -= RealPower;
                    if(targetCard.cardData.Power <= 0)
                    {
                        Debug.Log($"{targetCard.gameObject.transform.parent.name} card is dead");
                        targetCard.gameObject.SetActive(false);

                        CheckFinal();
                    }
                }
                WhoIsPlaying = PlayerType.Enemy;
                selectedCard = null;
                targetCard = null;
                textView.SetText("Enemy Turn");
                playerGridManager.ResetSelection();
                enemyGridManager.ResetSelection();
            }
        }
        else
        {
            if(selectedCard == null && clickedCardType == PlayerType.Player)
            {
                Debug.Log("Enemy is already playing");
                selectedCard = null;
                targetCard = null;
                playerGridManager.ResetSelection();
                enemyGridManager.ResetSelection();
                return;
            }
            
            if (clickedCardType == PlayerType.Enemy)
            {
                selectedCard = clickedCard;
                textView.SetText("Choose Player Card to Attack");
            }
            else
            {
                targetCard = clickedCard;
                
                Debug.Log($"Enemy {selectedCard.gameObject.transform.parent.name} card is attacking {targetCard.gameObject.transform.parent.name} card");
                var RealPower = (int)(selectedCard.cardData.Power + (selectedCard.cardData.Luck * selectedCard.cardData.Power));

                if (targetCard.cardData.Stamina > 0)
                {
                    if (RealPower >= targetCard.cardData.Stamina)
                    {
                        var fark = RealPower - targetCard.cardData.Stamina;
                        targetCard.cardData.Stamina = 0;
                        targetCard.cardData.Power -= fark;
                        if (targetCard.cardData.Power <= 0)
                        {
                            Debug.Log($"{targetCard.gameObject.transform.parent.name} card is dead");
                            targetCard.gameObject.SetActive(false);

                            CheckFinal();
                        }
                    }
                    else
                    {
                        targetCard.cardData.Stamina -= RealPower;
                        if (targetCard.cardData.Power <= 0)
                        {
                            Debug.Log($"{targetCard.gameObject.transform.parent.name} card is dead");
                            targetCard.gameObject.SetActive(false);

                            CheckFinal();
                        }
                    }
                }
                else
                {
                    targetCard.cardData.Power -= RealPower;
                    if (targetCard.cardData.Power <= 0)
                    {
                        Debug.Log($"{targetCard.gameObject.transform.parent.name} card is dead");
                        targetCard.gameObject.SetActive(false);

                        CheckFinal();
                    }
                }  
                WhoIsPlaying = PlayerType.Player;
                selectedCard = null;
                targetCard = null;
                textView.SetText("Player Turn");                
                playerGridManager.ResetSelection();
                enemyGridManager.ResetSelection();
            }
        }
    }

    private void CheckFinal()
    {
        var enemyCardCount = enemyGridManager.CountActiveCards();
        var playerCardCount = playerGridManager.CountActiveCards();
                    
        if (enemyCardCount == 0 || playerCardCount == 0)
        {
            Debug.Log(enemyCardCount == 0 ? "Player Won" : "Enemy Won");
            
            if(enemyCardCount == 0)
            {
                ActiveLevel++;
            }          
            var sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneBuildIndex);
        }        
    }
}


