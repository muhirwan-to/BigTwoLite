using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameStateID
{
    GS_PlayerSelection,
    GS_Gameplay,
}

public abstract class GameState : MonoBehaviour
{
    [SerializeField]
    private GameObject      m_GUI;
    [SerializeField]
    private EGameStateID    m_id;

    public EGameStateID     ID => m_id;
    public GameObject       GUIObject => m_GUI;

    public _UIClass         GetGUIAs<_UIClass>()
    {
        return GUIObject.GetComponent<_UIClass>();
    }
}
