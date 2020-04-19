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
    public static bool IsFight(SmartTile tileToCheck, EnumTeams currentMovingTeam)
    {
        Character AttackingCharacter = tileToCheck.GetCharacter(currentMovingTeam);
        SmartTile tempTile;

        float actualRange = AttackingCharacter.Data.Range;
        //if range is 0 we also check the tile in front of us to see if we need to attack after moving to the next tile
        if (actualRange == 0)
        {
            actualRange = 1;
        }

        if (AttackingCharacter != null)
        {
            //we check each tile in range if there is an enemy in this range we start attacking the closesed enemy unit
            for (int i = 0; i <= actualRange; i++)
            {
                tempTile = BattlefieldHandler.GetTileFromDirectionAhead(i, tileToCheck, currentMovingTeam, TurnHandler.GetDirectionFromTeam(EnumDirection.UP));
                
                if (!tempTile.IsEmpty(TurnHandler.GetOtherTeam(currentMovingTeam)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Give the first enemy character we find in range if there is no enemy we return null
    /// </summary>
    /// <param name="TileToStartCheckingFrom"></param>
    /// <param name="currentMovingTeam"></param>
    /// <returns>if there is no enemy we return null else we return the enemy in range</returns>
    public static void FightWithClosestEnemy(SmartTile TileToStartCheckingFrom, EnumTeams currentMovingTeam)
    {
        Character AttackingCharacter = TileToStartCheckingFrom.GetCharacter(currentMovingTeam);
        SmartTile tempTile;

        Character Enemy = null;

        if (AttackingCharacter != null)
        {
            //we check each tile in range if there is an enemy in this range we start attacking the closesed enemy unit
            for (int i = 0; i <= AttackingCharacter.Data.Range; i++)
            {
                tempTile = BattlefieldHandler.GetTileFromDirectionAhead(i, TileToStartCheckingFrom, currentMovingTeam, TurnHandler.GetDirectionFromTeam(EnumDirection.UP));

                if (!tempTile.IsEmpty(TurnHandler.GetOtherTeam(currentMovingTeam)))
                {
                    Enemy = tempTile.GetCharacter(TurnHandler.GetOtherTeam(currentMovingTeam));
                    break;
                }
            }
        }

        if (Enemy != null)
        {
            AttackingCharacter.DealDamageTo(Enemy);
            Enemy.CheckDeath();
        }
    }
}
