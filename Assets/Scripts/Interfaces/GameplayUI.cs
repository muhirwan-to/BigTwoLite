using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    private Button          m_helpButton;
    [SerializeField]
    private List<Transform> m_avatarPositions;
    [SerializeField]
    private Text            m_countdownText;

    public Button           ButtonHelp => m_helpButton;
    public List<Transform>  AvatarPositions => m_avatarPositions;
    public Text             TextCountdown => m_countdownText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
