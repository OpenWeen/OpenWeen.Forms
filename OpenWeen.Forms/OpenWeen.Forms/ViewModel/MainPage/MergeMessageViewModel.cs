using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeen.Core.Model;

namespace OpenWeen.Forms.ViewModel.MainPage
{
    public class MergeMessageViewModel : ListViewModelBase<MergeMessageViewModel.MergeMessageCursor, BaseModel>
    {
        protected override MergeMessageCursor DefaultCursor => MergeMessageCursor.DEFAULT;

        protected override async Task<ListData<MergeMessageCursor, ObservableCollection<BaseModel>>> GetListOverride(MergeMessageCursor cursor, int loadCount)
        {
            var mentions = (await Core.Api.Statuses.Mentions.GetMentions(max_id: cursor.MentionCursor, count: loadCount));
            var comments = (await Core.Api.Comments.GetCommentToMe(max_id: cursor.CommentCursor, count: loadCount));
            var commentMentions = (await Core.Api.Comments.GetCommentMentions(max_id: cursor.CommentMentionCursor, count: loadCount));
            return new ListData<MergeMessageCursor, ObservableCollection<BaseModel>>(new MergeMessageCursor(mentions.Statuses.LastOrDefault()?.ID ?? 0, comments.Comments.LastOrDefault()?.ID ?? 0, commentMentions.Comments.LastOrDefault()?.ID ?? 0), new ObservableCollection<BaseModel>(mentions.Statuses.Concat<BaseModel>(comments.Comments).Concat(commentMentions.Comments).OrderByDescending(item => item.CreateTime)));
        }
        public struct MergeMessageCursor
        {
            public long MentionCursor { get; internal set; }
            public long CommentCursor { get; internal set; }
            public long CommentMentionCursor { get; internal set; }
            public MergeMessageCursor(long mentionCursor = 0L, long commentCursor = 0L, long commentMentionCursor = 0L)
            {
                MentionCursor = mentionCursor;
                CommentCursor = commentCursor;
                CommentMentionCursor = commentMentionCursor;
            }
            public static MergeMessageCursor DEFAULT => new MergeMessageCursor();
        }
    }
}
