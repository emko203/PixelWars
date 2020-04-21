using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private KeyCode Player2_Up = KeyCode.UpArrow;
    [SerializeField] private KeyCode Player2_Down = KeyCode.DownArrow;

    [SerializeField] private KeyCode Player1_Up = KeyCode.W;
    [SerializeField] private KeyCode Player1_Down = KeyCode.S;

    [SerializeField] private KeyCode Player2_Select = KeyCode.LeftControl;
    [SerializeField] private KeyCode Player1_Select = KeyCode.E;

    [SerializeField] private KeyCode Player2_DeSelect = KeyCode.RightArrow;
    [SerializeField] private KeyCode Player1_DeSelect = KeyCode.A;

    [SerializeField] private KeyCode Player2_EndTurn = KeyCode.LeftShift;
    [SerializeField] private KeyCode Player1_EndTurn = KeyCode.RightShift;

    public EnumPressedKeyAction CheckKeyInput(EnumTeams currentTeam)
    {
        switch (currentTeam)
        {
            case EnumTeams.Red:
                return CheckPressedKeyPlayer1();
            case EnumTeams.Blue:
                return CheckPressedKeyPlayer2();
            default:
                return CheckPressedKeyPlayer1();
        }
    }

    private EnumPressedKeyAction CheckPressedKeyPlayer2()
    {
        return CheckPressedKey(Player2_Up, Player2_Down, Player2_Select, Player2_DeSelect, Player2_EndTurn);
    }

    private EnumPressedKeyAction CheckPressedKeyPlayer1()
    {
        return CheckPressedKey(Player1_Up, Player1_Down, Player1_Select, Player1_DeSelect, Player1_EndTurn);
    }

    private EnumPressedKeyAction CheckPressedKey(KeyCode Up, KeyCode Down, KeyCode Select, KeyCode Deselect, KeyCode EndTurn)
    {
        if (Input.GetKeyDown(Up))
            return EnumPressedKeyAction.UP;
        else if (Input.GetKeyDown(Down))
            return EnumPressedKeyAction.DOWN;
        else if (Input.GetKeyDown(Select))
            return EnumPressedKeyAction.SELECT;
        else if (Input.GetKeyDown(Deselect))
            return EnumPressedKeyAction.DESELECT;
        else if (Input.GetKeyDown(EndTurn))
            return EnumPressedKeyAction.END_TURN;
        else
            return EnumPressedKeyAction.NO_ACTION;
    }
}
