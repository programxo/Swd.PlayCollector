using Swd.PlayCollector.Model;
using Swd.PlayCollector.Repository;

namespace Swd.PlayCollector.Test
{
    [TestClass]
    public class TestRepository
    {

        [TestMethod]
        public void Add_CollectionItem()
        {
            // Arrange 
            CollectionItem item = new();
            item.Name = "Testitem";
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

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
            CollectionItem item = new();
            item.Name = "Testitem";
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
            CollectionItem item = new();
            item.Name = "Testitem";
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
            CollectionItemRepository repo = new();
            string itemName = "Testitem";
            CollectionItem item = new();
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
        
            // Act
            var items = repo.GetAll();
            var items2 = repo.GetAll().ToList();

            int itemCount = repo.GetAll().Count();

            // Assert
            Assert.AreNotEqual(0, itemCount);

        }
    }
}