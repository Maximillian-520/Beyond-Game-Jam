using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private Animator animator;
    // [SerializeField] private List<SpriteRenderer> spriteList;
    [Header("Hurt")]
    // [SerializeField] private Color hurtFlashColor = Color.red;
    // [SerializeField] private float hurtFlashTime = 0.2f;

    private int currentFacingDirection = 1;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(animator, "animator is missing");
        // Check spriteList
        // if (spriteList.Count == 0) Debug.Log("spriteList is empty");
    }
    #endregion

    // ====================================================================================================
    //                     Sprite Functions
    // ====================================================================================================
    #region Sprite
    // NOT USED
    public void UpdateFacingDirection(int newDirection)
    {
        if (newDirection == 0) return;
        if (newDirection != currentFacingDirection)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            currentFacingDirection = newDirection;
        }
    }

    public void DoIdle()
    {
        animator.SetBool("isWalking", false);
    }

    public void DoWalk()
    {
        animator.SetBool("isWalking", true);
    }

    public void DoHurt()
    {
        // StartCoroutine(DisplayHurtFlash());
    }

    // private IEnumerator DisplayHurtFlash()
    // {
    //     foreach (SpriteRenderer sprite in spriteList)
    //     {
    //         sprite.color = hurtFlashColor;
    //     }
    //     yield return new WaitForSeconds(hurtFlashTime);
    //     foreach (SpriteRenderer sprite in spriteList)
    //     {
    //         sprite.color = Color.white;
    //     }
    // }
    #endregion
}
