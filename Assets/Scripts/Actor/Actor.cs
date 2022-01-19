using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    [SerializeField]
    private Avatar              m_avatar;
    [SerializeField]
    private string              m_name;
    [SerializeField]
    private GameObject          m_inHandCardsContainer;

    private PlayerController    m_controller;
    private Card                m_selectedCard;

    public Avatar               Avatar => m_avatar;
    public string               Name => m_name;
    public bool                 IsMC { get; private set; }
    public List<Card>           InHandCards { get; private set; }

    private void Awake()
    {
        m_avatar.Actor = this;
        m_selectedCard = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            m_inHandCardsContainer.transform.localRotation = Quaternion.Euler(
                m_inHandCardsContainer.transform.localRotation.eulerAngles.x
                , -180
                , m_inHandCardsContainer.transform.localRotation.eulerAngles.z
                );
        }
    }

    public void SelectCard(Card _card)
    {
        print("click select cards: " + _card + " with: " + this);
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

    public void SwapCards(Card _first, Card _second)
    {
        GameObject firstParent = _first.transform.parent.gameObject;

        print("swap cards: " + _first + " with: " + _second);

        _first.transform.SetParent(_second.transform.parent, false);
        _second.transform.SetParent(firstParent.transform, false);
    }
}
