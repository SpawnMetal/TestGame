using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public CheckGroundClass CG; //Объект отвечающий за соприкосновение с землёй и расположен на уровне ног игрока
    public float Speed = 10f; //Скорость персонажа
    public float JumpSpeed = 7f; //Сила прыжка
    public float MinHeight = -20f; //Минимальная высота для перезапуска сцены
    
    private Rigidbody2D RB; //Класс компонента управления физикой
    private int AxisMove = 0; //Данные направления персонажа
    private bool FacingRight = true; //Смотрит ли персонаж вправо
    private int[] AxisMas = new int[2]; //Массив направлений движений
    private int AxisMasScreenID = 0; //Направление движения заданное на сенсорном дисплее
    private int AxisMasKeyboardID = 1; //Направление движения заданное клавиатурой
    private int AxisLeft = -1; //Направление движения влево
    private int AxisNone = 0; //Направление движения отсутствует
    private int AxisRight = 1; //Направление движения вправо

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>(); //Получаем методы и свойства компонента отвечающего за физику
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump(); //Если была нажата клавиша пробел, выполнится метод прыжка
        if(transform.position.y < MinHeight) ReloadLevel();
    }

    void FixedUpdate() //Используется при обновлении Rigidbody
    {
        GetMoveAxis();
        MoveVelocity();
        FlipCheck();
    }

    public void Jump() //Прыжкок
    {
        if (!CG.IsGrounded) return; //Если персонаж не на земле, не делать прыжок

        RB.AddForce(transform.up * JumpSpeed, ForceMode2D.Impulse); //Задаём импульс вверх
        GlobalModule.PlayerJumped = true;
    }

    public void GetMoveFromScreenButton(int InputAxis) //Получает направление движения исходя из нажатых кнопок на сенсорном дисплее
    {
        AxisMas[AxisMasScreenID] = InputAxis;
    }

    void GetMoveFromKeyboardButton() //Получает направление движения исходя из нажатых кнопок на клавиатуре
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) AxisMas[AxisMasKeyboardID] = AxisLeft;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) AxisMas[AxisMasKeyboardID] = AxisRight;
        else AxisMas[AxisMasKeyboardID] = AxisNone;
    }

    void GetMoveAxis() //Получает направление движения
    {
        bool MoveStop = true;

        GetMoveFromKeyboardButton();

        for(int i = 0; i < AxisMas.Length; i++) if (AxisMas[i] != AxisNone)
        {
            AxisMove = AxisMas[i];
            MoveStop = false;

            break;
        }

        if (MoveStop) AxisMove = AxisNone;
    }

    void SetMoveAxis(int Axis) //Получает глобальное bool направление движения
    {
        if (Axis == AxisLeft) GlobalModule.MoveLeft = true;
        else if(Axis == AxisRight) GlobalModule.MoveRight = true;
        else
        {
            GlobalModule.MoveLeft = false;
            GlobalModule.MoveRight = false;
        }
    }

    void MoveVelocity() //Движение персонажа
    {
        RB.velocity = new Vector2(AxisMove * Speed, RB.velocity.y); //Задаёт вектор направления движения помноженный на скорость для перемещения персонажа
        SetMoveAxis(AxisMove);
    }

    void FlipCheck() //Проверяет и разворачивает изображение персонажа
    {
        if (FacingRight && AxisMove < AxisNone) Flip();
        else if (!FacingRight && AxisMove > AxisNone) Flip();
    }

    void Flip() //Разворот персонажа
    {
        FacingRight = !FacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void OnCollisionEnter2D(Collision2D Collision) //Соприкосновение с объектом
    {
        if (Collision.gameObject.tag == "asteroid") ReloadLevel(); //Перезагрузка уровня при соприкосновении с астероидом
        if (Collision.gameObject.tag == "Finish") Application.LoadLevel("Scene2"); //Переходим к следующей сцене при соприкосновении с финишом
    }

    void OnCollisionExit2D(Collision2D Collision) //Покидание соприкосновения с объектом
    {
        
    }

    void ReloadLevel() //Перезагрузка уровня
    {
        Application.LoadLevel(Application.loadedLevel); //Загрузка уровня - Загруженный уровень
    }

    void OnGUI() //Обновление GUI
    {
        //GUI.Label(new Rect(0, 0, 100, 20), transform.position.x.ToString()); //Вывод текста слева сверху
    }
}
