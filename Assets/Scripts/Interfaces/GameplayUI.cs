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
    private List<GameObject>    m_dropAreaList;
    [SerializeField]
    private List<GameObject>    m_cardAreaList;

    public Button               ButtonHelp => m_helpButton;
    public List<Transform>      AvatarPositions => m_avatarPositions;
    public List<Transform>      HandCardsPositions => m_handCardsPositions;
    public Text                 TextCountdown => m_countdownText;
    public GameObject           ActionScreen => m_actionScreen;
    public List<GameObject>     DropAreaList => m_dropAreaList;
    public List<GameObject>     CardAreaList => m_cardAreaList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
