using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].transform.localPosition = m_inHandCardsContainer.transform.GetChild(0).localPosition;
            _cards[i].transform.localRotation = m_inHandCardsContainer.transform.GetChild(0).localRotation;
            _cards[i].transform.localScale = m_inHandCardsContainer.transform.GetChild(0).localScale;

            DestroyImmediate(m_inHandCardsContainer.transform.GetChild(0).gameObject);
            Instantiate(_cards[i], m_inHandCardsContainer.transform);
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
}
