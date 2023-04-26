using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;

    private MovementAI enemyMovement;

    private string currentParameterBoolName;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        currentParameterBoolName = "WalkForwardUnarmed";
        PlayAnimation(currentParameterBoolName);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayDeathAnimation();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayGettingHitAnimation();

        }




        if (IsPlayingAnimationWithTag("Death"))
        {
            Debug.Log("ja");
        }
    }
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
        currentParameterBoolName = "GettingHit";

        animator.SetInteger("GettingHitAnimationNumber", RandomNumberBetween(1, 4));
        animator.SetTrigger(currentParameterBoolName);
    }
    /// <summary>
    /// Plays the death animation of the enemy
    /// Decides which death animation based on a random number
    /// </summary>
    public void PlayDeathAnimation()
    {
        currentParameterBoolName = "Death";

        animator.SetInteger("DeathAnimationNumber", RandomNumberBetween(1, 3));

        PlayAnimation(currentParameterBoolName);
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

    private bool IsPlayingAnimationWithTag(string tagName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag(tagName) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator WaitForAnimationEnd(string animationTagName)
    {
        while (IsPlayingAnimationWithTag(animationTagName))
        {
            yield return null;
        }
    }

    public AnimationClip FindAnimation(string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }

    private int RandomNumberBetween(int minInclusive, int maxExclusive)
    {
        return Random.Range(minInclusive, maxExclusive);
    }
}
