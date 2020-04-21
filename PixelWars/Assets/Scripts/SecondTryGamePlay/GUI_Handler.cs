using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Handler : MonoBehaviour
{
    [Space]
    [Header("Animations")]
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private GameObject blueArrow;
    [SerializeField] private GameObject redArrow;
    [SerializeField] private Animator arrowAnimator;
    [Space]
    [Header("CharacterMenu")]
    [SerializeField] private Text characterInfoText;
    [SerializeField] private GameObject Menu;

    public void InitMenu(List<GameObject> characters)
    {
        StringBuilder builder = new StringBuilder();
        List<EnumUnit> unitsInText = new List<EnumUnit>();
        builder.Append("-------------------------------------------------------");
        builder.AppendLine();

        foreach (GameObject ob in characters)
        {
            Character CharacterInfo = ob.GetComponent<Character>();
            if (!unitsInText.Contains(CharacterInfo.UnitType))
            {
                builder.Append(CharacterInfo.CharacterName);
                builder.Append(":");
                builder.AppendLine();
                builder.Append("ManaCost/");
                builder.Append(CharacterInfo.ManaCost);
                builder.Append("  MaxHealth/");
                builder.Append(CharacterInfo.MaxHealth);
                builder.AppendLine();
                builder.Append("  Range/");
                builder.Append(CharacterInfo.Range);
                builder.Append("  Speed/");
                builder.Append(CharacterInfo.Speed);
                builder.Append("  Damage/");
                builder.Append(CharacterInfo.Damage);
                builder.AppendLine();
                builder.Append("-----------------------------------");
                builder.AppendLine();

                unitsInText.Add(CharacterInfo.UnitType);
            }
        }

        characterInfoText.text = builder.ToString();
    }

    public void FlipMenuState()
    {
        if (Menu.activeSelf)
        {
            Menu.SetActive(false);
        }
        else
        {
            Menu.SetActive(true);
        }
    }

    public Animator CharacterAnimator { get => characterAnimator; set => characterAnimator = value; }
    public GameObject BlueArrow { get => blueArrow; set => blueArrow = value; }
    public GameObject RedArrow { get => redArrow; set => redArrow = value; }
    public Animator ArrowAnimator { get => arrowAnimator; set => arrowAnimator = value; }
}
