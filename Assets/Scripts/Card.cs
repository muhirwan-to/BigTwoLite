using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public enum EType
    {
        Diamond,
        Club,
        Heart,
        Spade
    }

    public enum EValue
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    [SerializeField]
    private EType       m_type;
    [SerializeField]
    private EValue      m_value;

    private bool        m_playable;

    public EType        Type => m_type;
    public EValue       Value => m_value;
    public bool         Playable => m_playable;

    [HideInInspector]
    public Actor        Actor;

    private void Awake()
    {
        m_playable = false;
        Actor = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DropArea"))
        {
            if (m_playable && collision.transform.childCount == 0)
            {
                GetComponent<DragAndDrop>().SetObjectHover(collision.gameObject);
            }
        }
    }

    public void SetPlayable()
    {
        m_playable = true;
    }
}
