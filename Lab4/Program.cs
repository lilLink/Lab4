using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Magazine magazine = new Magazine("Daily Buglees", Frequency.Monthly, new DateTime(2010, 12, 12), 250000);

            magazine.AddArticles(new Article(new Person("Nick", "Back", new DateTime(1990, 10, 20)),
                "Corona-Time", 2.2));
            magazine.AddEditors(new Person("Kek", "lol", new DateTime(1988, 8, 7)));

            MagazineCollection collection1 = new MagazineCollection()
            {
                CollectionName = "collection1"
            };

            MagazineCollection collection2 = new MagazineCollection()
            {
                CollectionName = "collection2"
            };

            Listener listener1 = new Listener();
            Listener listener2 = new Listener();

            collection1.MagazineAdded += listener1.MagazineListHandler;
            collection1.MagazineReplaced += listener1.MagazineListHandler;

            collection2.MagazineAdded += listener2.MagazineListHandler;
            collection2.MagazineReplaced += listener2.MagazineListHandler;

            collection1.AddDefaults();
            collection1.AddMagazines(magazine);
            collection2.AddMagazines(magazine);

            collection1.Replace(0, collection2[0]);
            collection2[0] = collection1[1];

            Console.WriteLine("Listener1:\n" + listener1);
            Console.WriteLine("Listener2:\n" + listener2);

        }
    }
}
