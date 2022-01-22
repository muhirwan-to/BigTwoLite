using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Actor : MonoBehaviour
{
    public enum EState
    {
        Idle,
        Win,
        Lose
    }

    [SerializeField]
    private Avatar              m_avatar;
    [SerializeField]
    private string              m_name;
    [SerializeField]
    private GameObject          m_inHandCardsContainer;

    public Avatar               Avatar => m_avatar;
    public string               Name => m_name;
    public bool                 IsMC { get; private set; }
    public List<Card>           InHandCards { get; private set; }
    public int                  Score => m_score;

    [HideInInspector]
    public CardGroup            LowCardGroup;
    [HideInInspector]
    public CardGroup            MidCardGroup;
    [HideInInspector]
    public CardGroup            HighCardGroup;

    private int                 m_score;
    private Controller          m_controller;
    private Card                m_selectedCard;
    private EState              m_state;
    private Card.ESide          m_handCardsSide;


    private void Awake()
    {
        m_selectedCard = null;
        m_state = EState.Idle;
        m_handCardsSide = Card.ESide.FaceUp;

        LowCardGroup.Area = CardGroup.EGroupArea.Low;
        LowCardGroup.HighestValue = Card.EValue.Two;
        LowCardGroup.WinRate = 0;
        MidCardGroup.Area = CardGroup.EGroupArea.Mid;
        MidCardGroup.HighestValue = Card.EValue.Two;
        MidCardGroup.WinRate = 0;
        HighCardGroup.Area = CardGroup.EGroupArea.High;
        HighCardGroup.HighestValue = Card.EValue.Two;
        HighCardGroup.WinRate = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Avatar?.SetState(m_state);

        UpdateCardSequences();
    }

    void UpdateCardSequences()
    {
        if (InHandCards == null)
        {
            return;
        }

        List<Card> lowCards = InHandCards.GetRange(0, 5);
        List<Card> midCards = InHandCards.GetRange(5, 5);
        List<Card> highCards = InHandCards.GetRange(10, 3);

        LowCardGroup.Sequence = GameManager.Instance.SequenceChecker.GetSequence(lowCards);
        MidCardGroup.Sequence = GameManager.Instance.SequenceChecker.GetSequence(midCards);
        HighCardGroup.Sequence = GameManager.Instance.SequenceChecker.GetSequence(highCards);

        foreach (Card card in lowCards)
        {
            if (LowCardGroup.HighestValue.CompareTo(card.Value) < 0)
            {
                LowCardGroup.HighestValue = card.Value;
                LowCardGroup.HighestFlag = card.Flag;
            }
        }

        foreach (Card card in midCards)
        {
            if (MidCardGroup.HighestValue.CompareTo(card.Value) < 0)
            {
                MidCardGroup.HighestValue = card.Value;
                MidCardGroup.HighestFlag = card.Flag;
            }
        }

        foreach (Card card in highCards)
        {
            if (HighCardGroup.HighestValue.CompareTo(card.Value) < 0)
            {
                HighCardGroup.HighestValue = card.Value;
                HighCardGroup.HighestFlag = card.Flag;
            }
        }
    }

    public void SetAvatar(Avatar _avatar)
    {
        m_avatar = _avatar;
    }

    public void SetRole(bool _isMC)
    {
        if (m_controller)
        {
            m_controller = null;
        }

        IsMC = _isMC;

        if (IsMC)
        {
            m_controller = gameObject.AddComponent<PlayerController>();
        }
        else
        {
            m_controller = gameObject.AddComponent<AIController>();
        }
        
        m_controller.Actor = this;
    }

    public void PutCardsInHand(List<Card> _cards)
    {
        if (InHandCards != null && InHandCards.Count > 0)
        {
            InHandCards.Clear();
        }
        else if (InHandCards == null)
        {
            InHandCards = new List<Card>(_cards.Count);
        }

        for (int i = 0; i < _cards.Count; i++)
        {
            Card card = Instantiate(_cards[i], m_inHandCardsContainer.transform.GetChild(i).transform);

            InHandCards.Add(card);
        }
            
        if (!IsMC)
        {
            FlipHandCards(Card.ESide.FaceDown);
        }
    }

    public void SelectCard(Card _card)
    {
        if (!_card.GetComponent<DragAndDrop>().IsDragging)
        {
            if (!m_selectedCard)
            {
                m_selectedCard = _card;
            }
            else
            {
                if (m_selectedCard != _card)
                {
                    SwapCards(m_selectedCard, _card);
                }
            
                m_selectedCard = null;
            }
        }
    }

    public void DeSelectCard()
    {
        m_selectedCard = null;
    }

    public static void SwapCards(Card _first, Card _second, bool _ignoreLink = false)
    {
        if (_first.transform.parent != _second.transform.parent)
        {
            Utility.SwapParent(_first.transform, _second.transform, false);
        }
        else
        {
            Utility.SwapTransformLocal(_first.transform, _second.transform);
        }

        if (!_ignoreLink)
        {
            if (_first.LinkedCard && _second.LinkedCard)
            {
                SwapCards(_first.LinkedCard, _second.LinkedCard, true);
            }
        }
    }

    public void FlipHandCards()
    {
        Quaternion quat = m_inHandCardsContainer.transform.rotation;

        // flip opposite side
        if (m_handCardsSide == Card.ESide.FaceUp)
        {
            m_inHandCardsContainer.transform.rotation = Quaternion.Euler(quat.eulerAngles.x, -180, quat.eulerAngles.z);
            m_handCardsSide = Card.ESide.FaceDown;
        }
        else
        {
            m_inHandCardsContainer.transform.rotation = Quaternion.Euler(quat.eulerAngles.x, 0, quat.eulerAngles.z);
            m_handCardsSide = Card.ESide.FaceUp;
        }
    }

    public void FlipHandCards(Card.ESide _side)
    {
        Quaternion quat = m_inHandCardsContainer.transform.rotation;

        if (_side == Card.ESide.FaceUp)
        {
            m_inHandCardsContainer.transform.rotation = Quaternion.Euler(quat.eulerAngles.x, 0, quat.eulerAngles.z);
        }
        else
        {
            m_inHandCardsContainer.transform.rotation = Quaternion.Euler(quat.eulerAngles.x, -180, quat.eulerAngles.z);
        }

        m_handCardsSide = _side;
    }

    public void SetState(EState _state)
    {
        m_state = _state;
    }

    public void AddScore(int _addedScore)
    {
        m_score += _addedScore;
    }

    public void MultiplyScore(int _multiplier)
    {
        m_score *= _multiplier;
    }
}
