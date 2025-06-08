using Weav_App.DTOs.Entities.User;

namespace Weav_App.Models.ViewsModel;

public class CustomerPageViewModel : GeneralPageViewModel
{
    public List<UserListDTO> UsersList { get; set; }
}