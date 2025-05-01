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

    public void Add(BaseCardView card)
    {
        if (card)
            _cards.Add(card);
    }

    public void Remove(BaseCardView card)
    {
        if (card)
            _cards.Remove(card);
    }

    private void SetPoseTile()
    {
        float size = _maxSpacing * _cards.Count;

        if (size < _maxSize)
            _spacing = _maxSpacing;
        else
            _spacing = _maxSize / size * _maxSize;

        for (int i = 0; i < _cards.Count; i++)
        {
            var offset = new Vector3(0, (i - _cards.Count / 2.2f) * _spacing, 0);
            if (_axisSwap)
                offset = new Vector3(offset.y, offset.x, offset.z);

            _cards[i].gameObject.transform.position = transform.position + offset;
        }
    }
}
