using Game.CMS_Content;
using System.Collections.Generic;
using UnityEngine;
public class HandCards : MonoBehaviour
{
    
    private List<BaseCard> _carts = new List<BaseCard>();
    private float _spacing;
    
    
    private void Update()
    {
        SetPoseTile();
    }

    public void Add(BaseCard Card)
    {
      //  Card.Get<DataCard>(out var Component);
        //Instantiate(Component.Prefab);
        _carts.Add(Card);
    }

    public void Remove(BaseCard Card)
    {
        _carts.Remove(Card);
    }
    private void SetPoseTile()
    {
        for (int i = 0; i < _carts.Count; i++)
        {
            var Offset = i * _spacing - _carts.Count * _spacing;

          //  _carts[i].Data.Prefab.transform.localPosition = new Vector2(0, i * Spacing) + Vector2.down * Offset;
        }
    }
}
