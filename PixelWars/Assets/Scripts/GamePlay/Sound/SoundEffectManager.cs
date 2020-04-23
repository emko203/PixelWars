using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager
{
    private static GameObject BasicCloseAttack;
    private static GameObject BasicRangedAttack;
    private static GameObject BasicMagicAttack;
    private static GameObject BasicMove;

    private static bool IsInitialized = false;

    private static void Init()
    {
        BasicCloseAttack = Resources.Load<GameObject>("SoundEffects/BasicCloseAttack");
        BasicRangedAttack = Resources.Load<GameObject>("SoundEffects/BasicRangedAttack");
        BasicMagicAttack = Resources.Load<GameObject>("SoundEffects/BasicMagicAttack");
        BasicMove = Resources.Load<GameObject>("SoundEffects/BasicMove");
    }

    private static void CreateSound(GameObject source)
    {
        GameObject soundPlayerOb = GameObject.Instantiate(source);
        AudioSource audioSource = soundPlayerOb.GetComponent<AudioSource>();
        audioSource.Play();
        GameObject.Destroy(soundPlayerOb, 2f);
    }

    public static void PlayBasicMove()
    {
        CheckForInit();
        if (IsNotNull(BasicMove))
        {
            CreateSound(BasicMove);
        }

    }

    public static void PlayBasicMagicAttack()
    {
        CheckForInit();
        if (IsNotNull(BasicMagicAttack))
        {
            CreateSound(BasicMagicAttack);
        }

    }

    public static void PlayBasicCloseCombatAttack()
    {
        CheckForInit();
        if (IsNotNull(BasicCloseAttack))
        {
            CreateSound(BasicCloseAttack);
        }
    }

    public static void PlayBasicRangedAttack()
    {
        CheckForInit();
        if (IsNotNull(BasicRangedAttack))
        {
            CreateSound(BasicRangedAttack);
        }
    }

    private static bool IsNotNull(GameObject source)
    {
        if (source != null)
        {
            return true;
        }
        return false;
    }

    private static void CheckForInit()
    {
        if (!IsInitialized)
        {
            Init();
            IsInitialized = true;
        }
    }
}
