using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Gameplay : GameState
{
    public enum EGamePhase
    {
        Preparation,
        ShuffleCard,
        PlayingCard,
        CompareCard,
        Result,
        Idle
    }

    [SerializeField]
    private GameObject  m_deck;
    [SerializeField]
    private int         m_countdownSec;

    public EGamePhase   Phase { get; private set; }

    private GameplayUI  UI;

    private void Awake()
    {
        UI = Instantiate(UIPrefab.GetComponent<GameplayUI>(), GameManager.Instance.Canvas.transform);
        Phase = EGamePhase.Idle;
    }

    // Start is called before the first frame update
    void Start()
    {
        int AIIdx = 1;
        foreach (var player in GameManager.Instance.PlayerList)
        {
            Avatar avatar = Instantiate(player.Avatar, UI.transform);
            if (avatar)
            {
                Transform positionRef;

                if (player.IsMC)
                {
                    positionRef = UI.AvatarPositions[0];
                    player.transform.position = UI.HandCardsPositions[0].position;
                }
                else
                {
                    player.transform.position = UI.HandCardsPositions[AIIdx].position;
                    positionRef = UI.AvatarPositions[AIIdx++];
                }

                avatar.Actor = player;
                avatar.transform.position = positionRef.position;
                avatar.transform.rotation = positionRef.rotation;
                avatar.transform.localScale = positionRef.localScale;

                Destroy(positionRef.gameObject);
            }
        }

        SwitchPhase(EGamePhase.Preparation);
    }

    // Update is called once per frame
    void Update()
    {
        switch (Phase)
        {
            case EGamePhase.Preparation:
                {
                    break;
                }
            case EGamePhase.ShuffleCard:
                {
                    break;
                }
            case EGamePhase.PlayingCard:
                {
                    break;
                }
            case EGamePhase.CompareCard:
                {
                    break;
                }
            case EGamePhase.Result:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void SwitchPhase(EGamePhase _newPhase)
    {
        switch (_newPhase)
        {
            case EGamePhase.Preparation:
                {
                    StopAllCoroutines();
                    StartCoroutine(StartCountdown());
                    break;
                }
            case EGamePhase.ShuffleCard:
                {
                    UI.TextCountdown.gameObject.SetActive(false);
                    m_deck.SetActive(true);

                    StopCoroutine(StartCountdown());
                    StartCoroutine(DistributeCards());
                    break;
                }
            case EGamePhase.PlayingCard:
                {
                    m_deck.SetActive(false);

                    StopCoroutine(DistributeCards());
                    break;
                }
            case EGamePhase.CompareCard:
                {
                    break;
                }
            case EGamePhase.Result:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    IEnumerator StartCountdown()
    {
        int countdown = m_countdownSec;

        while (countdown > 0)
        {
            UI.TextCountdown.text = (countdown--).ToString();
            yield return new WaitForSeconds(1);
        }

        SwitchPhase(EGamePhase.ShuffleCard);
    }

    IEnumerator DistributeCards()
    {
        yield return new WaitForSeconds(0.5f);

        List<Card> deck = new List<Card>();
        List<Card> shuffledDeck = new List<Card>();

        deck.AddRange(GameManager.Instance.CardListPrefab);

        while (deck.Count > 0)
        {
            int randIdx = Random.Range(0, deck.Count);

            shuffledDeck.Add(deck[randIdx]);
            deck.RemoveAt(randIdx);
        }

        int numberOfCardsInHand = shuffledDeck.Count / GameManager.Instance.PlayerList.Count;
        
        foreach (var player in GameManager.Instance.PlayerList)
        {
            player.PutCardsInHand(shuffledDeck.GetRange(0, numberOfCardsInHand));
            shuffledDeck.RemoveRange(0, numberOfCardsInHand);

            yield return new WaitForSeconds(0.5f);
        }

        SwitchPhase(EGamePhase.PlayingCard);
    }

    void SpawnCard(Card _card)
    {

    }
}
