using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsListApp.Models
{
    public class Relation
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int FriendID { get; set; }
        public string Description { get; set; }

        public Friend Friend { get; set; }
    }
}
