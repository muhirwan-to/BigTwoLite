using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_CharacterSelection : GameState
{
    [SerializeField]
    private List<Vector3>   m_characterPositions;

    public List<Vector3>    Character => m_characterPositions;

    private void Awake()
    {
        var obj = Instantiate(UI, GameManager.Instance.Canvas.transform);
        var con = obj.transform.Find("CharacterList");

        List<Character> players = GameManager.Instance.Players;

        float gapX = con.transform.GetComponent<RectTransform>().sizeDelta.x / (players.Count - 1);
        float leftX = -con.transform.GetComponent<RectTransform>().sizeDelta.x / 2;

        for (int i = 0; i < players.Count; i++)
        {
            var player = Instantiate(players[i], obj.transform);
            player.transform.localPosition = new Vector3((leftX + (gapX * i)), player.transform.position.y, player.transform.position.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
