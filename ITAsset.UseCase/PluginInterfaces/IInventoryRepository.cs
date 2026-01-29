using ITAsset.CoreBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ITAsset.UseCase.PluginInterfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name);
    }
}
