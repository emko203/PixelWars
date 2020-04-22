using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Animator textAnimator;
    private Text DamageText;

    private void OnEnable()
    {
        AnimatorClipInfo[] clipinfo = textAnimator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipinfo[0].clip.length);

        DamageText = textAnimator.GetComponent<Text>();
    }

    public void SetText(string damageText)
    {
        DamageText.text = damageText;
    }
}
