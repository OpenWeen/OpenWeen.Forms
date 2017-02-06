using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeen.Core.Model.Status;

namespace OpenWeen.Forms.ViewModel.MainPage
{
    public class TimelineViewModel : ListViewModelBase<long, MessageModel>
    {
        private long _groupID = -1;

        protected override long DefaultCursor => 0;

        protected override async Task<ListData<long, ObservableCollection<MessageModel>>> GetListOverride(long cursor, int loadCount)
        {
            if (_groupID == -1)
            {
                var item = (await Core.Api.Statuses.Home.GetTimeline(max_id: cursor, count: loadCount));
                return new ListData<long, ObservableCollection<MessageModel>>(item.Statuses.LastOrDefault()?.ID ?? 0, new ObservableCollection<MessageModel>(item.Statuses));
            }
            else
            {
                var item = (await Core.Api.Friendships.Groups.GetGroupTimeline(_groupID.ToString(), max_id: cursor, count: loadCount));
                return new ListData<long, ObservableCollection<MessageModel>>(item.Statuses.LastOrDefault()?.ID ?? 0, new ObservableCollection<MessageModel>(item.Statuses));
            }
        }

        internal void SetGroupAndRefresh(GroupModel groupModel)
        {
            _groupID = groupModel.ID;
            Refresh();
        }
    }
}
