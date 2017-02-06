using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeen.Core.Model;
using OpenWeen.Core.Model.Comment;
using OpenWeen.Core.Model.Status;
using Xamarin.Forms;

namespace OpenWeen.Forms.Common.Selector
{
    public class WeiboListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CommentTemplate { get; set; }
        public DataTemplate MessageTemplate { get; set; }
        public bool IsRepostList { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is BaseModel)
            {
                //Repost Grid.IsVisible will binding to IsRepostList
                (item as BaseModel).IsRepostList = !IsRepostList;
            }
            if (item is CommentModel)
            {
                return CommentTemplate;
            }
            else if (item is MessageModel)
            {
                return MessageTemplate;
            }
            return new DataTemplate();
        }
    }
}
