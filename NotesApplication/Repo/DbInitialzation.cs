using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApplication.Models;


namespace NotesApplication.Repo
{

    public static class DbInitializer
    {
        public static void Initialize(NoteContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
            new User{Email="Bob@gmail.com",Name="Bob Barker",CreatedOn=DateTime.Parse("2018-04-28")},
            new User{Email="Sally@gmail.com",Name="Ryan Reynolds",CreatedOn=DateTime.Parse("2018-04-29")},
            new User{Email="Ryan@gmail.com",Name="Sally Fields",CreatedOn=DateTime.Parse("2018-04-30")}
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();


            var notes = new Note[]
            {
                new Note{Title="Test 1",Notes="Test 1",CategoryId=1,userId=1,IsDeleted=false },
                new Note{Title="Test 2",Notes="Test 2",CategoryId=2,userId=2,IsDeleted=false },
                new Note{Title="Test 3",Notes="Testing 3",CategoryId=3,userId=3,IsDeleted=false }
            };
            foreach (Note n in notes)
            {
                context.Notes.Add(n);
            }
            context.SaveChanges();

            var categories = new Category[]
            {
            new Category{Name="Pre-Calculus"},
            new Category{Name="Network Security"},
            new Category{Name="Internet Programming"}
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            
        }
    }
}