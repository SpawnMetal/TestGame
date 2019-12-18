using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalModule : MonoBehaviour
{
    public static bool DontDestroyAudioCheck = true; //Не позволяет удалить музыку при перезапуске сцены
    public static bool MoveLeft = false; //Персонаж движется влево
    public static bool MoveRight = false; //Персонаж движется вправо
    public static bool PlayerJumped = false; //Персонаж прыгнул
}
