using ITAsset.CoreBusiness.Entities;
using ITAsset.UseCase.PluginInterfaces;

namespace ITAsset.Plugin.InMemory
{
    internal class InventoryRepository : IInventoryRepository
    {
        private List<Inventory> _inventories;
        public InventoryRepository()
        {
            _inventories = new List<Inventory>()
            {
                new Inventory() { InventoryId = 1, InventoryName = "Case", Quantity = 5 },
                new Inventory() {InventoryId=2,InventoryName="Monitor",Quantity=5 },
                new Inventory() {InventoryId=3,InventoryName="Mouse",Quantity=15 },
                new Inventory() {InventoryId=4,InventoryName="Spekar",Quantity=6 }
            };
        }
        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(_inventories);
            return _inventories.Where(x => x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
