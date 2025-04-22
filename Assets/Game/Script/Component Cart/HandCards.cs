using Game.CMS_Content.Cards;
using System.Collections.Generic;
using UnityEngine;
public class HandCards : MonoBehaviour
{
    [SerializeField]
    private List<BaseCardView> _cards = new List<BaseCardView>();

    private float _spacing;

    [SerializeField]
    private bool _axisSwap;

    [Range(0.1f, 2), SerializeField]
    private float _maxSpacing;
    [Range(1, 20), SerializeField]
    private float _maxSize;

    private void Update()
    {
        SetPoseTile();
    }

    public void Add(BaseCardView Card)
    {
        if (Card)
            _cards.Add(Card);
    }

    public void Remove(BaseCardView Card)
    {
        if (Card)
            _cards.Remove(Card);
    }

    private void SetPoseTile()
    {
        float Size = _maxSpacing * _cards.Count;

        if (Size < _maxSize)
            _spacing = _maxSpacing;
        else
            _spacing = _maxSize / Size * _maxSize;

        for (int i = 0; i < _cards.Count; i++)
        {
            var Offset = new Vector3(0, (i - _cards.Count / 2.2f) * _spacing, 0);
            if (_axisSwap)
                Offset = new Vector3(Offset.y, Offset.x, Offset.z);

            _cards[i].gameObject.transform.position = transform.position + Offset;
        }
    }
}
