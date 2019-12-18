using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundClass : MonoBehaviour
{
    public bool IsGrounded = false; //Персонаж находится на земле
    private float IsGroundedRadius = 0.3f; //Радиус персонажа при котором определяется положение на земле
    private int MinCollidersForIsGroundChecked = 2; //Минимальное присутствие коллайдеров для проверки нахождения на земле
    private bool PlayerJumpChecked = false; //После нажатия на прыжок персонаж ещё на земле и факта отрыва от земли нет, необходимо пропустить установку состояние нахождения в прыжке

    void FixedUpdate() //Используется при обновлении Rigidbody
    {
        CheckGround();
    }

    void CheckGround() //Проверка, находится ли персонаж на земле
    {
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(transform.position, IsGroundedRadius);
        IsGrounded = Colliders.Length > MinCollidersForIsGroundChecked;

        if (GlobalModule.PlayerJumped && IsGrounded)
        {
            if (PlayerJumpChecked)
            {
                PlayerJumpChecked = false;
                GlobalModule.PlayerJumped = false; //Персонаж приземлился
            }
            else PlayerJumpChecked = true;
            
        }
    }
}
