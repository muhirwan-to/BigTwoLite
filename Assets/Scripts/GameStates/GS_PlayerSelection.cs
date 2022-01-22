using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GS_PlayerSelection : GameState
{
    private Avatar              m_selectedAvatar;
    private PlayerSelectionUI   GUI => GetGUIAs<PlayerSelectionUI>();
 
    public Avatar               SelectedAvatar => m_selectedAvatar;

    private void Awake()
    {
        m_selectedAvatar = null;

        GUI.SetGameState(this);
        GUI.transform.SetParent(GameManager.Instance.Canvas.transform, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        CloneAvatarToCanvas();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void CloneAvatarToCanvas()
    {
        List<Actor> players = GameManager.Instance.PlayerList;
        GameObject avatarsContainer = GUI.AvatarsContainer;

        float xgap = avatarsContainer.GetComponent<RectTransform>().sizeDelta.x / (players.Count - 1);
        float left = -avatarsContainer.GetComponent<RectTransform>().sizeDelta.x / 2;

        for (int i = 0; i < players.Count; i++)
        {
            var avatarClone = Instantiate(players[i].Avatar, avatarsContainer.transform);
            if (avatarClone)
            {
                avatarClone.SetActor(players[i]);
                avatarClone.transform.localPosition = new Vector3((left + (xgap * i)), avatarsContainer.transform.position.y, avatarsContainer.transform.position.z);
                avatarClone.GetComponent<Button>().onClick.AddListener( delegate { OnAvatarClick(avatarClone); });
            }
        }
    }

    void OnAvatarClick(Avatar _selected)
    {
        m_selectedAvatar = _selected;
        GUI.TextShouldSelect.SetActive(false);
    }
}
