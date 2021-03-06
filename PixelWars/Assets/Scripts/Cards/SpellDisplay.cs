using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellDisplay : MonoBehaviour
{
    public Spell spell;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;
    public Image cardBackImage;

    public Text manaText;


    // Start is called before the first frame update
    void Start()
    {
        nameText.text = spell.name;
        descriptionText.text = spell.description;

        artworkImage.sprite = spell.artwork;
        cardBackImage.sprite = spell.cardBack;

        manaText.text = spell.manaCost.ToString();

    }


}
