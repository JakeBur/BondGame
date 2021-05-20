using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class RabbitTestAnimator : MonoBehaviour
{

    private RabbitAnimator animator => GetComponent<RabbitAnimator>();

    private void OnSemicolon()
    {
        animator.Pause();
    }

    private void OnL()
    {
        animator.Play();
    }

    private void OnP()
    {
        
    }

    private void OnO()
    {
        
    }
    // Mapped to WASD
    private void OnMovement(InputValue value)
    {
        var direction = value.Get<Vector2>();
        direction.Normalize();

        var direction3 = new Vector3( direction.x, 0, direction.y );
        animator.Move(direction3);
    }

    private void OnButton1()
    {
        animator.DefaultAttack();
    }

    private void OnButton2()
    {
        animator.Play();
        animator.BicycleKick();
        Debug.Log("BicycleKick");
    }

    private void OnButton3()
    {
        animator.Play();
        animator.ElusiveStepPose();
        Debug.Log("ElusiveStepPose");
    }

    private void OnButton4()
    {
        animator.Play();
        animator.ElusiveStepStrike();
        Debug.Log("ElusiveStepStrike");
    }

    private void OnButton5()
    {
        animator.Play();
        animator.Quickdraw();
        Debug.Log("Quickdraw");
    }

    private void OnButton6()
    {
        animator.Play();
        animator.Shotgun();
        Debug.Log("Shotgun");
    }

    private void OnButton7()
    {
        
    }

    private void OnButton8()
    {
        
    }

    private void OnButton9()
    {
        
    }

    private void OnButton0()
    {
        
    }

    private void OnQ()
    {
        animator.Cry();
    }

    private void OnE()
    {
        animator.Eat();
    }

    private void OnF()
    {
        animator.Relax();
    }

    private void OnZ()
    {
        animator.InteractFlower();
    }

    private void OnX()
    {
        animator.InteractTree();
    }

    private void OnC()
    {
        animator.PlayerNoticed();
    }
    
}
