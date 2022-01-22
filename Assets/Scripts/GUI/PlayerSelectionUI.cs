using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionUI : GUIBase
{
    [SerializeField]
    private Button      m_goButton;
    [SerializeField]
    private GameObject  m_shouldSelectText;
    [SerializeField]
    private GameObject  m_avatarsContainer;

    public Button       ButtonGo => m_goButton;
    public GameObject   TextShouldSelect => m_shouldSelectText;
    public GameObject   AvatarsContainer => m_avatarsContainer;

    // Start is called before the first frame update
    void Start()
    {
        m_goButton.onClick.AddListener(OnButtonClick_Go);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnButtonClick_Go()
    {
        GS_PlayerSelection gs = GetGamseState<GS_PlayerSelection>();

        // found selected avatar
        if (gs.SelectedAvatar)
        {
            m_shouldSelectText.SetActive(false);

            GameManager.Instance.SetMainCharacter(gs.SelectedAvatar.Actor);
            GameManager.Instance.SetGameState(EGameStateID.GS_Gameplay);
        }
        else
        {
            m_shouldSelectText.SetActive(true);
        }
    }
}
