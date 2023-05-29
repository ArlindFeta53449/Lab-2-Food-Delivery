namespace Data.DTOs
{
    public class MenuItemOfferDto
    {
       public int? Id{get;set;}

       public int? MenuItemId {get;set;}

       public int? OfferId {get;set;}

       public int? Quantity {get;set;}
       public MenuItemDto MenuItemDto{get;set;}
       public OfferDto OfferDto {get;set;}
    }
}
