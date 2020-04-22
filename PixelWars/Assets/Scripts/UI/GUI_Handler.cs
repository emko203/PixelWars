using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ManaCrystalsHandler))]
public class GUI_Handler : MonoBehaviour
{
    [Space]
    [Header("Arrows")]
    [SerializeField] private GameObject blueArrow;
    [SerializeField] private GameObject redArrow;
    [Space]
    [Header("CharacterMenu")]
    [SerializeField] private Text characterInfoText;
    [SerializeField] private GameObject Menu;
    [Space]
    [Header("Animations")]
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Animator arrowAnimator;


    private ManaCrystalsHandler manaCrystalsHandler;

    private bool DoneFillingText = false;

    public void InitUI(List<GameObject> characters)
    {
        StartCoroutine(FillText(characters));
        manaCrystalsHandler = gameObject.GetComponent<ManaCrystalsHandler>();
    }

    IEnumerator FillText(List<GameObject> characters)
    {
        yield return new WaitForSeconds(1f);

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
                builder.Append("ManaCost:");
                builder.Append(CharacterInfo.ManaCost);
                builder.Append(" | MaxHealth:");
                builder.Append(CharacterInfo.MaxHealth);
                builder.AppendLine();
                builder.Append("Range: ");
                builder.Append(CharacterInfo.Range);
                builder.Append(" | Speed: ");
                builder.Append(CharacterInfo.Speed);
                builder.Append(" | Damage: ");
                builder.Append(CharacterInfo.Damage);
                builder.AppendLine();
                builder.Append("-----------------------------------");
                builder.AppendLine();

                unitsInText.Add(CharacterInfo.UnitType);
            }
        }

        characterInfoText.text = builder.ToString();
        DoneFillingText = true;
    }

    public void FlipSelectorState()
    {
        if (characterAnimator.GetBool("IsOrange"))
        {
            characterAnimator.SetBool("IsOrange", false);
        }
        else
        {
            characterAnimator.SetBool("IsOrange", true);
        }
    }

    public void FlipMenuState()
    {
        if (DoneFillingText)
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
    }

    public ManaCrystalsHandler ManaCrystalsHandler { get => manaCrystalsHandler; }

    public Animator CharacterAnimator { get => characterAnimator; }
    public GameObject BlueArrow { get => blueArrow; }
    public GameObject RedArrow { get => redArrow; }
    public Animator ArrowAnimator { get => arrowAnimator; }
}
