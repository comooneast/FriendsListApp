using FriendsListApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsListApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(FriendsListContext context)
        {
            context.Database.EnsureCreated();

            // Look for any friends.
            if (context.Friends.Any())
            {
                return;   // DB has been seeded
            }

            var friends = new Friend[]
            {
            new Friend{FirstMidName="Shishlov",LastName="Misha", BirthDate=DateTime.Parse("1984-09-01"), FriendType=FriendType.Best_Friend, NickName="Miha"},
            new Friend{FirstMidName="Pavlov",LastName="Kolya", BirthDate=DateTime.Parse("1992-07-20"), FriendType=FriendType.Friend_of_my_friend, NickName="Kolyan"},
            new Friend{FirstMidName="Esenin",LastName="Dima", BirthDate=DateTime.Parse("1987-05-24"), FriendType=FriendType.Friend, NickName="Esya"},
            new Friend{FirstMidName="Osipets",LastName="Vitya", BirthDate=DateTime.Parse("1986-11-14"), FriendType=FriendType.Friend, NickName="Shlang"},
            new Friend{FirstMidName="But",LastName="Sasha", BirthDate=DateTime.Parse("1986-11-14"), FriendType=FriendType.Just_know_this_guy, NickName="Sancho"},
            new Friend{FirstMidName="But",LastName="Jenya", BirthDate=DateTime.Parse("1986-11-14"), FriendType=FriendType.Totaly_stranger, NickName="Jenya"},
            };
            foreach (Friend s in friends)
            {
                context.Friends.Add(s);
            }
            context.SaveChanges();

            var relations = new Relation[]
            {
            new  Relation{FriendID=1, Date=DateTime.Parse("2017-04-04"), Description="Had a fight" },
            new  Relation{FriendID=3, Date=DateTime.Parse("2017-04-03"), Description="Had a party" },
            new  Relation{FriendID=3, Date=DateTime.Parse("2017-04-02"), Description="Borrowed 100 uah" },
            new  Relation{FriendID=4, Date=DateTime.Parse("2017-04-01"), Description="Drank beer" },
            new  Relation{FriendID=4, Date=DateTime.Parse("2017-03-31"), Description="Did some job" },
            new  Relation{FriendID=2, Date=DateTime.Parse("2017-03-30"), Description="Had a talk" }
            };
            foreach (Relation e in relations)
            {
                context.Relations.Add(e);
            }
            context.SaveChanges();
        }
    }
}
