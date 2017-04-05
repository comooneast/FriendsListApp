using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsListApp.Models
{
    public enum FriendType
    {
        Best_Friend, Friend, Friend_of_my_friend, Just_know_this_guy, Totaly_stranger
    }

    public class Friend
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string NickName { get; set; }
        public DateTime BirthDate { get; set; }
        public FriendType? FriendType { get; set; }


        public ICollection<Relation> Relations { get; set; }
    }
}
