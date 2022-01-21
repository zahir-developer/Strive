using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.TimInventory;


namespace StriveOwner.Android.Helper
{
    public interface MyButtonClickListener
    {
        void OnClick(int position,checkOutViewModel checkOut);         
    }
    public interface DeleteButtonClickListener 
    {
        void OnClick(InventoryDataModel filteredlist);
    }
    
}