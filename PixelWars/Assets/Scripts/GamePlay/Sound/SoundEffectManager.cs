using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager
{
    private static AudioSource BasicCloseAttack;
    private static AudioSource BasicRangedAttack;
    private static AudioSource BasicMagicAttack;
    private static AudioSource BasicMove;

    private static bool IsInitialized = false;

    private static void Init()
    {
        BasicCloseAttack = Resources.Load<AudioSource>("SoundEffects/BasicCloseAttack.mp3");
        BasicRangedAttack = Resources.Load<AudioSource>("SoundEffects/BasicRangedAttack");
        BasicMagicAttack = Resources.Load<AudioSource>("SoundEffects/BasicMagicAttack");
        BasicMove = Resources.Load<AudioSource>("SoundEffects/BasicMove");
    }

    public static void PlayBasicMove()
    {
        if (IsNotNull(BasicMove))
        {
            BasicMove.Play();
        }
    }

    public static void PlayBasicMagicAttack()
    {
        if (IsNotNull(BasicMagicAttack))
        {
            BasicMagicAttack.Play();
        }
    }

    public static void PlayBasicCloseCombatAttack()
    {
        if (IsNotNull(BasicCloseAttack))
        {
            BasicCloseAttack.Play();
        }
    }

    public static void PlayBasicRangedAttack()
    {
        if (IsNotNull(BasicRangedAttack))
        {
            BasicRangedAttack.Play();
        }
    }

    private static bool IsNotNull(AudioSource source)
    {
        if (IsInitialized)
        {
            if (source != null)
            {
                return true;
            }
        }
        else
        {
            Init();
            IsInitialized = true;
            IsNotNull(source);
        }
        return false;
    }
}
