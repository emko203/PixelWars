using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public Animator camAnimator;

    EnumTeams[] validTeams = { EnumTeams.Blue, EnumTeams.Red };
    EnumTeams currentPlayerTurn;

    // Start is called before the first frame update
    void Start()
    {
        SelectRandomStartPlayer();
        SetCameraToCurrentPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCameraToCurrentPlayer()
    {
        
        switch (currentPlayerTurn)
        {
            case EnumTeams.Red:
                //Play animation for RedPlayer camera.
                camAnimator.SetTrigger("Player1");
                break;
            case EnumTeams.Blue:
                //Play animation for BluePlayer camera.
                camAnimator.SetTrigger("Player2");
                break;
            default:
                return;
        }
    }

    private void SelectRandomStartPlayer()
    { 
        currentPlayerTurn = validTeams[Random.Range(0, validTeams.Length)];
    }
}
