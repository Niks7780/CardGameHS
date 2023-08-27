using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class EndTurn : MonoBehaviour
    {
        [SerializeField]
        private GameObject _axis;
        public GameObject _p1;
        public GameObject _p2;
        private Card[] _cards;
        private DeckManager _deckManager;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _deckManager = FindObjectOfType<DeckManager>();
            _cards = FindObjectsOfType<Card>();
            
        }
        public void EndTheTurn()
        {
            
            StartCoroutine(TurnTable());
        }
        public IEnumerator TurnTable()
        {
            var time = 0f;
            var startPos = _axis.transform.eulerAngles;
            var endPos = _axis.transform.eulerAngles + new Vector3(0, 180, 0);
            var startPosp1 = _p1.transform.eulerAngles;
            var endPosp1 = _p1.transform.eulerAngles + new Vector3(0, 180, 0);
            var startPosp2 = _p2.transform.eulerAngles;
            var endPosp2 = _p2.transform.eulerAngles + new Vector3(0, 180, 0);
            while (time < 2f)
            {
                _axis.transform.eulerAngles = Vector3.Lerp(startPos, endPos, time);
                _p1.transform.eulerAngles = Vector3.Lerp(startPosp1, endPosp1, time);
                _p2.transform.eulerAngles = Vector3.Lerp(startPosp2, endPosp2, time);
                time += Time.deltaTime;
                yield return null;
            }
            foreach(Card card in _cards)
            {

                StartCoroutine(RotateCard(card));

                if (card.State == CardStateType.InHand)
                {
                    _deckManager.CloseCards(card);
                }
            }
            _gameManager.ChangeTurn();
            
        }
        private IEnumerator RotateCard(Card card)
        {
            var time = 0f;
            var startPos = card.transform.eulerAngles;
            var endPos = card.transform.eulerAngles + new Vector3(0f, 180f, 0f);
            while (time < 1f)
            {
                card.transform.eulerAngles = Vector3.Lerp(startPos, endPos, time);
                time += Time.deltaTime;
                yield return null;
            }
            card.transform.eulerAngles = endPos;

        }
    }
}