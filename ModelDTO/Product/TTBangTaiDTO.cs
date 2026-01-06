namespace AspnetCoreMvcFull.ModelDTO.Product
{
  public class TTBangTaiDTO
  {
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public int CategoryId { get; set; }

    public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
    public IFormFile? PdfFile { get; set; }

    public List<string>? ExistingImagePaths { get; set; }

    public List<int> DeletedImageIds { get; set; } = new List<int>();

    public List<string> DeletedImagePaths { get; set; } = new List<string>();
  }
}
