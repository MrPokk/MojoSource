using Game.CMS_Content.Cards;
using System.Collections.Generic;
using UnityEngine;

public class HandCards : MonoBehaviour
{
    [SerializeField]
    private List<BaseCardView> _cards = new List<BaseCardView>();

    public readonly int MaxCard = 5;

    [SerializeField]
    private bool _rotateOrigin;

    [Range(20, 50)]
    [SerializeField]
    private float _parabolaParameter;

    private void Update()
    {
        SetPoseTile();
    }

    public int GetCountCard()
    {
        return _cards.Count;
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
        var cardCount = _cards.Count;
        if (cardCount == 0) return;

        var centerOffset = (cardCount - 1) / 2.0f;

        for (int i = 0; i < cardCount; i++)
        {
            var x = i - centerOffset;
            var y = -(x * x) / _parabolaParameter;

            var newPosition = ConvertOrigin(ref i, ref centerOffset);
            _cards[i].transform.position = transform.position + newPosition;
        }
    }

    
    private Vector3 ConvertOrigin(ref int elementIndex, ref float centerOffset) {
        
        if (_rotateOrigin)
        {
            var x = elementIndex - centerOffset;
            var y = -(x * x) / _parabolaParameter;
            return new Vector3(y, x, -0.01f * elementIndex);

        }
        else
        {
            var x = elementIndex - centerOffset;
            var y = -(x * x) / _parabolaParameter;
            return new Vector3(x, y, -0.01f * elementIndex);

        }
    }
}
