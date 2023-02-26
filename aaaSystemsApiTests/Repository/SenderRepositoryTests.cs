using Microsoft.VisualStudio.TestTools.UnitTesting;
using aaaSystemsCommon.Entity;
using Microsoft.EntityFrameworkCore;
using aaaSystemsCommon.Difinitions;

namespace aaaSystemsApi.Repository.Tests
{
    [TestClass()]
    public class SenderRepositoryTests
    {
        private const long countSenders = 5;
        private readonly DbContextOptions<AppDbContext> options;

        public SenderRepositoryTests()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                   .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                   .Options;

            // Insert seed data 
            using var context = new AppDbContext(options);

            context.Senders.AddRange(GetTestSenders());
            context.SaveChanges();
        }


        //[TestMethod()]
        //public async Task SenderRepositoryTesUniqueId()
        //{
        //    using (var context = new AppDbContext(options))
        //    {
        //        var repository = new SenderRepository(context);

        //        const  id = 10;
        //        Assert.IsTrue(id >= countSenders);

        //        await repository.Delete(id);

        //        var senders = await repository.Read();

        //        foreach (var sender in senders)
        //        {
        //            Assert.IsFalse(sender.Id == id);
        //        }
        //    }

        //  NOT WORK!!!!!!!!
        //[TestMethod()]
        //public async Task SenderRepositoryTestCreate()
        //{
        //    using (var context = new AppDbContext(options))
        //    {
        //        var repository = new SenderRepository(context);

        //        const long id = 10;
        //        Assert.IsTrue(id >= countSenders);

        //        var createSender = () => repository.Create(GetSender(id));

        //        await createSender.Invoke();

        //        repository = new SenderRepository(context);

        //        await Assert.ThrowsExceptionAsync<ArgumentException>(() => repository.Create(GetSender(id)));
        //    }
        //}

        //private Sender GetSender(long id)
        //{
        //    return new Sender()
        //    {
        //        Id = id,
        //        Name = $"Name{id}",
        //        Phone = $"Phone{id}",
        //        Role = (id / 2 == 0) ? Role.Client : Role.Client
        //    };
        //}


        [TestMethod()]
        public async Task SenderRepositoryTestCreate()
        {
            using (var context = new AppDbContext(options))
            {
                var repository = new SenderRepository(context);

                var id = 10;
                Assert.IsTrue(id >= countSenders);

                var someSender = new Sender()
                {
                    Id = id,
                    Name = $"Name{id}",
                    Phone = $"Phone{id}",
                    Role = (id / 2 == 0) ? Role.Client : Role.Client
                };
                var createSender = () => repository.Create(someSender);

                await createSender.Invoke();
                await Assert.ThrowsExceptionAsync<ArgumentException>(createSender);
            }
        }

        [TestMethod()]
        public async Task SenderRepositoryTestReadAll()
        {
            using (var context = new AppDbContext(options))
            {
                var repository = new SenderRepository(context);

                var allSenders = await repository.Read();
                Assert.IsTrue(allSenders.Count == countSenders);
            }
        }

        [TestMethod()]
        public async Task SenderRepositoryTestReadOne()
        {
            using (var context = new AppDbContext(options))
            {
                var repository = new SenderRepository(context);

                var id = 0;
                Assert.IsTrue(id < countSenders);

                var sender = await repository.ReadFirst(s => s.Id == id);

                Assert.IsTrue(sender.Id == id);
            }
        }

        [TestMethod()]
        public async Task SenderRepositoryTestUpdate()
        {
            using (var context = new AppDbContext(options))
            {
                var repository = new SenderRepository(context);

                const long id = 0;
                Assert.IsTrue(id < countSenders);

                var name = "NewName";
                var sender = await repository.ReadFirst(s => s.Id == id);
                sender.Name = name;

                await repository.Update(sender);
                var changedSender = await repository.ReadFirst(s => s.Id == id);

                Assert.AreEqual(sender.Id, id);
                Assert.AreEqual(changedSender.Id, id);
                Assert.AreEqual(changedSender.Name, name);
            }
        }

        [TestMethod()]
        public async Task SenderRepositoryTestDelete()
        {
            using (var context = new AppDbContext(options))
            {
                var repository = new SenderRepository(context);

                const long id = 0;
                Assert.IsTrue(id < countSenders);

                await repository.Delete(id);

                var senders = await repository.Read();

                foreach (var sender in senders)
                {
                    Assert.IsFalse(sender.Id == id);
                }
            }
        }

        //private static List<DialogMessage> GetTestDialogMessages()
        //{
        //    var messages = new List<DialogMessage>();
        //    for (int i = 0; i < 3; i++)
        //    {
        //        messages.Add(new DialogMessage()
        //        {
        //            Id = 2000 + i,
        //            DateTime = DateTime.Now,
        //            ChatId = i,
        //        });
        //    }
        //    return messages;
        //}

        //private static List<Dialog> GetTestDialogs()
        //{
        //    var dialogs = new List<Dialog>();
        //    for (int i = 0; i < countSenders - 2; i++)
        //    {
        //        dialogs.Add(new Dialog()
        //        {
        //            ChatId = i,
        //        });
        //    }
        //    return dialogs;
        //}

        private static List<Sender> GetTestSenders()
        {
            var senders = new List<Sender>();
            for (int i = 0; i < countSenders; i++)
            {
                senders.Add(new Sender()
                {
                    Id = i,
                    Name = $"Name{i}",
                    Phone = $"Phone{i}",
                    Role = (i / 2 == 0) ? Role.Client : Role.Client
                });
            }
            return senders;
        }
    }
}