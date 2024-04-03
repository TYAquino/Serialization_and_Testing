using Assignment3;
using Assignment3.Utility;

namespace Assignment3.Tests
{
    public class SerializationTests
    {
        private ILinkedListADT users;
        private readonly string testFileName = "test_users.bin";

        [SetUp]
        public void Setup()
        {
            // Uncomment the following line
            users = new SLL();
            users.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            users.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));
            users.AddLast(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            users.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));
        }

        [Test]
        public void IsEmpty()
        {
            Assert.IsFalse(users.IsEmpty());
        }

        [Test]
        public void AddFirst()
        {
            var newUser = new User(5, "Jane Doe", "janedoe@example.com", "pass123");
            users.AddFirst(newUser);
            Assert.AreEqual(newUser, users.GetValue(0));
        }

        [Test]
        public void AddLast()
        {
            var newUser = new User(5, "Jane Doe", "janedoe@example.com", "pass123");
            users.AddLast(newUser);
            User lastUser = users.GetValue(users.Count() - 1);
            Assert.AreEqual(newUser, lastUser);
        }

        [Test]
        public void InsertAt_Index()
        {
            var newUser = new User(5, "Jane Doe", "janedoe@example.com", "pass123");
            int indexToInsert = 2; // Insert at index 2
            users.Add(newUser, indexToInsert);
            Assert.AreEqual(newUser, users.GetValue(indexToInsert));
        }

        [Test]
        public void Replace_ItemAtIndex()
        {
            var replacementUser = new User(5, "Jane Doe", "janedoe@example.com", "pass123");
            int indexToReplace = 1; // Replace item at index 1
            users.Replace(replacementUser, indexToReplace);
            Assert.AreEqual(replacementUser, users.GetValue(indexToReplace));
        }

        [Test]
        public void RemoveFirstItem()
        {
            User firstUser = users.GetValue(0);
            users.RemoveFirst();
            Assert.AreNotEqual(firstUser, users.GetValue(0));
        }

        [Test]
        public void RemoveLastItem()
        {
            User lastUser = users.GetValue(users.Count() - 1);
            users.RemoveLast();
            Assert.AreNotEqual(lastUser, users.GetValue(users.Count() - 1));
        }

        [Test]
        public void RemoveMiddleItem()
        {
            int indexToRemove = 1; // Index of the item to remove
            User userToRemove = users.GetValue(indexToRemove);
            users.Remove(indexToRemove);
            Assert.AreNotEqual(userToRemove, users.GetValue(indexToRemove));
        }

        [Test]
        public void FindAndRetrieve()
        {
            User userToFind = new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef");
            int indexOfUser = users.IndexOf(userToFind);
            User foundUser = users.GetValue(indexOfUser);
            Assert.AreEqual(userToFind, foundUser);
        }

        [Test]
        public void Join_TwoLists()
        {
            // Set up another list to join with 'users'
            var otherList = new SLL();
            otherList.AddLast(new User(5, "John Doe", "johndoe@example.com", "jdpassword"));

            // Get count before joining
            int originalCount = users.Count();

            users.Join(otherList);

            // Check the count after joining
            Assert.AreEqual(originalCount + 1, users.Count());
        }

        [TearDown]
        public void TearDown()
        {
            this.users.Clear();
        }

        /// <summary>
        /// Tests the object was serialized.
        /// </summary>
        [Test]
        public void TestSerialization()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            Assert.IsTrue(File.Exists(testFileName));
        }

        /// <summary>
        /// Tests the object was deserialized.
        /// </summary>
        [Test]
        public void TestDeSerialization()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            SLL deserializedUsers = (SLL)SerializationHelper.DeserializeUsers(testFileName);

            //log counts for debugging
            Console.WriteLine("Count of users: " + users.Count());
            Console.WriteLine("Count of deserializedUsers: " + deserializedUsers.Count());

            Assert.That(condition: users.Count() == deserializedUsers.Count());

            for (int i = 0; i < users.Count(); i++)
            {
                User expected = users.GetValue(i);
                User actual = deserializedUsers.GetValue(i);

                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Email, actual.Email);
                Assert.AreEqual(expected.Password, actual.Password);
            }
        }
    }
}
