using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;

    private MovementAI enemyMovement;

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
