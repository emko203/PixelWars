using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Ability system", menuName = "Create new Behaviour")]
public class AbilityBehaviour : ScriptableObject
{
    [SerializeField] private bool isDOT = false;
    [SerializeField] private int damagePerTick = 0;
    [SerializeField] private int timeBetweenTicks = 0;

    [SerializeField] private bool isAOE = false;
    [SerializeField] private int amountOfSpaces = 0;
    [SerializeField] private bool isNotAllDirections = false;
    [SerializeField] private EnumDirection direction = 0;

    [SerializeField] private bool isDamaging = false;
    [SerializeField] private int amountOfDamage = 0;

    [SerializeField] private bool isHealing = false;
    [SerializeField] private int amountOfHealing = 0;

    [SerializeField] private bool isDefensive = false;
    [SerializeField] private bool isMovement = false;

    [SerializeField] private bool isRanged = false;
    [SerializeField] private int amountOfRange = 0;

    [SerializeField] private bool isCloseCombat = false;

    [SerializeField] private bool canCastOnSelf = false;
    [SerializeField] private bool needsTarget = false;
    [SerializeField] private bool needsLineOfSight = false;

    [SerializeField] private bool hasPush = false;
    [SerializeField] private int amountOfPush = 0;

    [SerializeField] private bool hasPull = false;
    [SerializeField] private int amountOfPull = 0;

    [SerializeField] private bool hasCooldown = false;
    [SerializeField] private int amountOfCooldown = 0;

    public bool IsDOT { get => isDOT; set => isDOT = value; }
    public bool IsAOE { get => isAOE; set => isAOE = value; }
    public bool IsDamaging { get => isDamaging; set => isDamaging = value; }
    public bool IsHealing { get => isHealing; set => isHealing = value; }
    public bool IsDefensive { get => isDefensive; set => isDefensive = value; }
    public bool IsMovement { get => isMovement; set => isMovement = value; }
    public bool IsRanged { get => isRanged; set => isRanged = value; }
    public bool IsCloseCombat { get => isCloseCombat; set => isCloseCombat = value; }
    public bool CanCastOnSelf { get => canCastOnSelf; set => canCastOnSelf = value; }
    public bool NeedsTarget { get => needsTarget; set => needsTarget = value; }
    public bool HasPushBack { get => hasPush; set => hasPush = value; }
    public bool HasPull { get => hasPull; set => hasPull = value; }
    public bool HasCooldown { get => hasCooldown; set => hasCooldown = value; }
    public bool NeedsLineOfSight { get => needsLineOfSight; set => needsLineOfSight = value; }
    public int AmountOfDamage { get => amountOfDamage; set => amountOfDamage = value; }
    public int AmountOfRange { get => amountOfRange; set => amountOfRange = value; }
    public int AmountOfHealing { get => amountOfHealing; set => amountOfHealing = value; }
    public int AmountOfPush { get => amountOfPush; set => amountOfPush = value; }
    public int AmountOfPull { get => amountOfPull; set => amountOfPull = value; }
    public int AmountOfCooldown { get => amountOfCooldown; set => amountOfCooldown = value; }
    public int DamagePerTick { get => damagePerTick; set => damagePerTick = value; }
    public int TimeBetweenTicks { get => timeBetweenTicks; set => timeBetweenTicks = value; }
    public int AmountOfSpaces { get => amountOfSpaces; set => amountOfSpaces = value; }
    public EnumDirection Direction { get => direction; set => direction = value; }
    public bool IsNotAllDirections { get => isNotAllDirections; set => isNotAllDirections = value; }
}
