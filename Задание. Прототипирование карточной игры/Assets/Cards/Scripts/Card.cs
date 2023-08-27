using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Cards.ScriptableObjects;

namespace Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public int _health;
        public int _attack;
        private static readonly Vector3 _stepPosition = new Vector3(0f, 5f, 0f);
        private const float _scale = 2f;

        [SerializeField]
        public GameObject _frontCard;
        [SerializeField]
        private MeshRenderer _mesh;

        [Space, SerializeField]
        private TextMeshPro _costText;
        [SerializeField]
        private TextMeshPro _nameText;
        [SerializeField]
        private TextMeshPro _descriptionText;
        [SerializeField]
        private TextMeshPro _attackText;
        [SerializeField]
        private TextMeshPro _healthText;
        [SerializeField]
        public TextMeshPro _typeText;

        private DeckManager _deck;
        private HandPlayer _hand;
        private Damager _damager;

        public static Card _card;
        private float _distance;
        [SerializeField]
        private Collider[] _tablePositions = new Collider[10];


        private Health _healthH;
        private Damager _attackD;

        public Vector3 PositionInHand { get; set; }
        public CardConfiguration CardConfiguration { get; private set; }
        [SerializeField]
        private LayerMask _tablePointLayer;

        private Ability _ability;

        private void Start()
        {
            _damager = GetComponent<Damager>();
            _deck = FindObjectOfType<DeckManager>();
            _hand = FindObjectOfType<HandPlayer>();
        }

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                _healthText.text = _health.ToString();
            }
        }

        public CardStateType State { get; set; } = CardStateType.InDeck;

        public void Configuration(CardPropertiesData data, Material mat, string description)
        {
            _mesh.sharedMaterial = mat;
            _costText.text = data.Cost.ToString();
            _nameText.text = data.Name;
            _descriptionText.text = description;
            _attackText.text = data.Attack.ToString();
            _healthText.text = (_health = data.Health).ToString();
            _typeText.text = data.Type == CardUnitType.None ? string.Empty : data.Type.ToString();
        }

        internal void Configuration(CardConfiguration configuration, Material mat, string description)
        {
            if (configuration == null) return;
            CardConfiguration = configuration;
            _mesh.sharedMaterial = mat;
            _costText.text = configuration._cost.ToString();
            _nameText.text = configuration._name;
            _descriptionText.text = description;
            _attackText.text = configuration._attack.ToString();
            _healthText.text = (_health = configuration._health).ToString();
            _typeText.text = configuration._type == CardUnitType.None ? string.Empty : configuration._type.ToString();
            _healthH = GetComponent<Health>();
            _attackD = GetComponent<Damager>();
            _healthH.Value = configuration._health;
            _attackD.Damage = configuration._attack;
        }

        public void SetAbility(Ability ability)
        {
            _ability = ability;
            //Debug.Log(ability);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InHand:
                    transform.localPosition += _stepPosition;
                    transform.localScale *= _scale;
                    break;
                case CardStateType.OnTable:
                    break;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InHand:
                    transform.localPosition -= _stepPosition;
                    transform.localScale /= _scale;
                    break;
                case CardStateType.OnTable:
                    break;
            }
        }

        [ContextMenu("Switch card")]
        public void SwitchCard()
        {
            transform.eulerAngles = new Vector3(0f, 0f, 180f);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _card = this;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            for (int i = 0; i < Physics.OverlapSphereNonAlloc(transform.position, 50, _tablePositions); i++)
            {
                //Debug.Log(_tablePositions[i]);
                if (_tablePositions[i].TryGetComponent(out DrawCard cardPoint) && cardPoint.IsEmpty)
                {
                    transform.position = cardPoint.transform.position + Vector3.up * 5;
                    cardPoint.IsEmpty = false;
                    return;
                }
                if (_tablePositions[i].TryGetComponent(out Card card))
                {
                    Debug.Log("...");
                    _damager.Attack();
                    _ability.Apply(this, card);
                    return;
                }
                State = CardStateType.OnTable;
                Debug.Log("На столе");
            }
            transform.position = PositionInHand;
            
            
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 delta = eventData.delta;
            Vector3 newPosition = transform.localPosition + new Vector3(delta.x, 0f, delta.y);
            transform.localPosition = newPosition;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 5);

        }
    }
}