using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FightHandler
{
    /// <summary>
    /// Check if there is a fight withtin range of the tile to checks character.
    /// </summary>
    /// <param name="tileToCheck"></param>
    /// <param name="currentMovingTeam"></param>
    /// <returns>return true if there is an enemy within range</returns>
    public static Character IsFight(SmartTile tileToCheck, EnumTeams currentMovingTeam)
    {
        Character AttackingCharacter = tileToCheck.GetCharacter(currentMovingTeam);
        SmartTile tempTile;

        float actualRange = AttackingCharacter.Range;

        if (AttackingCharacter != null)
        {
            //we check each tile in range if there is an enemy in this range we start attacking the closesed enemy unit
            for (int i = 0; i <= actualRange; i++)
            {
                tempTile = BattlefieldHandler.GetTileFromDirectionAhead(i, tileToCheck, currentMovingTeam, EnumDirection.UP);

                //check if tile actualy exists
                if (tempTile != null)
                {
                    if (!tempTile.IsEmpty(TurnHandler.GetOtherTeam(currentMovingTeam)))
                    {
                        Debug.Log("Found enemy " + tempTile.GetCharacter(TurnHandler.GetOtherTeam(currentMovingTeam)).TeamColor + tempTile.GetCharacter(TurnHandler.GetOtherTeam(currentMovingTeam)).CharacterName + " at range " + i);
                        return tempTile.GetCharacter(TurnHandler.GetOtherTeam(currentMovingTeam));
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Give the first enemy character we find in range if there is no enemy we return null
    /// </summary>
    /// <param name="TileToStartCheckingFrom"></param>
    /// <param name="currentMovingTeam"></param>
    /// <returns>if there is no enemy we return null else we return the enemy in range</returns>
    public static void FightWithClosestEnemy(SmartTile TileToStartCheckingFrom, EnumTeams currentMovingTeam, Character enemy)
    {
        Character AttackingCharacter = TileToStartCheckingFrom.GetCharacter(currentMovingTeam);

        if (enemy != null && AttackingCharacter != null)
        {
            AttackingCharacter.DealDamageTo(enemy);
        }
    }
}
