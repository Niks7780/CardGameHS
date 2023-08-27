using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cards
{
    public class GameManager : MonoBehaviour
    {
        public event Notifier ChangeTurnEvent;

        private Material _baseMat;

        public static GameManager Self;
        public bool IsStartPhase { get; private set; } = true;
        public bool IsFirstTurn { get; private set; } = true;

        private bool _isPlayer1Turn = true;

        [SerializeField]
        public DeckManager DeckManager;
        private HandPlayer[] _playerHands;
        [SerializeField]
        private PlayerHero[] _players;


        private void LateUpdate()
        {
            CheckHeroes();
        }

        private void CheckHeroes()
        {
            foreach (var player in _players)
            {
                if (player.Health <= 0) GameOver(player);
            }
        }

        private void GameOver(PlayerHero player)
        {
            Debug.LogWarning($"Game Over! {player.name} lost!");
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif

        }

        public bool IsPlayer1Turn
        {
            get => _isPlayer1Turn;
            private set { _isPlayer1Turn = value; }
        }

        private void Awake()
        {
            Self = this;
            _baseMat = new Material(Shader.Find("TextMeshPro/Sprite"));
            _baseMat.renderQueue = 2995;
        }

        public void ChangeTurn()
        {
            Debug.LogWarning("End of the turn!");
            if (IsPlayer1Turn)
            {
                IsPlayer1Turn = false;
            }
            else
            {
                IsPlayer1Turn = true;
            }
            //DeckManager.SwitchCards();
            //StartCoroutine(RotateCamera());
            //RotateHeroes();
            //RotateCards();
            //TableManager.AllCardsCanAttack();
            if (!IsStartPhase)
            {
                ChangeTurnEvent?.Invoke();
                IsFirstTurn = false;
            }
        }

        
    }
}