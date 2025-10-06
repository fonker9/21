using System.Collections.Generic;
using UnityEngine;

namespace Game_Logic
{
    public class Hand : MonoBehaviour
    {
        public List<GameObject> cards = new List<GameObject>(); //Список карт игрока
        //public Transform handParent; // Контейнер для карт (пустой объект в сцене)
        public float cardSpacing = 0.06f;
    }
}