﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_06
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var users = new Seeds().users;

            //1. Ilość rekordów w tablicy
            int count = users.Count();
            Console.WriteLine($"1. Ilość rekordów w tablicy: {count}");

            //2. Listę nazw użytkowników
            IEnumerable<string> userNameList = from user in users select user.Name;

            Console.WriteLine($"2. Listę nazw użytkowników:");

            foreach (var obj in userNameList)
            {
                Console.WriteLine("\t" + obj);
            }

            //3. Posortowanych użytkowników po nazwach
            IEnumerable<User> sortedUsersByName = from user in users orderby user.Name select user;

            Console.WriteLine($"3. Posortowanych użytkowników po nazwach:");

            foreach (var obj in sortedUsersByName)
            {
                Console.WriteLine("\t" + obj);
            }

            //4. Listę dostępnych ról użytkowników
            IEnumerable<string> roleNameList = (from user in users select user.Role).Distinct();

            Console.WriteLine($"4. Listę dostępnych ról użytkowników");

            foreach (var obj in roleNameList)
            {
                Console.WriteLine("\t" + obj);
            }

            //5. Listy pogrupowanych użytkowników po rolach
            var groupedByRole = users.GroupBy(user => user.Role.Equals(roleNameList));

            Console.WriteLine($"5. Listy pogrupowanych użytkowników po rolach");

            foreach (var grouping in groupedByRole)
            {
                Console.WriteLine(grouping.Key);
                /* foreach (var role in grouping)
                 {
                     Console.WriteLine("\t" + grouping);
                 }*/
            }

            //6. Ilość rekordów, dla których podano oceny (nie null i więcej niż 0)
            var rowCountMarks = (from user in users where user.Marks != null && user.Marks.Length > 0 select users).Count();

            Console.WriteLine($"6. Ilość rekordów, dla których podano oceny (nie null i więcej niż 0): {rowCountMarks}");

            //7. Sumę, ilość i średnią wszystkich ocen studentów
            Console.WriteLine($"7. Sumę, ilość i średnią wszystkich ocen studentów");

            var marks = (from user in users
                         where user.Marks != null
                         from m in user.Marks
                         select m).ToList();

            var marksSum = marks.Sum();
            var marksCount = marks.Count();
            var marksAvg = marks.Average();

            Console.WriteLine($"\tSuma: {marksSum}, ilość: {marksCount}, średnia: {marksAvg}");

            //8. Najlepszą ocenę

            var marksMax = marks.Max();
            Console.WriteLine($"8. Najlepszą ocenę: {marksMax}");

            //9. Najgorszą  ocenę

            var marksMin = marks.Min();
            Console.WriteLine($"9. Najgorszą ocenę: {marksMin}");

            //10. Najlepszego studenta

            var students = (from user in users
                            where user.Marks != null
                            select new { user = user, marksAvg = user.Marks.ToList().Average(), marksCount = user.Marks.ToList().Count() }).ToList();

            var bestStudent = students.Aggregate((s1, s2) => s1.marksAvg > s2.marksAvg ? s1 : s2);

            Console.WriteLine($"10. Najlepszego studenta\r\n\t{bestStudent}");

            //11. Listę studentów, którzy posiadają najmniej ocen

            Console.WriteLine("11. Listę studentów, którzy posiadają najmniej ocen");

            var lowCountMarks = (from user in users
                                 where user.Marks != null
                                 orderby user.Marks.Length descending
                                 select user).Take(3);

            foreach (var user in lowCountMarks)
            {
                Console.WriteLine($"\t{user}");
            }

            //12. Listę studentów, którzy posiadają najmniej ocen

            Console.WriteLine("12. Listę studentów, którzy posiadają najwięcej ocen");

            var highCountMarks = (from user in users
                                  where user.Marks != null
                                  orderby user.Marks.Length
                                  select user).Take(3);

            foreach (var user in highCountMarks)
            {
                Console.WriteLine($"\t{user}");
            }

            //13. Listę obiektów zawierających tylko nazwę i średnią ocenę (mapowanie na inny obiekt)

            Console.WriteLine("13. Listę obiektów zawierających tylko nazwę i średnią ocenę (mapowanie na inny obiekt)");

            var objList = from user in users where user.Marks != null select new { name = user.Name, avg = user.Marks.ToList().Average() };

            foreach (var user in objList)
            {
                Console.WriteLine($"\tName: {user.name}, AVG: {user.avg}");
            }

            //14. Studentów posortowanych od najlepszego

            Console.WriteLine("14. Studentów posortowanych od najlepszego");

            var sortedByBestStudens = from user in students
                                      orderby marksAvg
                                      select user.user;

            foreach (var user in sortedByBestStudens)
            {
                Console.WriteLine($"\t{user}");
            }
        }
    }
}