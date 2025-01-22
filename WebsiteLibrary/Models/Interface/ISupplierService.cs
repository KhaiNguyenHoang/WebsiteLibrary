namespace WebsiteLibrary.Models.Interface;

using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteLibrary.Models.Entites;
public interface ISupplierService
{
    Task AddSupplierAsync(Supplier supplier); // Thêm nhà cung cấp mới
    Task UpdateSupplierAsync(int supplierId, Supplier updatedSupplier); // Cập nhật nhà cung cấp
    Task DeleteSupplierAsync(int supplierId); // Xóa nhà cung cấp
    Task<Supplier> GetSupplierByIdAsync(int supplierId); // Lấy thông tin nhà cung cấp theo ID
    Task<IEnumerable<Supplier>> GetAllSuppliersAsync(); // Lấy danh sách tất cả nhà cung cấp
}
