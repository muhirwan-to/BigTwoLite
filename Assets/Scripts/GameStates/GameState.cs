using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    public enum Id
    {
        GS_CharacterSelection,
        GS_Gameplay,
    }

    [SerializeField]
    private GameObject  m_ui;
    [SerializeField]
    private Id          m_id;

    public Id           ID => m_id;
    public GameObject   UI => m_ui;

}
