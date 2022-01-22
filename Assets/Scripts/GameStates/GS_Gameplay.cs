using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EGamePhase
{
    Idle,
    Preparation,
    ShuffleCard,
    PlayingCard,
    CompareCard,
    Result
}

public class GS_Gameplay : GameState
{
    [SerializeField]
    private GameObject  m_deckDeco;
    [SerializeField]
    private int         m_preparationCountdownSec;
    [SerializeField]
    private int         m_playingTimerSec;
    [SerializeField]
    private GameObject  m_handCardsCoordinates;

    public GameObject   HandCardsCoordinates => m_handCardsCoordinates;
    
    public EGamePhase   GamePhase { get; private set; }

    private GameplayUI  GUI => GetGUIAs<GameplayUI>();

    private void Awake()
    {
        GUI.SetGameState(this);
        GUI.transform.SetParent(GameManager.Instance.Canvas.transform, false);

        GamePhase = EGamePhase.Idle;
    }

    // Start is called before the first frame update
    void Start()
    {
        int AIIdx = 1;
        foreach (var player in GameManager.Instance.PlayerList)
        {
            Transform coordinate;

            if (player.IsMC)
            {
                coordinate = GUI.AvatarCoordinates.transform.GetChild(0);
                player.transform.position = HandCardsCoordinates.transform.GetChild(0).position;
            }
            else
            {
                coordinate = GUI.AvatarCoordinates.transform.GetChild(AIIdx);
                player.transform.position = HandCardsCoordinates.transform.GetChild(AIIdx).position;

                AIIdx++;
            }

            Avatar avatar = Instantiate(player.Avatar, coordinate.parent);

            if (avatar)
            {
                avatar.transform.position = coordinate.position;
                avatar.transform.rotation = coordinate.rotation;
                avatar.transform.localScale = coordinate.localScale;

                avatar.SetActor(player);
                player.SetAvatar(avatar);

                Destroy(coordinate.gameObject);
            }
        }

        Destroy(HandCardsCoordinates.transform.GetChild(0).parent.gameObject);

        SwitchPhase(EGamePhase.Preparation);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GamePhase)
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
                    UpdateHint();

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

    void UpdateHint()
    {
        List<Card> lowCardList = new List<Card>();
        List<Card> midCardList = new List<Card>();
        List<Card> highCardList = new List<Card>();

        foreach (DropArea area in GUI.DropAreaList)
        {
            if (area.AreaID == DropArea.EAreaID.LowArea)
            {
                if (area.transform.childCount > 0)
                {
                    lowCardList.Add(area.transform.GetChild(0).GetComponent<Card>());
                }
            }
            else if (area.AreaID == DropArea.EAreaID.MidArea)
            {
                if (area.transform.childCount > 0)
                {
                    midCardList.Add(area.transform.GetChild(0).GetComponent<Card>());
                }
            }
            else if (area.AreaID == DropArea.EAreaID.HighArea)
            {
                if (area.transform.childCount > 0)
                {
                    highCardList.Add(area.transform.GetChild(0).GetComponent<Card>());
                }
            }
        }

        GUI.LowHint.text = "";
        GUI.MidHint.text = "";
        GUI.HighHint.text = "";

        if (lowCardList.Count > 0)
        {
            GUI.LowHint.text = GameManager.Instance.SequenceChecker.SequenceName[(int)GameManager.Instance.SequenceChecker.GetSequence(lowCardList)];
        }

        if (midCardList.Count > 0)
        {
            GUI.MidHint.text = GameManager.Instance.SequenceChecker.SequenceName[(int)GameManager.Instance.SequenceChecker.GetSequence(midCardList)];
        }

        if (highCardList.Count > 0)
        {
            GUI.HighHint.text = GameManager.Instance.SequenceChecker.SequenceName[(int)GameManager.Instance.SequenceChecker.GetSequence(highCardList)];
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
                    m_deckDeco.SetActive(true);
                    
                    StopCoroutine(StartCountdown());
                    StartCoroutine(DistributeCards());

                    GUI.TextPreparationCountdown.gameObject.SetActive(false);

                    break;
                }
            case EGamePhase.PlayingCard:
                {
                    m_deckDeco.SetActive(false);
                    
                    StopCoroutine(DistributeCards());
                    StartCoroutine(StartPlayingTimer());

                    CloneCardsToPlayingArea();
                    LinkDropAreaWithCards(GameManager.Instance.MainCharacter);

                    GUI.ActionScreen.SetActive(true);
                    GUI.SortCard(ECardSortType.SortByValue);

                    break;
                }
            case EGamePhase.CompareCard:
                {
                    GUI.ActionScreen.SetActive(false);

                    _newPhase = EGamePhase.Result;

                    SwitchPhase(_newPhase);

                    break;
                }
            case EGamePhase.Result:
                {
                    GUI.TextGameOver.gameObject.SetActive(true);

                    int winner = Random.Range(0, (int)Time.timeAsDouble) % GameManager.Instance.PlayerList.Count;

                    for (int i = 0; i < GameManager.Instance.PlayerList.Count; i++)
                    {
                        Actor player = GameManager.Instance.PlayerList[i];
                        if (i == winner)
                        {
                            player.SetState(Actor.EState.Win);

                            GUI.TextGameOver.text = player.Name + " Win!";
                        }
                        else
                        {
                            player.SetState(Actor.EState.Lose);
                        }
                    }

                    break;
                }
            default:
                {
                    break;
                }
        }

        GamePhase = _newPhase;
    }

    void CloneCardsToPlayingArea()
    {
        Actor mc = GameManager.Instance.MainCharacter;

        if (!mc)
        {
            return;
        }

        if (GUI.InHandCardsGUI != null && GUI.InHandCardsGUI.Count > 0)
        {
            GUI.InHandCardsGUI.Clear();
        }
        else if (GUI.InHandCardsGUI == null)
        {
            GUI.InHandCardsGUI = new List<Card>(mc.InHandCards.Count);
        }

        for (int i = 0; i < GUI.HandAreaList.Count; i++)
        {
            DropArea area = GUI.HandAreaList[i];

            if (i < mc.InHandCards.Count)
            {
                Card card = mc.InHandCards[i];
                Card card_ui = Instantiate(card, area.transform);

                card.LinkedCard = card_ui;

                card_ui.transform.localPosition = new Vector3(0, 0, -1);
                card_ui.transform.localRotation = Quaternion.identity;
                card_ui.transform.localScale = new Vector3(10, 10, 10);

                card_ui.LinkedCard = card;
                card_ui.Actor = mc;
                card_ui.IsGUI = true;

                card_ui.GetComponent<DragAndDrop>().SetDragDropActive(true);
                card_ui.GetComponent<Button>().onClick.AddListener(delegate { mc.SelectCard(card_ui); });

                GUI.InHandCardsGUI.Add(card_ui);
            }
        }
    }

    void LinkDropAreaWithCards(Actor _actor)
    {
        for (int i = 0; i < GUI.DropAreaList.Count; i++)
        {
            GUI.DropAreaList[i].LinkedCardParent = _actor.InHandCards[i].transform.parent;
        }
    }

    public IEnumerator StartCountdown()
    {
        int countdown = m_preparationCountdownSec;

        while (countdown > 0)
        {
            GUI.TextPreparationCountdown.text = (countdown--).ToString();
            yield return new WaitForSeconds(1);
        }

        SwitchPhase(EGamePhase.ShuffleCard);
    }

    public IEnumerator StartPlayingTimer()
    {
        int timer = m_playingTimerSec;

        while (timer > 0)
        {
            GUI.TextPlayingTimer.text = (timer--).ToString();
            yield return new WaitForSeconds(1);
        }

        SwitchPhase(EGamePhase.CompareCard);
    }

    public IEnumerator DistributeCards()
    {
        const float waitInterval = 0.1f;

        yield return new WaitForSeconds(waitInterval);

        List<Card> cards = GameManager.Instance.CardList;
        List<Card> shuffledDeck = new List<Card>(cards.Count);

        while (cards.Count > 0)
        {
            int randIdx = Random.Range(0, cards.Count);

            shuffledDeck.Add(cards[randIdx]);
            cards.RemoveAt(randIdx);
        }

        // put cards back
        GameManager.Instance.CardList.AddRange(shuffledDeck);

        int numberOfCardsInHand = shuffledDeck.Count / GameManager.Instance.PlayerList.Count;

        foreach (var player in GameManager.Instance.PlayerList)
        {
            player.PutCardsInHand(shuffledDeck.GetRange(0, numberOfCardsInHand));
            shuffledDeck.RemoveRange(0, numberOfCardsInHand);

            yield return new WaitForSeconds(waitInterval);
        }

        SwitchPhase(EGamePhase.PlayingCard);
    }
}
