using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ECardSortType
{
    None,
    SortByFlag,
    SortByValue
}

public class GameplayUI : GUIBase
{
    [SerializeField]
    private Button              m_helpButton;
    [SerializeField]
    private Button              m_sortButton;
    [SerializeField]
    private Button              m_skipButton_Cheat;
    [SerializeField]
    private Button              m_flipButton_Cheat;
    [SerializeField]
    private Text                m_preparationCountdownText;
    [SerializeField]
    private Text                m_gameOverText;
    [SerializeField]
    private Text                m_playingTimerText;
    [SerializeField]
    private GameObject          m_avatarCoordinates;
    [SerializeField]
    private GameObject          m_actionScreen;
    [SerializeField]
    private GameObject          m_playersScore;
    [SerializeField]
    private List<DropArea>      m_dropAreaList;
    [SerializeField]
    private List<DropArea>      m_handAreaList;
    [SerializeField]
    private Text                m_lowHint;
    [SerializeField]
    private Text                m_midHint;
    [SerializeField]
    private Text                m_highHint;

    public Button               ButtonHelp => m_helpButton;
    public Button               ButtonSort => m_sortButton;
    public Button               ButtonSkip_Cheat => m_skipButton_Cheat;
    public Button               ButtonFlip_Cheat => m_flipButton_Cheat;
    public Text                 TextPreparationCountdown => m_preparationCountdownText;
    public Text                 TextGameOver => m_gameOverText;
    public Text                 TextPlayingTimer => m_playingTimerText;
    public GameObject           AvatarCoordinates => m_avatarCoordinates;
    public GameObject           ActionScreen => m_actionScreen;
    public GameObject           PlayersScore => m_playersScore;
    public List<DropArea>       DropAreaList => m_dropAreaList;
    public List<DropArea>       HandAreaList => m_handAreaList;
    public Text                 LowHint => m_lowHint;
    public Text                 MidHint => m_midHint;
    public Text                 HighHint => m_highHint;
   
    public List<Card>           InHandCardsGUI { get; set; }

    private ECardSortType       m_lastSortByButton;


    // Start is called before the first frame update
    void Start()
    {
        m_lastSortByButton = ECardSortType.None;
        m_sortButton.onClick.AddListener(OnButtonClick_Sort);
        m_skipButton_Cheat.onClick.AddListener(OnButtonClick_Cheat_Skip);
        m_flipButton_Cheat.onClick.AddListener(OnButtonClick_Cheat_FlipOpponentCards);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnButtonClick_Sort()
    {
        ECardSortType sort = ECardSortType.None;

        switch (m_lastSortByButton)
        {
            case ECardSortType.None:
            case ECardSortType.SortByFlag:
                {
                    sort = ECardSortType.SortByValue;
                    break;
                }
            case ECardSortType.SortByValue:
                {
                    sort = ECardSortType.SortByFlag;
                    break;
                }
        }

        SortCard(sort);

        m_lastSortByButton = sort;
    }

    void OnButtonClick_Cheat_Skip()
    {
        GS_Gameplay gs = GetGamseState<GS_Gameplay>();
        
        if (gs.GamePhase == EGamePhase.PlayingCard)
        {
            StopCoroutine(gs.StartPlayingTimer());
            gs.SwitchPhase(EGamePhase.CompareCard);
        }
    }

    void OnButtonClick_Cheat_FlipOpponentCards()
    {
        foreach (Actor player in GameManager.Instance.PlayerList)
        {
            if (!player.IsMC)
            {
                player.FlipHandCards();
            }
        }
    }

    public void SortCard(ECardSortType _type)
    {
        if (_type == ECardSortType.SortByFlag)
        {
            InHandCardsGUI.Sort((card1, card2) => card1.Flag.CompareTo(card2.Flag));
        }
        else if (_type == ECardSortType.SortByValue)
        {
            InHandCardsGUI.Sort((card1, card2) => card1.Value.CompareTo(card2.Value));
        }

        for (int i = 0; i < HandAreaList.Count; i++)
        {
            InHandCardsGUI[i].transform.SetParent(HandAreaList[i].transform, false);
        }
    }
}
