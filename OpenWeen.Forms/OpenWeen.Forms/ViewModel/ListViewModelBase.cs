using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeen.Forms.Common.Helpers;
using Nito.AsyncEx;
using PropertyChanged;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using System.Collections;

namespace OpenWeen.Forms.ViewModel
{
    [ImplementPropertyChanged]
    public abstract class ListViewModelBase<C, T>
    {
        private int _loadCount => Settings.LoadCount;
        public INotifyTaskCompletion<ListData<C, ObservableCollection<T>>> List { get; private set; }
        public void Refresh()
        {
            List = NotifyTaskCompletion.Create(GetListOverride(DefaultCursor, _loadCount));
        }
        public async Task LoadMore()
        {
            try
            {
                List.Result.Add(await GetListOverride(List.Result.Cursor, _loadCount));
            }
            catch
            {
                UserDialogs.Instance.ShowError("载入失败");
            }
        }
        public class ListData<K, V> where V : IList<T>
        {
            public K Cursor { get; private set; }
            public V Value { get; private set; }
            public ListData(K cursor, V value)
            {
                Cursor = cursor;
                Value = value;
            }
            public void Add(K cursor, V value)
            {
                Cursor = cursor;
                foreach (var item in value)
                    Value.Add(item);
            }
            public void Add(ListData<K, V> data)
            {
                Add(data.Cursor, data.Value);
            }
        }
        protected abstract Task<ListData<C, ObservableCollection<T>>> GetListOverride(C cursor, int loadCount);
        protected abstract C DefaultCursor { get; }
    }
}
