using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private int         m_playingTimerSec;

    public EGamePhase   Phase { get; private set; }

    private GameplayUI  UI;
    private Actor       m_mc;

    private void Awake()
    {
        UI = Instantiate(UIPrefab.GetComponent<GameplayUI>(), GameManager.Instance.Canvas.transform);
        Phase = EGamePhase.Idle;
        m_mc = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        int AIIdx = 1;
        foreach (var player in GameManager.Instance.PlayerList)
        {
            Transform positionRef;

            if (player.IsMC)
            {
                positionRef = UI.AvatarPositions[0];
                player.transform.position = UI.HandCardsPositions[0].position;

                m_mc = player;
            }
            else
            {
                positionRef = UI.AvatarPositions[AIIdx];
                player.transform.position = UI.HandCardsPositions[AIIdx].position;

                AIIdx++;
            }

            Avatar avatar = Instantiate(player.Avatar, positionRef.parent);

            if (avatar)
            {
                avatar.Actor = player;
                avatar.transform.position = positionRef.position;
                avatar.transform.rotation = positionRef.rotation;
                avatar.transform.localScale = positionRef.localScale;

                Destroy(positionRef.gameObject);
                Destroy(player.Avatar.gameObject);
            }
        }

        Destroy(UI.HandCardsPositions[0].parent.gameObject);

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
                    List<Card> lowCardList = new List<Card>();
                    List<Card> midCardList = new List<Card>();
                    List<Card> highCardList = new List<Card>();

                    for (int i = 0; i < UI.LowDropAreaList.transform.childCount; i++)
                    {
                        Transform drop = UI.LowDropAreaList.transform.GetChild(i);
                        if (drop.childCount > 0)
                        {
                            lowCardList.Add(drop.GetChild(0).gameObject.GetComponent<Card>());
                        }
                    }

                    for (int i = 0; i < UI.MidDropAreaList.transform.childCount; i++)
                    {
                        Transform drop = UI.MidDropAreaList.transform.GetChild(i);
                        if (drop.childCount > 0)
                        {
                            midCardList.Add(drop.GetChild(0).gameObject.GetComponent<Card>());
                        }
                    }

                    for (int i = 0; i < UI.HighDropAreaList.transform.childCount; i++)
                    {
                        Transform drop = UI.HighDropAreaList.transform.GetChild(i);
                        if (drop.childCount > 0)
                        {
                            highCardList.Add(drop.GetChild(0).gameObject.GetComponent<Card>());
                        }
                    }

                    if (lowCardList.Count == 5)
                    {
                        UI.LowHint.text = GameManager.Instance.SequenceChecker.SequenceName[(int)GameManager.Instance.SequenceChecker.GetSequence(lowCardList)];
                    }
                    
                    if (midCardList.Count == 5)
                    {
                        UI.MidHint.text = GameManager.Instance.SequenceChecker.SequenceName[(int)GameManager.Instance.SequenceChecker.GetSequence(midCardList)];
                    }
                    
                    if (highCardList.Count == 3)
                    {
                        UI.HighHint.text = GameManager.Instance.SequenceChecker.SequenceName[(int)GameManager.Instance.SequenceChecker.GetSequence(highCardList)];
                    }

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
                    m_deck.SetActive(true);
                    
                    StopCoroutine(StartCountdown());
                    StartCoroutine(DistributeCards());

                    UI.TextCountdown.gameObject.SetActive(false);

                    break;
                }
            case EGamePhase.PlayingCard:
                {
                    m_deck.SetActive(false);
                    
                    StopCoroutine(DistributeCards());
                    StartCoroutine(StartPlayingTimer());

                    CloneCardsToPlayingArea();

                    UI.ActionScreen.SetActive(true);
                    //UI.SortCard(GameplayUI.ESortType.SortByValue);

                    break;
                }
            case EGamePhase.CompareCard:
                {
                    UI.ActionScreen.SetActive(false);
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

        Phase = _newPhase;
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

    IEnumerator StartPlayingTimer()
    {
        int timer = m_playingTimerSec;

        while (timer > 0)
        {
            UI.TextPlayingTimer.text = (timer--).ToString();
            yield return new WaitForSeconds(1);
        }

        SwitchPhase(EGamePhase.CompareCard);
    }

    IEnumerator DistributeCards()
    {
        const float waitInterval = 0.1f;

        yield return new WaitForSeconds(waitInterval);

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

            yield return new WaitForSeconds(waitInterval);
        }

        SwitchPhase(EGamePhase.PlayingCard);
    }

    void CloneCardsToPlayingArea()
    {
        if (m_mc)
        {
            for (int i = 0; i < UI.PlayedCards.transform.childCount; i++)
            {
                GameObject area = UI.PlayedCards.transform.GetChild(i).gameObject;

                if (i < m_mc.InHandCards.Count)
                {
                    Card card = m_mc.InHandCards[i];
                    Card clone = Instantiate(card, area.transform);

                    card.LinkedCard = clone;

                    clone.transform.localPosition = new Vector3(0, 0, -1);
                    clone.transform.localRotation = Quaternion.identity;
                    clone.transform.localScale = new Vector3(10, 10, 10);

                    clone.LinkedCard = card;
                    clone.Actor = m_mc;

                    clone.SetPlayable();
                    clone.GetComponent<DragAndDrop>().SetDragDropActive(true);
                    clone.GetComponent<Button>().onClick.AddListener(delegate { m_mc.SelectCard(clone); });
                }
            }
        }
    }
}
