using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 StartPosition; //Начальная позиция объекта
    public Vector2 EndPosition; //Конечная позиция объекта
    public float Speed; //Скорость перемещения от начальной позиции к конечной
    public bool Cycle = true; //Цикличное инверсированное перемещение
    public bool StartPosIsCurrentPos = true; //Сделать стартовую позицию текущим положением объекта
    private float Progress; //Текущий прогресс на отрезке
    private bool FirstStart = true; //Первый запуск в скрипте движения от начальной позиции к конечной, необходима для отмены инверсиинаправления при старте

    // Start is called before the first frame update
    void Start()
    {
        if (StartPosIsCurrentPos) StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

    void MoveObject() //Функция движения объекта
    {
        transform.position = Vector2.Lerp(StartPosition, EndPosition, Progress); //Векторная функция движения по отрезку

        if (Progress >= 1.0f) //Если прогресс завершился
        {
            if (!Cycle) return; //Выходим если это не цикличное движение

            Speed *= -1; //Инверсируем направление движения
        }
        else if (Progress <= 0) //Если прогресс в обратном направлении завершился
        {
            if (FirstStart) FirstStart = false; //При завершении движения разрешаем инверсировать направление
            else
            {
                if (!Cycle) return; //Выходим если это не цикличное движение

                Speed *= -1; //Инверсируем направление движения
            }
        }

        Progress += Speed; //Добавляем скорость к текущему пройденному расстоянию
    }
}
