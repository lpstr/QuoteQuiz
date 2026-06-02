using Microsoft.EntityFrameworkCore;
using QuoteQuiz.Domain.Entities;
using QuoteQuiz.Domain.Enums;
using QuoteQuiz.Infrastructure;
using System;

namespace QuoteQuiz.Infrastructure;

public static class DbSeeder
{
    public static async Task SeedAsync(QuizDbContext db)
    {
        await SeedUsers(db);
        await SeedAuthors(db);
        await SeedQuotes(db);
        await SeedSessions(db);
        await SeedRoles(db);
        await SeedUserRoles(db);
    }

    private static async Task SeedAuthors(QuizDbContext db)
    {
        if (await db.Authors.AnyAsync()) return;

        var authors = new List<Author>
        {
            new() { Name = "Albert Einstein" },
            new() { Name = "Mark Twain" },
            new() { Name = "Oscar Wilde" },
            new() { Name = "Friedrich Nietzsche" },
            new() { Name = "George Bernard Shaw" },
            new() { Name = "Socrates" },
            new() { Name = "Plato" },
            new() { Name = "Aristotle" },
            new() { Name = "Confucius" },
            new() { Name = "Sun Tzu" },
            new() { Name = "Leonardo da Vinci" },
            new() { Name = "Benjamin Franklin" },
            new() { Name = "Ralph Waldo Emerson" },
            new() { Name = "Henry David Thoreau" },
            new() { Name = "Jane Austen" },
            new() { Name = "Charles Dickens" },
            new() { Name = "Leo Tolstoy" },
            new() { Name = "William Shakespeare" },
            new() { Name = "Voltaire" },
            new() { Name = "Blaise Pascal" }
        };

        db.Authors.AddRange(authors);
        await db.SaveChangesAsync();
    }

    private static async Task SeedUsers(QuizDbContext db)
    {
        if (await db.Users.AnyAsync()) return;

        var users = new List<User>
        {
            new() { Username = "AUser", Email = "auser@email.com", IsDisabled = false },
            new() { Username = "BBUser", Email = "hhhhh@email.com", IsDisabled = false },
            new() { Username = "CCCUser", Email = "teascccc@email.com", IsDisabled = false },
            new() { Username = "DEDUser", Email = "fsaaswe@email.com", IsDisabled = false },
            new() { Username = "FERUser", Email = "dsafhhh@email.com", IsDisabled = false },
            new() { Username = "QWERUser", Email = "erasuser@email.com", IsDisabled = false },
            new() { Username = "TYUUser", Email = "tyuser@email.com", IsDisabled = false },
            new() { Username = "admin", Email ="admin@admin.com", IsDisabled = false },
            new() { Username = "adminSub", Email= "subadmin@subadmin.com", IsDisabled = false }
        };
        db.Users.AddRange(users);
        await db.SaveChangesAsync();
    }

    private static async Task SeedSessions(QuizDbContext db)
    {
        if (await db.GameSessions.AnyAsync()) return;

        var users = await db.Users.Take(5).ToListAsync();
        var quotes = await db.Quotes.Include(q => q.Author).ToListAsync();
        var random = new Random();

        var sessions = new List<GameSession>();

        foreach (var user in users)
        {
            int sessionCount = random.Next(4, 6);

            for (int i = 0; i < sessionCount; i++)
            {
                var session = new GameSession
                {
                    UserId = user.Id,
                    StartedAt = DateTime.UtcNow.AddDays(-random.Next(1, 20)),
                    FinishedAt = DateTime.UtcNow.AddDays(-random.Next(1, 20)).AddMinutes(5),
                    Questions = new List<GameQuestion>()
                };

                int questionCount = random.Next(5, 9);
                var selectedQuotes = quotes.OrderBy(_ => random.Next()).Take(questionCount).ToList();

                foreach (var quote in selectedQuotes)
                {
                    bool isBinary = random.Next(0, 2) == 0;

                    if (isBinary)
                    {
                        // Binary mode
                        bool answerYes = random.Next(0, 2) == 0;
                        bool isCorrect = answerYes;

                        session.Questions.Add(new GameQuestion
                        {
                            QuoteId = quote.Id,
                            Mode = GameMode.Binary,
                            SuggestedAuthorId = quote.AuthorId,
                            SelectedAuthorId = null,
                            AnswerYesNo = answerYes,
                            IsCorrect = isCorrect,
                            AnsweredAt = session.StartedAt.AddSeconds(random.Next(10, 120))
                        });
                    }
                    else
                    {
                        // Multiple choice mode
                        var wrongAuthor = quotes
                            .Where(q => q.AuthorId != quote.AuthorId)
                            .OrderBy(_ => random.Next())
                            .First()
                            .Author;

                        bool pickCorrect = random.Next(0, 2) == 0;

                        session.Questions.Add(new GameQuestion
                        {
                            QuoteId = quote.Id,
                            Mode = GameMode.MultipleAnswer,
                            SuggestedAuthorId = null,
                            SelectedAuthorId = pickCorrect ? quote.AuthorId : wrongAuthor.Id,
                            AnswerYesNo = null,
                            IsCorrect = pickCorrect,
                            AnsweredAt = session.StartedAt.AddSeconds(random.Next(10, 120))
                        });
                    }
                }

                sessions.Add(session);
            }
        }

        db.GameSessions.AddRange(sessions);
        await db.SaveChangesAsync();
    }

    private static async Task SeedQuotes(QuizDbContext db)
    {
        if (await db.Quotes.AnyAsync()) return;

        var authors = await db.Authors.ToListAsync();
        Author A(string name) => authors.First(a => a.Name == name);

        var quotes = new List<Quote>
        {
            new() { Text = "Life is like riding a bicycle. To keep your balance, keep moving.", AuthorId = A("Albert Einstein").Id },
            new() { Text = "Imagination is more important than knowledge.", AuthorId = A("Albert Einstein").Id },
            new() { Text = "The secret of getting ahead is getting started.", AuthorId = A("Mark Twain").Id },
            new() { Text = "Courage is resistance to fear, not absence of fear.", AuthorId = A("Mark Twain").Id },
            new() { Text = "Be yourself; everyone else is already taken.", AuthorId = A("Oscar Wilde").Id },
            new() { Text = "Experience is simply the name we give our mistakes.", AuthorId = A("Oscar Wilde").Id },
            new() { Text = "He who has a why to live can bear almost any how.", AuthorId = A("Friedrich Nietzsche").Id },
            new() { Text = "No price is too high to pay for owning yourself.", AuthorId = A("Friedrich Nietzsche").Id },
            new() { Text = "Progress is impossible without change.", AuthorId = A("George Bernard Shaw").Id },
            new() { Text = "Life isn't about finding yourself. Life is about creating yourself.", AuthorId = A("George Bernard Shaw").Id },
            new() { Text = "The only true wisdom is knowing you know nothing.", AuthorId = A("Socrates").Id },
            new() { Text = "An unexamined life is not worth living.", AuthorId = A("Socrates").Id },
            new() { Text = "The beginning is the most important part of the work.", AuthorId = A("Plato").Id },
            new() { Text = "Courage is knowing what not to fear.", AuthorId = A("Plato").Id },
            new() { Text = "We are what we repeatedly do.", AuthorId = A("Aristotle").Id },
            new() { Text = "Knowing yourself is the beginning of all wisdom.", AuthorId = A("Aristotle").Id },
            new() { Text = "Everything has beauty, but not everyone sees it.", AuthorId = A("Confucius").Id },
            new() { Text = "It does not matter how slowly you go as long as you do not stop.", AuthorId = A("Confucius").Id },
            new() { Text = "In the midst of chaos, there is opportunity.", AuthorId = A("Sun Tzu").Id },
            new() { Text = "Victorious warriors win first and then go to war.", AuthorId = A("Sun Tzu").Id },
            new() { Text = "Learning never exhausts the mind.", AuthorId = A("Leonardo da Vinci").Id },
            new() { Text = "Simplicity is the ultimate sophistication.", AuthorId = A("Leonardo da Vinci").Id },
            new() { Text = "Well done is better than well said.", AuthorId = A("Benjamin Franklin").Id },
            new() { Text = "Energy and persistence conquer all things.", AuthorId = A("Benjamin Franklin").Id },
            new() { Text = "What lies behind us and what lies before us are tiny matters compared to what lies within us.", AuthorId = A("Ralph Waldo Emerson").Id },
            new() { Text = "To be yourself in a world that is constantly trying to make you something else is the greatest accomplishment.", AuthorId = A("Ralph Waldo Emerson").Id },
            new() { Text = "Go confidently in the direction of your dreams.", AuthorId = A("Henry David Thoreau").Id },
            new() { Text = "Our life is frittered away by detail. Simplify, simplify.", AuthorId = A("Henry David Thoreau").Id },
            new() { Text = "There is no charm equal to tenderness of heart.", AuthorId = A("Jane Austen").Id },
            new() { Text = "Know your own happiness.", AuthorId = A("Jane Austen").Id },
            new() { Text = "Have a heart that never hardens, and a temper that never tires.", AuthorId = A("Charles Dickens").Id },
            new() { Text = "No one is useless in this world who lightens the burden of another.", AuthorId = A("Charles Dickens").Id },
            new() { Text = "If you want to be happy, be.", AuthorId = A("Leo Tolstoy").Id },
            new() { Text = "The two most powerful warriors are patience and time.", AuthorId = A("Leo Tolstoy").Id },
            new() { Text = "We know what we are, but know not what we may be.", AuthorId = A("William Shakespeare").Id },
            new() { Text = "There is nothing either good or bad, but thinking makes it so.", AuthorId = A("William Shakespeare").Id },
            new() { Text = "Judge a man by his questions rather than his answers.", AuthorId = A("Voltaire").Id },
            new() { Text = "Doubt is not a pleasant condition, but certainty is absurd.", AuthorId = A("Voltaire").Id },
            new() { Text = "The heart has its reasons which reason knows nothing of.", AuthorId = A("Blaise Pascal").Id },
            new() { Text = "Small minds are concerned with the extraordinary, great minds with the ordinary.", AuthorId = A("Blaise Pascal").Id }
        };

        db.Quotes.AddRange(quotes);
        await db.SaveChangesAsync();
    }

    private static async Task SeedRoles(QuizDbContext db)
    {
        if (await db.Roles.AnyAsync()) return;

        db.Roles.AddRange(
            new Role { Name = "Admin" },
            new Role { Name = "User" }
        );

        await db.SaveChangesAsync();
    }

    private static async Task SeedUserRoles(QuizDbContext db)
    {
        if (await db.UserRoles.AnyAsync()) return;

        //var admin = await db.Users.FirstAsync(u => u.Username == "admin");
        //var subAdmin = await db.Users.FirstAsync(u => u.Username == "adminSub");
        //var userRole = await db.Roles.FirstAsync(r => r.Name == "User");
        //var adminRole = await db.Roles.FirstAsync(r => r.Name == "Admin");


        var userList = await db.Users.ToListAsync();

        List<UserRole> userRoles = new List<UserRole>();

        User A(string username) => userList.First(a => a.Username == username);

        var userRole = await db.Roles.FirstAsync(r => r.Name == "User");
        var adminRole = await db.Roles.FirstAsync(r => r.Name == "Admin");

        foreach (var user in userList)
        {
            UserRole role = new UserRole();

            role.UserId = user.Id;
            role.RoleId = (user.Username == "admin" || user.Username == "adminSub") ? adminRole.Id : userRole.Id;

            userRoles.Add(role);
        }

        db.UserRoles.AddRange(userRoles);

        await db.SaveChangesAsync();
    }
}
