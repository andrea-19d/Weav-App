using Weav_App.Models;

namespace Weav_App.Services.General.InsertStrategies;

public interface IStrategy<T>
{
    Task<ServiceResult<T>> CreateItem(T model);
}
