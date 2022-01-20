using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    private Button              m_helpButton;
    [SerializeField]
    private List<Transform>     m_avatarPositions;
    [SerializeField]
    private List<Transform>     m_handCardsPositions;
    [SerializeField]
    private Text                m_countdownText;
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
    public GameObject           ActionScreen => m_actionScreen;
    public GameObject           LowDropAreaList => m_lowDropArea;
    public GameObject           MidDropAreaList => m_midDropArea;
    public GameObject           HighDropAreaList => m_highDropArea;
    public GameObject           PlayedCards => m_playedCards;
    public Text                 LowHint => m_lowHint;
    public Text                 MidHint => m_midHint;
    public Text                 HighHint => m_highHint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
