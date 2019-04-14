using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Model
{
    public class Tweet
    {
        public string Name { get; set; } = "";
        public string Text { get; set; } = "";
        public string ImageUrl { get; set; } = null;

        public Tweet()
        {
        }

        public Tweet(string name, string text, string imageurl = null)
        {
            this.Name = name;
            this.Text = text;
            this.ImageUrl = imageurl;
        }
    }
}
