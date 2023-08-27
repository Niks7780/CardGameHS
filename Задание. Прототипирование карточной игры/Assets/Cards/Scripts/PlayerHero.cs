using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class PlayerHero : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _healthT;
        [SerializeField]
        private TMP_Text _manaT;
        [SerializeField]
        private PlayerType _playerType;

        private bool _isFirstTurn = true;

        [SerializeField]
        private int _health = 30;
        private int _currentMana = 1;
        private int _maxMana = 1;
        public int MaxHealth { get; private set; }

        private void Start()
        {
            Health = _health;
            MaxHealth = _health;
            Mana = _currentMana;
            //var newMat = new Material(Shader.Find("TextMeshPro/Sprite"));
            //newMat.mainTexture = _heroImage;
            var mesh = gameObject.GetComponent<MeshRenderer>();
            //mesh.sharedMaterial = newMat;
            //newMat = null;
            //mesh = null;
            GameManager.Self.ChangeTurnEvent += OnChangeTurn;
        }

        public PlayerType Player
        {
            get => _playerType;
            private set
            {
                _playerType = value;
            }
        }

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                _healthT.text = _health.ToString();
            }
        }

        public int Mana
        {
            get => _currentMana;
            set
            {
                _currentMana = value;
                _manaT.text = $"{_currentMana}/{_maxMana}";
            }
        }
        private void OnChangeTurn()
        {
            if (Player == PlayerType.Player1 && GameManager.Self.IsPlayer1Turn)
            {
                if (_maxMana < 10 && !GameManager.Self.IsFirstTurn) _maxMana += 1;
                Mana = _maxMana;
                _isFirstTurn = false;
            }
            else if (Player == PlayerType.Player2 && !GameManager.Self.IsPlayer1Turn)
            {
                if (_maxMana < 10 && !GameManager.Self.IsFirstTurn) _maxMana += 1;
                Mana = _maxMana;
                _isFirstTurn = false;
            }
        }




    }
}