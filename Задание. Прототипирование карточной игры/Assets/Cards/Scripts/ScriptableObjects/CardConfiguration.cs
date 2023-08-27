using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.ScriptableObjects
{
    [CreateAssetMenu(fileName ="New CardConfiguration", menuName = "CardConfigs/Card Configuration", order =51)]
    public class CardConfiguration : ScriptableObject
    {
        [SerializeField]
        public int _cost;
        [SerializeField]
        public int _id;
        [SerializeField]
        public string _name;
        [SerializeField]
        public Texture _texture;
        [SerializeField]
        public int _attack;
        [SerializeField]
        public int _health;
        [SerializeField]
        public CardUnitType _type;
        [SerializeField]
        public string _description;

    }
}