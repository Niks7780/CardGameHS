using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards.ScriptableObjects;

namespace Cards
{
    public delegate void Notifier();
    public class DeckManager : MonoBehaviour
    {
        private Material _baseMat;

        [SerializeField]
        private Material _shirtMat;
        [SerializeField]
        private GameObject _shirtCard;

        private Card[] _deckPlayer1;
        private Card[] _deckPlayer2;

        [SerializeField]
        private int _maxCardInDeck = 30;
        [SerializeField]
        private Transform _deckPlayer1Parent;
        [SerializeField]
        private Transform _deckPlayer2Parent;

        [Space, SerializeField]
        private HandPlayer _handPlayer1;
        [SerializeField]
        private HandPlayer _handPlayer2;


        [Space, SerializeField]
        private Card _prefabCard;
        [SerializeField]
        private CardPackConfiguration[] _packs;

        [Space, SerializeField]
        private CardConfiguration[] _allCardsPlayer1;
        [SerializeField]
        private CardConfiguration[] _allCardsPlayer2;
        [SerializeField]
        private AbilitySystem _abilitySystem;

        private GameManager _gameManager;

        private void Awake()
        {
            IEnumerable<CardPropertiesData> cards = new List<CardPropertiesData>();

            foreach (var pack in _packs) cards = pack.UnionProperties(cards);

            _baseMat = new Material(Shader.Find("TextMeshPro/Sprite"));
            _baseMat.renderQueue = 2995;
            _deckPlayer1 = CreateDeck(_deckPlayer1Parent, _allCardsPlayer1);
            _deckPlayer2 = CreateDeck(_deckPlayer2Parent, _allCardsPlayer2);
        }
        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }


        private Card[] CreateDeck(Transform parent, CardConfiguration[] allCards)
        {
            foreach (var ability in _abilitySystem._abilities.Values)
            {
               
            }
            var deck = new Card[_maxCardInDeck];
            var offset = new Vector3();
            for (int i=0; i<_maxCardInDeck; i++)
            {
                deck[i] = Instantiate(_prefabCard, parent);
                int index = Random.Range(0, 7);
                var type = (StatType)index;
                _abilitySystem._abilities.TryGetValue(type, out var value);
                deck[i].SetAbility(value);
                deck[i].transform.localPosition = offset;
                offset += new Vector3(0f, 1f, 0f);

                var random = allCards[Random.Range(0, allCards.Length)];
                var newMat = new Material(_baseMat);
                newMat.mainTexture = random._texture;
                deck[i].Configuration(random, newMat, CardUtility.GetDescriptionById((uint)random._id));
            }
            return deck;
        }

        public void CloseCards(Card card)
        {
            StartCoroutine(CloseCard(card));
        }
        public IEnumerator CloseCard(Card card)
        {
            var time = 0f;
            var startPos = card.transform.eulerAngles;
            var endPos = card.transform.eulerAngles + new Vector3(0f, 0f, 180f);
            while (time < 1f)
            {
                card.transform.eulerAngles = Vector3.Lerp(startPos, endPos, time);
                time += Time.deltaTime;
                yield return null;
            }
            card.transform.eulerAngles = endPos;
        }

            private void Update()
        {
           
            if (_gameManager.IsPlayer1Turn == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    for (int i = _deckPlayer2.Length - 1; i >= 0; i--)
                    {
                        if (_deckPlayer2[i] != null)
                        {
                            _handPlayer2.SetNewCard(_deckPlayer2[i]);
                            _deckPlayer2[i] = null;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    for (int i = _deckPlayer1.Length - 1; i >= 0; i--)
                    {
                        if (_deckPlayer1[i] != null)
                        {
                            _handPlayer1.SetNewCard(_deckPlayer1[i]);
                            _deckPlayer1[i] = null;
                            break;
                        }
                    }
                }
            }
        }

    }
}