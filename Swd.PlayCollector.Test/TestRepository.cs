using System.Data;

namespace Swd.PlayCollector.Test
{
    [TestClass]
    public class TestRepository
    {
        public TestRepository()
        {
            EmptyDatabase();
        }

        [TestMethod]
        public void Add_CollectionItem()
        {
            // Arrange 
            
            CollectionItem item = GetCollectionItem();
             string itemName = "Testitem";
            // Act
            CollectionItemRepository repo = new();
            repo.Add(item);

            // Assert
            Assert.AreNotEqual(0, item.Id);

        }

        [TestMethod]
        public async Task Add_CollectionItemAsync()
        {
            // Arrange 
            CollectionItem item = GetCollectionItem();
            string itemName = "Testitem";
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            // Act
            CollectionItemRepository repo = new();
            await repo.AddAsync(item);

            // Assert
            Assert.AreNotEqual(0, item.Id);

        }

        [TestMethod]
        [DataRow(0.0)] // Für jede Zeile
        [DataRow(-10.0)]
        [DataRow(10.0)]
        public void Add_CollectionItemWithPrice(double price)
        {
            // Arrange 
            CollectionItem item = GetCollectionItem();
            string itemName = "Testitem";
            item.Price = Convert.ToDecimal(price);
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            // Act
            CollectionItemRepository repo = new();
            repo.Add(item);

            // Assert
            Assert.AreNotEqual(0, item.Id);

        }

        [TestMethod]
        public void Update_CollectionItem()
        {
            // Arrange 
            CollectionItem item = GetCollectionItem();
            CollectionItemRepository repo = new();
            string itemName = "Testitem";
            item.Name = itemName;
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            // Act
            repo.Add(item);
            
            CollectionItem addedItem = repo.GetById(item.Id);
            addedItem.Name = String.Format("Testitem{0}", DateTime.Now);
            repo.Update(addedItem, addedItem.Id);

            CollectionItem updateItem = repo.GetById(item.Id);

            
            // Assert
            Assert.AreNotEqual(itemName, updateItem.Name);

        }

        [TestMethod]
        public void Add_Location()
        {
            // Arrange 
            Location item = new();
            item.Name = "Testlocation";
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            // Act
            LocationRepository repo = new();
            repo.Add(item);

            // Assert
            Assert.AreNotEqual(0, item.Id);
        }

        [TestMethod]
        public async Task Add_LocationAsync()
        {
            // Arrange 
            Location item = new();
            item.Name = "Testlocation";
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            // Act
            LocationRepository repo = new();
            await repo.AddAsync(item);

            // Assert
            Assert.AreNotEqual(0, item.Id);
        }

        [TestMethod]
        public void Delete_CollectionItem()
        {
            // Arrange 
            CollectionItemRepository repo = new();
            string itemName = "Testitem";
            CollectionItem item = new();
            item.Name = itemName;
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            // Act
            repo.Add(item);
            int id = item.Id;
            repo.Delete(id);

            CollectionItem deletedItem = repo.GetById(item.Id);

            // Assert
            Assert.IsNull(deletedItem);

        }

        [TestMethod]
        public void GetAll_CollectionItem()
        {
            // Arrange 
            CollectionItemRepository repo = new();
            CollectionItem item = GetCollectionItem();
            string itemName = "Testitem";
            repo.Add(item);

            // Act
            int itemCount = repo.GetAll().Count();

            // Assert
            Assert.AreNotEqual(0, itemCount);

        }

        [TestMethod]
        public async Task GetAll_CollectionItemAsync()
        {
            // Arrange 
            CollectionItemRepository repo = new();
            CollectionItem item = GetCollectionItem();
            string itemName = "Testitem";
            await repo.AddAsync(item);

            // Act
            int itemCount = await repo.GetAllAsync().Result.CountAsync();

            // Assert
            Assert.AreNotEqual(0, itemCount);

        }

        private static CollectionItem GetCollectionItem()
        {
            CollectionItem item = new();
            item.Name = "Testitem";
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            return item;
        }

        // mockaroo fake data

        private static void EmptyDatabase()
        {
            PlayCollectorContext testContext = new();

            var command = testContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = "spEmptyDatabase";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            // Bsp. für einen Parameter der an die SP übergeben wird
            // command.Parameters.Add(new SqlParameter("parametername", "parametervalue"));


            testContext.Database.OpenConnection();
            command.ExecuteNonQuery();

            // Bsp. um Rückgabewerte zu verarbeiten
            // var result = command.ExecuteReader();
            // var dataTable = new DataTable();
            // dataTable.Load(result);

        }
    }
}