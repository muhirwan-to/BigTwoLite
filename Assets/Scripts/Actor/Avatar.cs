using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Avatar : MonoBehaviour
{
    [SerializeField]
    private Sprite      m_faceIdle;
    [SerializeField]
    private Sprite      m_faceWin;
    [SerializeField]
    private Sprite      m_faceLose;
    [SerializeField]
    private Image       m_currentFace;

    public Actor        Actor { get; set; }

    protected void Awake()
    {
    }

    // Start is called before the first frame update
    protected void Start()
    {
        Transform txtName = gameObject.transform.Find("TxtName");
        if (txtName != null)
        {
            txtName.gameObject.GetComponent<Text>().text = Actor != null ? Actor.Name : "";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetState(Actor.EState _state)
    {
        if (m_currentFace)
        {
            switch (_state)
            {
                case Actor.EState.Win:
                    {
                        m_currentFace.sprite = m_faceWin;
                        break;
                    }
                case Actor.EState.Lose:
                    {
                        m_currentFace.sprite = m_faceLose;
                        break;
                    }
                default:
                    {
                        m_currentFace.sprite = m_faceIdle;
                        break;
                    }
            }
        }
    }
}
