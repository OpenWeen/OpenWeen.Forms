using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeen.Core.Model.DirectMessage;

namespace OpenWeen.Forms.ViewModel.MainPage
{
    public class MessageUserListViewModel : ListViewModelBase<int, DirectMessageUserModel>
    {
        protected override int DefaultCursor => 0;

        protected override async Task<ListData<int, ObservableCollection<DirectMessageUserModel>>> GetListOverride(int cursor, int loadCount)
        {
            var item = await Core.Api.DirectMessages.GetUserList(cursor: cursor, count: loadCount);
            return new ListData<int, ObservableCollection<DirectMessageUserModel>>(int.Parse(item.NextCursor), new ObservableCollection<DirectMessageUserModel>(item.UserList));
        }
    }
}
