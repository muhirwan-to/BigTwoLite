using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GS_CharacterSelection : GameState
{
    private Button m_btnGo;

    private void Awake()
    {
        Transform ui = Instantiate(UI, GameManager.Instance.Canvas.transform).transform;
        Transform characterList = ui.Find("CharacterList");
        List<Character> players = GameManager.Instance.PlayerList;

        float gapX = characterList.GetComponent<RectTransform>().sizeDelta.x / (players.Count - 1);
        float leftX = -characterList.GetComponent<RectTransform>().sizeDelta.x / 2;

        for (int i = 0; i < players.Count; i++)
        {
            var player = Instantiate(players[i], ui);
            player.transform.localPosition = new Vector3((leftX + (gapX * i)), player.transform.position.y, player.transform.position.z);
        }

        m_btnGo = ui.Find("ButtonGo").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_btnGo.onClick.AddListener(delegate { GameManager.Instance.SetGameState(Id.GS_Gameplay); });
    }

    // Update is called once per frame
    void Update()
    {
    }
}
