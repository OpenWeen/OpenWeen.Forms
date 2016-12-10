using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OpenWeen.Forms.Common.Controls
{
    public class ExListView : ListView
    {
        public event EventHandler<SelectedItemChangedEventArgs> LongPress;
        public event EventHandler<SelectedItemChangedEventArgs> ItemClick;
        public event EventHandler LoadMore;

        public ExListView()
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
            base.ItemTapped += ExListView_ItemTapped;
        }

        private void ExListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ItemClick?.Invoke(this, new SelectedItemChangedEventArgs(e.Item));
            SelectedItem = null;
        }

        void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;
            if (items != null && e.Item == items[items.Count - 1] && !IsRefreshing)
            {
                LoadMore?.Invoke(this, new EventArgs());
            }
        }

        public void OnLongPress(object v)
        {
            LongPress?.Invoke(this, new SelectedItemChangedEventArgs(v));
        }
    }
}
