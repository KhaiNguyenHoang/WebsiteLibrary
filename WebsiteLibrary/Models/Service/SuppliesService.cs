using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Models.Entites;
using WebsiteLibrary.Models.Interface;

namespace WebsiteLibrary.Models.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly LibraryDatabaseContext _context;

        public SupplierService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSupplierAsync(int supplierId, Supplier updatedSupplier)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier == null)
                throw new KeyNotFoundException("Supplier not found");

            supplier.Name = updatedSupplier.Name;
            supplier.ContactName = updatedSupplier.ContactName;
            supplier.Email = updatedSupplier.Email;
            supplier.PhoneNumber = updatedSupplier.PhoneNumber;
            supplier.Address = updatedSupplier.Address;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSupplierAsync(int supplierId)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier == null)
                throw new KeyNotFoundException("Supplier not found");

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(int supplierId)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier == null)
                throw new KeyNotFoundException("Supplier not found");

            return supplier;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }
    }
}
