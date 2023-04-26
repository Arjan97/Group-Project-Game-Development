using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;

    private MovementAI enemyMovement;
    
    private string currentParameterBoolName;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
        enemyMovement = GetComponent<MovementAI>();

        ActivateParameterBool(currentParameterBoolName);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayDeathAnimation();
        }
    }
    public void PlayAnimation(string stateName)
    {
        DeactivateParameterBoolsExcept(stateName);
        ActivateParameterBool(stateName);
    }

    public void PlayGettingHitAnimation()
    {
        currentParameterBoolName = "GettingHit";

  
    }
    /// <summary>
    /// 
    /// </summary>
    private void PlayDeathAnimation()
    {
        currentParameterBoolName = "Death";

        int randomNumber = Random.Range((int)1, (int)2);

        animator.SetInteger("DeathAnimationNumber", randomNumber);

        DeactivateParameterBoolsExcept(currentParameterBoolName);
        ActivateParameterBool(currentParameterBoolName);

        Debug.Log(randomNumber);

        Debug.Log(currentParameterBoolName);
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
}
