using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAnimationController : MonoBehaviour
{
    /* Get the animator component to play animations */
    private Animator animator;

    /* String to store the current parameters of the animator component */
    private string currentParamaterName;

    /* Bool to track if the death animation has already been played */
    private bool playedDeathAnimation = false;

    private int layerNumber = 0;

    // Start is called before the first frame update
    void Start()
    {       
        animator = GetComponent<Animator>();


        /* Set the base animation of to the WalkForwardUnarmed animation */
        currentParamaterName = "WalkForwardUnarmed";
        PlayAnimation(currentParamaterName);
    }

    // Update is called once per frame
    void Update()
    {
        //<<<REMOVE AFTER TESTING>>> Developer code to test the death animation
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayDeathAnimation();
        }
    }
    /// <summary>
    /// Method for performing a animation with a given state name.
    /// </summary>
    /// <param name="stateName"></param>
    public void PlayAnimation(string stateName)
    {
        DeactivateParameterBoolsExcept(stateName);
        ActivateParameterBool(stateName);
    }

    /// <summary>
    /// Play animation that shows the object getting hit.
    /// Random animation based on a random  integer between 1 - 3
    /// </summary>
    public void PlayGettingHitAnimation()
    {
        currentParamaterName = "GettingHit";

        /* Decide what animation will be played based on a random number, then perform the animation by enabling the trigger */
        animator.SetInteger("GettingHitAnimationNumber", RandomNumberBetween(1, 4));
        animator.SetTrigger(currentParamaterName);
    }
    /// <summary>
    /// Plays the death animation of the enemy
    /// Sets the Integer parameter to a random number to play a random death animation
    /// </summary>
    public void PlayDeathAnimation()
    {
        currentParamaterName = "Death";        

        /* If statement to only play the animation once */
        if (!playedDeathAnimation)
        {
            /* Set the int parameter to a random number to decide what death animation will be played */
            animator.SetInteger("DeathAnimationNumber", RandomNumberBetween(1, 3));

            /* Play the animation by enabling the trigger */
            animator.SetTrigger(currentParamaterName);
        }

        /* Set to true to not perform above code again */
        playedDeathAnimation = true;
    }

    /// <summary>
    /// Bool to check if an animation of a certain group is still playing
    /// Returns true if the current animation has the given tag and if the animation is still going (if the time is < 1)
    /// </summary>
    /// <param name="tagName"></param>
    /// <returns></returns>
    public bool IsPlayingAnimationWithTag(string tagName)
    {
        if (animator.GetCurrentAnimatorStateInfo(layerNumber).IsTag(tagName) &&
            animator.GetCurrentAnimatorStateInfo(layerNumber).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Coroutine used for waiting after a certain animation is done playing
    /// Needs to be refined and tested, not working as intended yet
    /// </summary>
    /// <param name="animationTagName"></param>
    /// <returns></returns>
    public IEnumerator WaitForAnimationEnd(string animationTagName)
    {
        while (!IsPlayingAnimationWithTag(animationTagName)) yield return null;

        Debug.Log("Animation over");
    }

    /// <summary>
    /// Activates the given parameter bool of the animator to start playing an animation
    /// </summary>
    /// <param name="parameterName"></param>
    private void ActivateParameterBool(string parameterName)
    {
        animator.SetBool(parameterName, true);
    }

    /// <summary>
    /// Sets all bool parameters of the animator component to false, except the given parameter, to stop playing the animations
    /// </summary>
    /// <param name="parameterName"></param>
    private void DeactivateParameterBoolsExcept(string parameterName)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name != parameterName)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }

    /// <summary>
    /// Returns a random integer between minInclusive and maxExclusive
    /// </summary>
    /// <param name="minInclusive"></param>
    /// <param name="maxExclusive"></param>
    /// <returns></returns>
    private int RandomNumberBetween(int minInclusive, int maxExclusive)
    {
        return Random.Range(minInclusive, maxExclusive);
    }
}
