using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GS_CharacterSelection : GameState
{
    private Button      m_btnGo;
    private GameObject  m_characterList;
    private GameObject  m_shouldSelectText;

    private Character   m_selectedCharacter;

    private void Awake()
    {
        m_selectedCharacter = null;
        
        UI = Instantiate(UIPrefab, GameManager.Instance.Canvas.transform);

        m_characterList = UI.transform.Find("CharacterList").gameObject;
        m_shouldSelectText = UI.transform.Find("TextShouldSelect").gameObject;
        m_btnGo = UI.transform.Find("ButtonGo").GetComponent<Button>();
        m_btnGo.onClick.AddListener(ChangeNextState);

        PutCharacterOnList();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckSelectedCharacter();
    }

    void PutCharacterOnList()
    {
        List<Character> prefabs = GameManager.Instance.CharacterListPrefab;

        float gapX = m_characterList.GetComponent<RectTransform>().sizeDelta.x / (prefabs.Count - 1);
        float leftX = -m_characterList.GetComponent<RectTransform>().sizeDelta.x / 2;

        for (int i = 0; i < prefabs.Count; i++)
        {
            var player = Instantiate(prefabs[i], m_characterList.transform);
            if (player)
            {
                player.transform.localPosition = new Vector3((leftX + (gapX * i)), player.transform.position.y, player.transform.position.z);
            }
        }
    }

    void CheckSelectedCharacter()
    {
        for (int i = 0; i < m_characterList.transform.childCount; i++)
        {
            var character = m_characterList.transform.GetChild(i).GetComponent<Character>();
            if (character.IsSelected)
            {
                // change selected character, leave this as previous if nothing selected
                m_selectedCharacter = character;
                m_shouldSelectText.SetActive(false);

                break;
            }
        }
    }

    void ChangeNextState()
    {
        // found selected character
        if (m_selectedCharacter)
        {
            GameManager.Instance.SelectCharacter(m_selectedCharacter);
            GameManager.Instance.SetGameState(EId.GS_Gameplay);
            
            m_shouldSelectText.SetActive(false);
        }
        else
        {
            m_shouldSelectText.SetActive(true);
        }
    }
}
