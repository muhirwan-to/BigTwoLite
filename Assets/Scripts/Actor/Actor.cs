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

    public Avatar               Avatar => m_avatar;
    public string               Name => m_name;
    public bool                 IsMC { get; private set; }
    public List<Card>           InHandCards { get; private set; }

    private const int           k_inHandCardsCount = 13;

    private void Awake()
    {
        m_avatar.Actor = this;
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
            print("add listener to card: " + card);

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

    void SwapCards(Card _first, Card _second)
    {
        Transform temp = _first.transform;

        print("swap cards: " + _first + " with: " + _second);

        _first.transform.position = _second.transform.position;
        _first.transform.rotation = _second.transform.rotation;
        _first.transform.localScale = _second.transform.localScale;

        _second.transform.position = temp.transform.position;
        _second.transform.rotation = temp.transform.rotation;
        _second.transform.localScale = temp.transform.localScale;
    }
}
