using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeLinkedList
{
    #region ChangeType
    /// <summary>
    /// 変更イベント種類
    /// </summary>
    public enum ChangeType
    {
        ItemInserted,
        ItemRemoved,
        ItemSet,
        ItemCleared
    }
    #endregion

    #region ItemChangedEventArgs
    /// <summary>
    /// 変更イベントパラメータ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ItemChangedEventArgs<T> : EventArgs
    {
        public readonly ChangeType Type;
        public readonly int Index;
        public readonly T Item;

        public ItemChangedEventArgs(ChangeType _type, int _index, T _item)
        {
            this.Type = _type;
            this.Index = _index;
            this.Item = _item;
        }
    }
    #endregion

    #region SyncConvert
    /// <summary>
    /// 同期対象格納時、オブジェクト変換用メソッド（デフォルト）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    /// <returns></returns>
    public delegate object SyncConvert<T>(T input);
    #endregion

    #region ListSyncronizer
    /// <summary>
    /// 同期化用プロキシクラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListSyncronizer<T>
    {
        #region Member
        /// <summary>
        /// 同期対象
        /// </summary>
        private IList target;

        /// <summary>
        /// 同期対象格納時、オブジェクト変換メソッド
        /// </summary>
        private SyncConvert<T> convert;
        #endregion

        #region Properties
        public IList Target
        {
            get { return target; }
            set { target = value; }
        }
        public SyncConvert<T> Convert
        {
            get { return convert; }
            set { convert = value; }
        }
        #endregion

        #region Constructor
        public ListSyncronizer()
        {
            this.Convert = DefaultConvert;
        }
        public ListSyncronizer(SyncConvert<T> _convert)
        {
            this.Convert = _convert;
        }
        #endregion

        #region Methods
        private object DefaultConvert(T arg)
        {
            return arg;
        }

        public void Handler(object sender, ItemChangedEventArgs<T> e)
        {
            switch (e.Type)
            {
                case ChangeType.ItemCleared:
                    this.Target.Clear();
                    break;
                case ChangeType.ItemInserted:
                    this.Target.Insert(e.Index, convert(e.Item));
                    break;
                case ChangeType.ItemRemoved:
                    this.Target.RemoveAt(e.Index);
                    break;
                case ChangeType.ItemSet:
                    this.Target[e.Index] = convert(e.Item);
                    break;
            }
        }
        #endregion
    }
    #endregion
}

