using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Collections
{
    public class SelectableObservableCollection<T> : ObservableCollection<T> where T : ISelectableItem
    {

        /// <summary>
        /// 最初の選択アイテムを取得します。
        /// </summary>
        /// <returns></returns>
        public T GetFirstSelectedItem()
        {
            return Items.FirstOrDefault(x => x.IsSelected);
        }

        /// <summary>
        /// 1つだけ選択します。
        /// </summary>
        public void SingleSelection(int selectIndex)
        {
            for (int i = 0; i < Items.Count(); i++)
            {
                if (i == selectIndex)
                {
                    Items[i].IsSelected = true;
                }
                else
                {
                    Items[i].IsSelected = false;
                }
            }
        }

        /// <summary>
        /// 選択をクリアします。
        /// </summary>
        public void ClearSelection()
        {
            foreach (var item in this.Items)
            {
                item.IsSelected = false;
            }
        }

    }
}
