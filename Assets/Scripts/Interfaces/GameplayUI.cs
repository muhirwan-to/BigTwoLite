using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public enum ESortType
    {
        None,
        SortByFlag,
        SortByValue
    }

    [SerializeField]
    private Button              m_helpButton;
    [SerializeField]
    private Button              m_sortButton;
    [SerializeField]
    private List<Transform>     m_avatarPositions;
    [SerializeField]
    private List<Transform>     m_handCardsPositions;
    [SerializeField]
    private Text                m_countdownText;
    [SerializeField]
    private Text                m_playingTimerText;
    [SerializeField]
    private GameObject          m_actionScreen;
    [SerializeField]
    private GameObject          m_lowDropArea;
    [SerializeField]
    private GameObject          m_midDropArea;
    [SerializeField]
    private GameObject          m_highDropArea;
    [SerializeField]
    private GameObject          m_playedCards;
    [SerializeField]
    private Text                m_lowHint;
    [SerializeField]
    private Text                m_midHint;
    [SerializeField]
    private Text                m_highHint;

    public Button               ButtonHelp => m_helpButton;
    public List<Transform>      AvatarPositions => m_avatarPositions;
    public List<Transform>      HandCardsPositions => m_handCardsPositions;
    public Text                 TextCountdown => m_countdownText;
    public Text                 TextPlayingTimer => m_playingTimerText;
    public GameObject           ActionScreen => m_actionScreen;
    public GameObject           LowDropAreaList => m_lowDropArea;
    public GameObject           MidDropAreaList => m_midDropArea;
    public GameObject           HighDropAreaList => m_highDropArea;
    public GameObject           PlayedCards => m_playedCards;
    public Text                 LowHint => m_lowHint;
    public Text                 MidHint => m_midHint;
    public Text                 HighHint => m_highHint;

    private ESortType           m_currentSort;

    // Start is called before the first frame update
    void Start()
    {
        m_currentSort = ESortType.None;
        m_sortButton.onClick.AddListener(SortCard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SortCard()
    {
        switch (m_currentSort)
        {
            case ESortType.None:
            case ESortType.SortByFlag:
                {
                    m_currentSort = ESortType.SortByValue;
                    break;
                }
            case ESortType.SortByValue:
                {
                    m_currentSort = ESortType.SortByFlag;
                    break;
                }
        }

        SortCard(m_currentSort);
    }

    public void SortCard(ESortType _type)
    {
        List<Transform> sortenedCards = new List<Transform>();

        for (int i = 0; i < m_playedCards.transform.childCount; i++)
        {
            sortenedCards.Add(m_playedCards.transform.GetChild(i));
        }

        if (_type == ESortType.SortByFlag)
        {
            sortenedCards.Sort(delegate (Transform t1, Transform t2)
                {
                    Card card1 = t1.GetChild(0).GetComponent<Card>();
                    Card card2 = t2.GetChild(0).GetComponent<Card>();

                    int compare = card1.Flag.CompareTo(card2.Flag);
                    if (compare < 0)
                    {
                        Vector3 tmp_pos = new Vector3(t1.transform.localPosition.x, t1.transform.localPosition.y, t1.transform.localPosition.z);
                        Quaternion tmp_rot = new Quaternion(t1.transform.localRotation.x, t1.transform.localRotation.y, t1.transform.localRotation.z, t1.transform.localRotation.w);
                        Vector3 tmp_sca = new Vector3(t1.transform.localScale.x, t1.transform.localScale.y, t1.transform.localScale.z);

                        t1.transform.localPosition = t2.transform.localPosition;
                        t1.transform.localRotation = t2.transform.localRotation;
                        t1.transform.localScale = t2.transform.localScale;

                        t2.transform.localPosition = tmp_pos;
                        t2.transform.localRotation = tmp_rot;
                        t2.transform.localScale = tmp_sca;
                    }

                    return compare;
                });
        }
        else if (_type == ESortType.SortByValue)
        {
            sortenedCards.Sort(delegate (Transform t1, Transform t2)
            {
                Card card1 = t1.GetChild(0).GetComponent<Card>();
                Card card2 = t2.GetChild(0).GetComponent<Card>();

                int c1 = (int)card1.Value;
                int c2 = (int)card2.Value;

                int compare = card1.Value.CompareTo(card2.Value);
                if (compare < 0)
                {
                    Vector3 tmp_pos = new Vector3(t1.transform.localPosition.x, t1.transform.localPosition.y, t1.transform.localPosition.z);
                    Quaternion tmp_rot = new Quaternion(t1.transform.localRotation.x, t1.transform.localRotation.y, t1.transform.localRotation.z, t1.transform.localRotation.w);
                    Vector3 tmp_sca = new Vector3(t1.transform.localScale.x, t1.transform.localScale.y, t1.transform.localScale.z);

                    t1.transform.localPosition = t2.transform.localPosition;
                    t1.transform.localRotation = t2.transform.localRotation;
                    t1.transform.localScale = t2.transform.localScale;

                    t2.transform.localPosition = tmp_pos;
                    t2.transform.localRotation = tmp_rot;
                    t2.transform.localScale = tmp_sca;
                }

                return compare;
            });
        }

        foreach (var card in sortenedCards)
        {
            print("sortenedCards: " + card.GetChild(0).GetComponent<Card>().Value);
        }
    }
}
