using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ViewModels
{
    public class MessageViewModel
    {
        public MessageViewModel(string sender, string text, DateTime postedDate)
        {
            Sender = sender;
            Text = text;
            PostedDate = postedDate;
        }

        public string Sender { get; set; }
        public string Text { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
