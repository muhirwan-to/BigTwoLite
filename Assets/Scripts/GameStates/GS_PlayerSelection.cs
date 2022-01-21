using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GS_PlayerSelection : GameState
{
    private Actor               m_selectedPlayer;
    private PlayerSelectionUI   UI;

    private void Awake()
    {
        m_selectedPlayer = null;
        
        UI = Instantiate(UIPrefab.GetComponent<PlayerSelectionUI>(), GameManager.Instance.Canvas.transform);
        UI.ButtonGo.onClick.AddListener(ChangeNextState);
    }

    // Start is called before the first frame update
    void Start()
    {
        PutAvatarOnList();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PutAvatarOnList()
    {
        List<Actor> players = GameManager.Instance.PlayerList;
        GameObject avatarList = UI.AvatarListContainer;

        float gapX = avatarList.GetComponent<RectTransform>().sizeDelta.x / (players.Count - 1);
        float leftX = -avatarList.GetComponent<RectTransform>().sizeDelta.x / 2;

        for (int i = 0; i < players.Count; i++)
        {
            var avatar = Instantiate(players[i].Avatar, avatarList.transform);
            if (avatar)
            {
                avatar.Actor = players[i];
                avatar.transform.localPosition = new Vector3((leftX + (gapX * i)), avatarList.transform.position.y, avatarList.transform.position.z);
                avatar.GetComponent<Button>().onClick.AddListener( delegate { SelectAvatar(avatar); });
            }
        }
    }

    void SelectAvatar(Avatar _selected)
    {
        m_selectedPlayer = _selected.Actor;
        UI.TextShouldSelect.SetActive(false);
    }

    void ChangeNextState()
    {
        // found selected avatar
        if (m_selectedPlayer)
        {
            UI.TextShouldSelect.SetActive(false);

            GameManager.Instance.SelectPlayer(m_selectedPlayer);
            GameManager.Instance.SetGameState(EId.GS_Gameplay);
        }
        else
        {
            UI.TextShouldSelect.SetActive(true);
        }
    }
}
