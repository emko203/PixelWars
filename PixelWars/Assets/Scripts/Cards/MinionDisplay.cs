using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionDisplay : MonoBehaviour
{
    public Minion minion;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;

    public Text manaText;
    public Text attackText;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = minion.name;
        descriptionText.text = minion.description;

        artworkImage.sprite = minion.artwork;

        manaText.text = minion.manaCost.ToString();
        attackText.text = minion.attack.ToString();
        healthText.text = minion.health.ToString();
    }

    
}
