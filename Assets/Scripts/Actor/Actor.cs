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

    [HideInInspector]
    public SequenceChecker.ESequence    LowCardSequence;
    [HideInInspector]
    public SequenceChecker.ESequence    MidCardSequence;
    [HideInInspector]
    public SequenceChecker.ESequence    HighCardSequence;
    [HideInInspector]
    public Card.EValue                  LowCardHighest;
    [HideInInspector]
    public Card.EValue                  MidCardHighest;
    [HideInInspector]
    public Card.EValue                  HighCardHighest;


    private PlayerController    m_controller;
    private Card                m_selectedCard;
    private EState              m_state;


    private void Awake()
    {
        m_selectedCard = null;
        m_state = EState.Idle;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Avatar?.SetState(m_state);
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
            Card card = Instantiate(_cards[i], m_inHandCardsContainer.transform);
            card.transform.localPosition = m_inHandCardsContainer.transform.GetChild(0).localPosition;
            card.transform.localRotation = m_inHandCardsContainer.transform.GetChild(0).localRotation;
            card.transform.localScale = m_inHandCardsContainer.transform.GetChild(0).localScale;

            DestroyImmediate(m_inHandCardsContainer.transform.GetChild(0).gameObject);

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
            GameObject firstParent = _first.transform.parent.gameObject;

            _first.transform.SetParent(_second.transform.parent, false);
            _second.transform.SetParent(firstParent.transform, false);
        }
        else
        {
            Vector3 tmp_pos = new Vector3(_first.transform.localPosition.x, _first.transform.localPosition.y, _first.transform.localPosition.z);
            Quaternion tmp_rot = new Quaternion(_first.transform.localRotation.x, _first.transform.localRotation.y, _first.transform.localRotation.z, _first.transform.localRotation.w);
            Vector3 tmp_sca = new Vector3(_first.transform.localScale.x, _first.transform.localScale.y, _first.transform.localScale.z);

            _first.transform.localPosition = _second.transform.localPosition;
            _first.transform.localRotation = _second.transform.localRotation;
            _first.transform.localScale = _second.transform.localScale;

            _second.transform.localPosition = tmp_pos;
            _second.transform.localRotation = tmp_rot;
            _second.transform.localScale = tmp_sca;
        }

        if (!_ignoreLink)
        {
            if (_first.LinkedCard && _second.LinkedCard)
            {
                SwapCards(_first.LinkedCard, _second.LinkedCard, true);
            }
        }
    }


    public void FlipHandCards(Card.ESide _side)
    {
        Quaternion quat = m_inHandCardsContainer.transform.rotation;

        if (_side == Card.ESide.FaceUp)
        {
            m_inHandCardsContainer.transform.rotation = Quaternion.Euler(quat.eulerAngles.x, 180, quat.eulerAngles.z);
        }
        else
        {
            m_inHandCardsContainer.transform.rotation = Quaternion.Euler(quat.eulerAngles.x, -180, quat.eulerAngles.z);
        }
    }

    public void SetState(EState _state)
    {
        m_state = _state;
    }
}
