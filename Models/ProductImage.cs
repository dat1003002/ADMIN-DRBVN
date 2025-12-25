namespace AspnetCoreMvcFull.Models
{
  public class ProductImage
  {
    public int Id { get; set; }                       

    public string ImagePath { get; set; } = null!; 

    public int ProductId { get; set; }   

    public Product Product { get; set; } = null!;  

    public bool IsMain { get; set; } = false;       
    public int SortOrder { get; set; } = 0;      

    public bool IsGeneratedFromPdf { get; set; } = false;
  }
}
