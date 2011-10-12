using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeLinkedList
{
    #region ExtendList
    /// <summary>
    /// 内部処理配列用(要素に変更があるとChangeイベントを発生させる機能追加)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtendList<T> : Collection<T>
    {
        public event EventHandler<ItemChangedEventArgs<T>> Changed;

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            Changed(this, new ItemChangedEventArgs<T>(ChangeType.ItemInserted, index, item));
        }

        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            Changed(this, new ItemChangedEventArgs<T>(ChangeType.ItemSet, index, item));
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            Changed(this, new ItemChangedEventArgs<T>(ChangeType.ItemRemoved, index, default(T)));
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            Changed(this, new ItemChangedEventArgs<T>(ChangeType.ItemCleared, -1, default(T)));
        }
    }
    #endregion
}
