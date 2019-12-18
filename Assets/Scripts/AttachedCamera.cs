using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachedCamera : MonoBehaviour
{
    public GameObject Player; //Игрок
    private Vector3 Offset; //Расстояние между объектами

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.position - Player.transform.position; //Получаем расстояние между игроком и камерой
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + Offset; //Позиция камеры равна позиции игрока плюс расстояние
    }
}
