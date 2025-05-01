using System;
using System.Collections.Generic;

class Program
{
    static Random rng = new Random();
    static List<string> inventory = new List<string>();
    static int health = 100;
    static int luck;

    static void Main(string[] args)
    {
        luck = rng.Next(1, 101);

        Console.WriteLine("🌄 Ти турист, що заблукав у джунглях. Виживи серед диких загроз.");
        Pause();

        Event("📍 Прокинувся під деревом. Що робити?", "Іти вглиб лісу", "Залишитися", 0, -10);
        Event("🕷️ Павук на руці!", "Зірвати", "Залишити", -5, -25);
        Event("🎒 Ти знаходиш рюкзак. Взяти?", "Так", "Ні", 0, 0, "Аптечка");
        StrangerEncounter();
        Event("🌪️ Буря насувається", "Сховатися", "Продовжити", 0, -30);
        Event("🔥 Табір туземців", "Підійти", "Сховатися", -40, 0);
        UseItemPrompt(); // можливість використати аптечку
        Event("👹 Монстр у печері", "Атакувати", "Втекти", -50, -20);
        FinalEvent();

        EndGame();
    }

    static void Event(string desc, string opt1, string opt2, int change1, int change2, string foundItem = null)
    {
        Console.WriteLine($"\n{desc}");
        Console.WriteLine($"1. {opt1}");
        Console.WriteLine($"2. {opt2}");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            health += change1;
            if (foundItem != null)
            {
                Console.WriteLine($"🎁 Ти знайшов: {foundItem}!");
                inventory.Add(foundItem);
            }
        }
        else
        {
            health += change2;
        }

        Console.WriteLine($"❤️ Здоров'я: {health}");
        ShowInventory();
        Pause();
    }

    static void StrangerEncounter()
    {
        Console.WriteLine("🧍 Ти зустрічаєш чоловіка.");
        Console.WriteLine("1. Підійти");
        Console.WriteLine("2. Ігнорувати");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            if (rng.Next(1, 101) <= luck)
            {
                Console.WriteLine("🤝 Він дає тобі ліки.");
                inventory.Add("Аптечка");
            }
            else
            {
                Console.WriteLine("🪓 Це грабіжник! Він ранить тебе і краде предмет.");
                health -= 30;
                if (inventory.Count > 0)
                {
                    Console.WriteLine($"💥 Втратив: {inventory[0]}");
                    inventory.RemoveAt(0);
                }
            }
        }

        Pause();
    }

    static void UseItemPrompt()
    {
        if (inventory.Contains("Аптечка"))
        {
            Console.WriteLine("\n💊 У тебе є аптечка. Хочеш використати?");
            Console.WriteLine("1. Так");
            Console.WriteLine("2. Ні");
            string input = Console.ReadLine();
            if (input == "1")
            {
                inventory.Remove("Аптечка");
                health += 25;
                Console.WriteLine("🩹 Ти використав аптечку. +25 HP");
            }
        }
    }

    static void FinalEvent()
    {
        Console.WriteLine("🚁 Ти бачиш вертоліт. Що зробити?");
        Console.WriteLine("1. Палити вогонь");
        Console.WriteLine("2. Махати руками");
        string choice = Console.ReadLine();
        if (choice == "1")
        {
            if (inventory.Contains("Запальничка"))
            {
                Console.WriteLine("🚁 Тебе помітили і врятували!");
                health += 999;
            }
            else
            {
                Console.WriteLine("🔥 Нема чим запалити вогонь.");
                health -= 50;
            }
        }
        else
        {
            Console.WriteLine("👋 Тебе не помітили.");
            health -= 20;
        }

        Pause();
    }

    static void ShowInventory()
    {
        Console.WriteLine("🎒 Інвентар: " + (inventory.Count == 0 ? "порожній" : string.Join(", ", inventory)));
    }

    static void Pause()
    {
        Console.WriteLine("\n(Натисни Enter...)");
        Console.ReadLine();
    }

    static void EndGame()
    {
        Console.WriteLine($"\n❤️ Кінцеве здоров’я: {health}");
        if (health <= 0)
            Console.WriteLine("💀 Ти не вижив. Гру завершено.");
        else if (health > 200)
            Console.WriteLine("🏆 Ти врятований і вижив! Молодець!");
        else
            Console.WriteLine("😌 Ти вижив, але ледве-ледве...");
    }
}
