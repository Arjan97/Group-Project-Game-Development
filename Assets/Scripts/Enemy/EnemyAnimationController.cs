using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    public static EnemyAnimationController enemyController;

    private MovementAI enemyMovement;

    public bool gettingHit;
    public bool patrolling;
    public bool death;

    private void Awake()
    {
        enemyController = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
        enemyMovement = GetComponent<MovementAI>();

        ActivateParameter("Walking");
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void PlayAnimation(string stateName)
    {
        DeactivateParametersExcept("");
        ActivateParameter(stateName);

    }
    private void PlayDeathAnimation()
    {
        DeactivateParametersExcept("Death");
        ActivateParameter("Death");
    }
    private void ActivateParameter(string parameterName)
    {
        DeactivateParametersExcept(parameterName);
        animator.SetBool(parameterName, true);
    }

    private void DeactivateParametersExcept(string parameterName)
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
