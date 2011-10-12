using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeLinkedList
{
    public class ChangeLinkedList
    {
        public static void Main()
        {
            ExtendList<string> strList = new ExtendList<string>();
            ListSyncronizer<string> syncStrList = new ListSyncronizer<string>();

            List<string> targetList = new List<string>();

            // 同期対象のリストを割当
            syncStrList.Target = targetList;

            // イベントハンドリング（変更するリストのイベントをハンドリング)
            strList.Changed += new EventHandler<ItemChangedEventArgs<string>>(syncStrList.Handler);

            // リストに何か変更を加える
            strList.Add("AAA");
            strList.Add("BBB");

            // 同期対象が同期されているか検証
            foreach (string value in targetList)
            {
                Console.WriteLine(value);
            }
        }
    }
}
