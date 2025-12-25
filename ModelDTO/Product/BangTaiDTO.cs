using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.ModelDTO.Product
{
    public class BangTaiDTO
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }

        // File upload
        public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
        public IFormFile? PdfFile { get; set; }

        // Danh sách đường dẫn ảnh hiện có (dùng để hiển thị)
        public List<string>? ExistingImagePaths { get; set; }

        // Danh sách Id ảnh cần xóa (giữ lại để tương thích cũ, nhưng không dùng nữa)
        public List<int> DeletedImageIds { get; set; } = new List<int>();

        // THÊM MỚI: Danh sách đường dẫn ảnh cần xóa (dùng để xóa chính xác)
        public List<string> DeletedImagePaths { get; set; } = new List<string>();
    }
}
