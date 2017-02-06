using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using Nito.AsyncEx;
using OpenWeen.Core.Model.User;
using OpenWeen.Core.Api.User;

namespace OpenWeen.Forms.ViewModel.MainPage
{
    [ImplementPropertyChanged]
    public class MainViewModel
    {
        public INotifyTaskCompletion<UserModel> User { get; }
        private async Task<UserModel> GetUser() => await Core.Api.User.User.GetUser(await Account.GetUid());
        public TimelineViewModel Timeline { get; } = new TimelineViewModel();
        public MergeMessageViewModel MergeMessage { get; } = new MergeMessageViewModel();
        public MessageUserListViewModel MessageUserList { get; } = new MessageUserListViewModel();
        public MainViewModel()
        {
            User = NotifyTaskCompletion.Create(GetUser);

        }
    }
}
