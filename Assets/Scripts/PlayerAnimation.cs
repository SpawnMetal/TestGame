using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator Anim;
    private bool AnimJumpStarted = false; //Начата анимация прыжка персонажа

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (GlobalModule.MoveLeft || GlobalModule.MoveRight) Anim.SetBool("Walk", true);
        else Anim.SetBool("Walk", false);

        if (GlobalModule.PlayerJumped) //Если персонаж в прыжке
        {
            if (!AnimJumpStarted) //Если анимация прыжка ещё не началась, запустить её
            {
                Anim.SetTrigger("Jump");
                AnimJumpStarted = true;
            }
        }
        else if(AnimJumpStarted) //Если персонаж приземлился, но анимация прыжка ещё запущена, остановить её
        {
            Anim.ResetTrigger("Jump");
            AnimJumpStarted = false;
        }
    }
}
