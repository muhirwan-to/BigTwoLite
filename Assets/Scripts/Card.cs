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
    private EType   m_type;
    [SerializeField]
    private EValue  m_value;

    public EType    Type => m_type;
    public EValue   Value => m_value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
